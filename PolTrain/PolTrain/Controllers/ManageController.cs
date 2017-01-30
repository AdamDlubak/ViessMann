using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Poltrain.Models;
using PolTrain.DAL;
using PolTrain.Models;
using PolTrain.ViewModels;
using static PolTrain.ViewModels.ManageViewModel;

namespace PolTrain.Controllers
{
    public class ManageController : Controller
    {
        private PolTrainContext db = new PolTrainContext();

        // GET: Lines
        public ActionResult ShowStations()
        {
            var stations = db.Stations.ToList();

            var vm = new ShowStationsViewModel()
            {
                Stations = stations
            };
            return View(vm);
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                HasPassword = this.HasPassword(),
                CurrentLogins = userLogins,
                OtherLogins = otherLogins,
                ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1,
                UserData = user.UserData
            };

            return View(model);
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public ActionResult ShowTracks()
        {
            var tracks = db.Tracks.ToList();
            var stations = db.Stations.ToList();

            var vm = new ShowTracksViewModel()
            {
                Tracks = tracks,
                Stations = stations
            };
            return View(vm);
        }

        public ActionResult ShowLines()
        {
            var lines = db.Lines.ToList();
            var stations = db.Stations.ToList();

            var vm = new ShowLinesViewModel()
            {
                Stations = stations,
                Lines = lines
            };
            return View(vm);
        }






        [Authorize(Roles = "Admin")]
        public ActionResult AddOrEditStation(int? stationId, bool? confirmSuccess)
        {


            var result = new AddOrEditStationViewModel();
            Station station;
            if (!stationId.HasValue)
            {
                station = new Station();
                result.EditMode = false;
            }
            else
            {
                station = db.Stations.Find(stationId);
                result.EditMode = true;
            }
            result.Station = station;
            result.ConfirmSuccess = confirmSuccess;

            return View(result);
        }

        public ActionResult DeleteStation(int stationId)
        {
            Station station = db.Stations.First(s => s.StationId == stationId);
            db.Stations.Remove(station);
            db.SaveChanges();
            return RedirectToAction("ShowStations", new {confirmSuccess = true});

        }

        public ActionResult DeleteTrack(int trackId)
        {
            Track track = db.Tracks.First(s => s.TrackId == trackId);
            db.Tracks.Remove(track);
            db.SaveChanges();
            return RedirectToAction("ShowTracks", new {confirmSuccess = true});

        }

        public ActionResult DeleteLine(int lineId)
        {
            Line line = db.Lines.First(s => s.LineId == lineId);
            db.Lines.Remove(line);
            db.SaveChanges();
            return RedirectToAction("ShowLines", new {confirmSuccess = true});

        }

        [HttpPost]
        public ActionResult AddOrEditStation(AddOrEditStationViewModel model)
        {
            if (model.Station.StationId > 0)
            {
                // Saving existing entry
                db.Entry(model.Station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddOrEditStation", new {confirmSuccess = true});
            }
            else
            {
                // Creating new entry
                db.Entry(model.Station).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("AddOrEditStation", new {confirmSuccess = true});
            }
        }


        public ActionResult AddOrEditTrack(int? trackId, bool? confirmSuccess)
        {


            var result = new AddOrEditTrackViewModel();
            Track track;
            if (!trackId.HasValue)
            {
                track = new Track();
                result.EditMode = false;
            }
            else
            {
                track = db.Tracks.Find(trackId);
                result.EditMode = true;
            }
            var stations = db.Stations.ToList();
            result.Stations = stations;
            result.Track = track;
            result.ConfirmSuccess = confirmSuccess;

            return View(result);
        }

        [HttpPost]
        public ActionResult AddOrEditTrack(AddOrEditTrackViewModel model)
        {
            if (model.Track.TrackId > 0)
            {
                // Saving existing entry
                db.Entry(model.Track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddOrEditTrack", new {confirmSuccess = true});
            }
            else
            {
                // Creating new entry
                db.Entry(model.Track).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("AddOrEditTrack", new {confirmSuccess = true});
            }
        }



        public ActionResult AddOrEditLine(int? lineId, bool? confirmSuccess)
        {
            var result = new AddOrEditLineViewModel();
            Line line;
            if (!lineId.HasValue)
            {
                line = new Line();
                result.EditMode = false;
            }
            else
            {
                line = db.Lines.Find(lineId);
                result.EditMode = true;
            }
            var tracks = db.Tracks.ToList();
            var stations = db.Stations.ToList();

            result.Line = line;
            result.ConfirmSuccess = confirmSuccess;

            result.Tracks = tracks.Select(a => new SelectListItem()
            {
                Text =
                    stations.First(b => b.StationId == a.StartStationId).Name + " - " +
                    stations.First(b => b.StationId == a.EndStationId).Name,
                Value = a.TrackId.ToString()
            });

            return View(result);
        }

        [HttpPost]
        public ActionResult AddOrEditLine(AddOrEditLineViewModel model)
        {
            if (model.Line.LineId > 0)
            {
                // Saving existing entry
                db.Entry(model.Line).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddOrEditLine", new {confirmSuccess = true});
            }
            else
            {
                // Creating new entry
                db.Entry(model.Line).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("AddOrEditLine", new {confirmSuccess = true});
            }
        }

    public class ManageCredentialsViewModel
        {
            public bool HasPassword { get; set; }
            public SetPasswordViewModel SetPasswordViewModel { get; set; }
            public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
            public PolTrain.Controllers.ManageController.ManageMessageId? Message { get; set; }
            public IList<UserLoginInfo> CurrentLogins { get; set; }
            public IList<AuthenticationDescription> OtherLogins { get; set; }
            public bool ShowRemoveButton { get; set; }

            public UserData UserData { get; set; }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            LinkSuccess,
            Error
        }

        public class SetPasswordViewModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [System.Web.Mvc.Compare("NewPassword",
                 ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class ChangePasswordViewModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [System.Web.Mvc.Compare("NewPassword",
                 ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }




    }
}