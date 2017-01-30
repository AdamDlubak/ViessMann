using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PolTrain.DAL;
using PolTrain.Models;
using PolTrain.ViewModels;
using static PolTrain.ViewModels.CustomerViewModel;

namespace PolTrain.Controllers
{
    public class CustomerController : Controller
    {
        private PolTrainContext db = new PolTrainContext();


        // GET: Customer
        public ActionResult AddTicket()
        {
            var concessions = db.Concessions.ToList();
            var concession = new Concession();

            var vm = new AddTicketViewModel
            {
                Concessions = concessions,
                Concession = concession
            };
            return RedirectToAction("CustomerReservations", new { confirmSuccess = true, vm });

        }

        public ActionResult CustomerReservations()
        {
            var stations = db.Stations.GroupBy(c => c.City).Select(g => g.FirstOrDefault()).ToList();
            Station station = new Station();
            IEnumerable<Reservation> reservations = null;
            var userId = User.Identity.GetUserId();

            reservations = db.Reservations.Where(o => o.UserId == userId).ToList();
            foreach (var reservation in reservations)
            {
                var tickets = db.Tickets.Where(t => (t.ReservationId == reservation.ReservationId)).ToList();
                foreach (var ticket in tickets)
                {
                    var line = db.Lines.Where(t => t.LineId == ticket.LineId).ToList();
                    foreach (var line1 in line)
                    {
                    var track = db.Tracks.Where(t => t.TrackId == line1.TrackId).ToList();
                        foreach (var track1 in track)
                        {
                            var startStation =
                                db.Stations.Where(t => t.StationId == track1.StartStationId).ToList();
                            var endStation = db.Stations.Where(t => t.StationId == track1.EndStationId).ToList();
                        }
                    }

                }
            }
            var vm = new CustomerReservationsViewModel()
            {
                Stations = stations,
                Station = station,
                Reservations = reservations,


            };
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddTicket(AddTicketViewModel model, int? lineId)
        {
            if (Request.IsAuthenticated)
            {

                if (lineId != null)
                {
                    Ticket ticket = new Ticket();
                    ticket.SeatsAmt = model.Ticket.SeatsAmt;
                    ticket.TicketId = model.Ticket.LineId;
                    ticket.ConcessionId = model.Ticket.ConcessionId;
                    ticket.LineId = lineId.Value;
                    var tracks = db.Tracks.ToList();
                    var lines = db.Lines.ToList();
                    var abc = lines.First(c => c.LineId == lineId).TrackId;

                    var normalPrice = tracks.First(b => b.TrackId == abc).NormalPrice;
                    var concessions = db.Concessions.ToList();
                    var concessionValue = concessions.First(c => c.ConcessionId == ticket.ConcessionId).Discount;
                    ticket.Price = (normalPrice - (concessionValue*normalPrice/100))*ticket.SeatsAmt;

                    IEnumerable<Reservation> userReservations;

                    var userId = User.Identity.GetUserId();
                    userReservations = db.Reservations.Where(o => o.UserId == userId).ToList();
                    bool exist = false;
                    Reservation rez = null;
                    foreach (var uR in userReservations)
                    {
                     //   List<Ticket> a = uR.Tickets.Where(t => t.LineId == lineId).ToList();
                        List<Ticket> a =
                            db.Tickets.Where(t => ((t.LineId == lineId) && (t.ReservationId == uR.ReservationId)))
                                .ToList();
                        foreach (var uRTicket in a)
                        {
                            if (uRTicket.LineId == lineId)
                            {
                                exist = true;
                                rez = uR;
                            }
                            if (exist) break;
                        }
                        if(exist) break;
                    }
                    if (exist)
                    {
                        rez.Tickets.Add(ticket);
                        rez.ReservationValue += ticket.Price;
                    }
                    else
                    {
                        var tmpTickets = new List<Ticket>();
                        tmpTickets.Add(ticket);
                        decimal reservationValue = ticket.Price;
                        rez = new Reservation()
                        {
                            Created = DateTime.Now,
                            IsPayed = false,
                            UserId = userId,
                            ReservationValue = reservationValue,
                            Tickets = tmpTickets,
                            
                        };
                    }
                    db.Reservations.AddOrUpdate(rez);
                    db.SaveChanges();

                }

            }
            else
            {
                
            }
            return RedirectToAction("AddTicket", new { confirmSuccess = true });

        }

        public ActionResult Pay(int reservationid)
        {
            var res = db.Reservations.First(a => a.ReservationId == reservationid);
            if (res != null) res.IsPayed = true;
            db.Reservations.AddOrUpdate(res);
            db.SaveChanges();
            return RedirectToAction("CustomerReservations", new { confirmSuccess = true });

        }
    }
}