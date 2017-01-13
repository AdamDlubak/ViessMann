using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Interfaces
{
    public interface IGatewayDataService
    {
        bool DoesGatewayExist(long serialNmb);
        void SaveGateway(Gateway gateway);
    }
}
