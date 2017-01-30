using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace PolTrain.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public int StartStationId { get; set; }
        public int EndStationId { get; set; }
        public string TrackName { get; set; }
        public int Length { get; set; }
        public int SeatsAmt { get; set; }
        public bool Diner { get; set; }
        public decimal NormalPrice { get; set; }
        public TimeSpan JourneyTime { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public virtual Station StartStation { get; set; }
        public virtual Station EndStation { get; set; }

    }

    public enum ClassLevel
    {
        [Display(Name = "1. klasa")]
        FirstClass,

        [Display(Name = "2. klasa")]
        SecondClass
    }
}
