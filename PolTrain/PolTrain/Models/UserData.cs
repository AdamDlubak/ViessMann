using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PolTrain.Models
{
    public class UserData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [RegularExpression(@"(\+\d{2})*[\d\s-]+",
            ErrorMessage = "Błędny format numeru telefonu.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }

    }
}