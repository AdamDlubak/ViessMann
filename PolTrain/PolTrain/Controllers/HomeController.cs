using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PolTrain.DAL;
using PolTrain.Models;
using PolTrain.ViewModels;
using static PolTrain.ViewModels.HomeViewModel;

namespace PolTrain.Controllers
{
    public class HomeController : Controller
    {
        private PolTrainContext db = new PolTrainContext();

        // GET: Home
        //     private static Logger logger = LogManager.GetCurrentClassLogger();

        //    private ICacheProvider cache;

        //        public HomeController(PolTrainContext context, ICacheProvider cache)
        //        {
        //            this.db = context;
        //            this.cache = cache;
        //        }

        //

        // GET: /Home/
        public ActionResult Index()
        {
            var stations = db.Stations.GroupBy(c => c.City).Select(g => g.FirstOrDefault()).ToList();
            Station station = new Station();
            IEnumerable<Reservation> reservations = null;
            var userId = User.Identity.GetUserId();

            reservations = db.Reservations.Where(o => o.UserId == userId).ToList();
            var vm = new IndexViewModel()
            {
                Stations = stations,
                Station = station,
                Reservations = reservations,
                
                
            };
            return View(vm);
        }



      //      logger.Info("Visited main page");
//
//            var bestsellers = db.Albums.Where(a => a.IsBestseller && !a.IsHidden).OrderBy(g => Guid.NewGuid()).Take(3);
//
//            List<Album> newArrivals;
//
//            //ICacheProvider cache = new DefaultCacheProvider();
//            if (cache.IsSet(Consts.NewItemsCacheKey))
//            {
//                newArrivals = cache.Get(Consts.NewItemsCacheKey) as List<Album>;
//            }
//            else
//            {
//                newArrivals = db.Albums.Where(a => !a.IsHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
//                cache.Set(Consts.NewItemsCacheKey, newArrivals, 30);
//            }
//
//            //var newArrivals = db.Albums.Where(a => !a.IsHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
//
//            var genres = db.Genres;
//



        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }


        [HttpPost]
        public ActionResult FindLines(FindLinesViewModel model)
        {
            var startCity = db.Stations.Where(a => a.City == model.StartCity).ToList();
            var endCity = db.Stations.Where(a => a.City == model.EndCity).ToList();
            var tracks = db.Tracks.ToList();

            

            var lines = new List<Line>();
            foreach (Station startStation in startCity)
            {
                foreach (var endStation in endCity)
                {
                    var nextTime = model.Arrival.AddHours(24);
                    var previousTime = model.Arrival.AddHours(-2);
                    List<Line> line2 =
                        db.Lines.Where(a => (a.Track.StartStationId == startStation.StationId) &&
                                (a.Track.EndStationId == endStation.StationId)).ToList();
                    List<Line> line = new List<Line>();

                    foreach (var line1 in line2)
                    {
                        if (line1.Arrival >= previousTime && line1.Arrival <= nextTime)
                        {
                            line.Add(line1);
                        }
                    }
                        foreach (var l in line)
                        {
                            
                        lines.Add(l);
                        
                        }
                        line.Clear();
                    }
                }
            var concessions = db.Concessions.ToList();
            var concession = new Concession();

            foreach (var line in lines)
            {
                tracks.Select(a => a.TrackId == line.TrackId).FirstOrDefault();

                var start = db.Stations.FirstOrDefault(a => a.StationId == line.Track.StartStationId);
                line.Track.StartStation = start;

                var end = db.Stations.FirstOrDefault(a => a.StationId == line.Track.EndStationId);
                line.Track.EndStation = end;
 
            }

            var vm = new CustomerViewModel.AddTicketViewModel()
            {
                Lines = lines,
                Concessions = concessions,
                Concession = concession,

                
            };
            return View(vm);    

        }
    }
}