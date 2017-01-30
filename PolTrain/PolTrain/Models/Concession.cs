using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolTrain.Models
{
    public class Concession
    {
        public int ConcessionId { get; set; }
        public int Discount { get; set; }
        public string Label { get; set; }
    }
}