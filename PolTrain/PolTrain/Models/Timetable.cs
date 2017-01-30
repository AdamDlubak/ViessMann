using System;
using System.Collections.Generic;
using Glimpse.Core.ClientScript;

namespace PolTrain.Models
{
    public class Timetable
    {
        public int TimetableId { get; set; }
        public int TrackId { get; set; }
        public DateTime Data { get; set; }
        public int SeatsQuantity  { get; set; }

        public List<Line> Lines { get; set; }
    }
}