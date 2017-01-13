using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityIot.VitocontrolApi.Interfaces;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IGatewayDataService _gatewayDataService;

        public GatewayService(IGatewayDataService gatewayDataService)
        {
            _gatewayDataService = gatewayDataService;
        }

        public Gateway RegisterGateway(User user, long serialNmb)
        {
            if (!_gatewayDataService.DoesGatewayExist(serialNmb))
            {
                var gateway = new Gateway
                {
                    User = user,
                    SerialNmb = serialNmb,
                    Status = GatewayStatus.Registered
                };

                _gatewayDataService.SaveGateway(gateway);
                return gateway;
            }
            return null;
        }

        public bool UnregisterGateway(User user, Gateway gateway)
        {
            throw new NotImplementedException();
        }
    }
}