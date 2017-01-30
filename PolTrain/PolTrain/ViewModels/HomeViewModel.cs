using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using PolTrain.Models;

namespace PolTrain.ViewModels
{
    public class HomeViewModel
    {
        public class IndexViewModel
        {
            public IEnumerable<SelectListItem> Tracks { get; set; }
            public Line Line { get; set; }
            public Station Station { get; set; }
            public IEnumerable<Station> Stations { get; set; }
            public string StartCity { get; set; }
            public string EndCity { get; set; }
            public DateTime Arrival { get; set; }

            public IEnumerable<Reservation> Reservations { get; set; }

            [Required(ErrorMessage = "Musisz wprowadzić e-mail")]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Musisz wprowadzić hasło")]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [Display(Name = "Zapamiętaj mnie")]
            public bool RememberMe { get; set; }
        }

        public class FindLinesViewModel
        {
            public string StartCity { get; set; }
            public string EndCity { get; set; }
            public DateTime Arrival { get; set; }

        }

    }
    }
