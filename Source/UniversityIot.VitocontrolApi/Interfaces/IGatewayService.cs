using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Interfaces
{
    public interface IGatewayService
    {
        Gateway RegisterGateway(User user, long serialNmb);
        bool UnregisterGateway(User user, Gateway gateway);
    }
}
