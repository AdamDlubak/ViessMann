using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolTrain.Models
{
    public class Line
    {
        public int LineId { get; set; }
        public int TrackId { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int ReservedSeatsAmt { get; set; }
        public virtual Track Track { get; set; }
    }
}