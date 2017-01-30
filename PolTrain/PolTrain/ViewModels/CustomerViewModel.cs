using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PolTrain.Models;

namespace PolTrain.ViewModels
{
    public class CustomerViewModel
    {
        public class AddTicketViewModel
        {
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

            public IEnumerable<Concession> Concessions { get; set; }
            public Concession Concession { get; set; }
            public Line Line { get; set; }
            public IEnumerable<Line> Lines { get; set; }

            public Ticket Ticket { get; set; }

        }

        public class CustomerReservationsViewModel
        {
            public Station Station { get; set; }
            public IEnumerable<Station> Stations { get; set; }

            public IEnumerable<Reservation> Reservations { get; set; }
        }
    }
}