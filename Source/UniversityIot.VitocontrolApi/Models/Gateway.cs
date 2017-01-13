using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityIot.VitocontrolApi.Models
{
    public class Gateway
    {
        public Gateway()
        {
            Status = GatewayStatus.NotRegistered;
        }

        public long SerialNmb { get; set; }
        public GatewayStatus Status { get; set; }
        public User User { get; set; }
    }

    public enum GatewayStatus
    {
        NotRegistered = 0,
        Registered = 1
    }
}