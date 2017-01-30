using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolTrain.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool IsPayed { get; set; }
        public DateTime Created { get; set; }
        public List<Ticket> Tickets { get; set; }
        public decimal ReservationValue { get; set; }


    }
}