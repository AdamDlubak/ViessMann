using Moq;
using UniversityIot.VitocontrolApi.Interfaces;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;
using Xunit;

namespace UniversityIot.VitoControlApi.Tests
{
    
    public class GatewayServiceTest
    {
        [Fact]
        public void ShouldCreateGatewayWithNewSerialNumberIfItDoesNotExistYet()
        {
            //Arrange
            var user = new User("Adam");
            long serialNmb = 4342532329557824;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);

            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);

            //Assert
            Assert.Equal(gateway.SerialNmb, serialNmb);
        }

        [Fact]
        public void ShouldConnectUserWithGateway()
        {
            //Arrange
            var user = new User("Adam");
            long serialNmb = 4342532329557824;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);

            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);


            //Assert
            Assert.Equal(gateway.User, user);
        }

        [Fact]
        public void ShouldSetGatewayStatusToRegistered()
        {
            //Arrange
            var user = new User("Adam");
            long serialNmb = 4342532329557824;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);

            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);

            //Assert
            Assert.Equal(gateway.Status, GatewayStatus.Registered);
        }

        [Fact]
        public void ShouldNotAllowToRegisterTheSameGatewayTwice()
        {
            //Arrange
            var user = new User("Adam");
            long serialNmb = 4342532329557824;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);

            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);

            //Assert
            Assert.Equal(gateway.Status, GatewayStatus.Registered);
        }

        [Fact]
        public void ShouldOnlyAllowToRegisterGatewaysWithSerialNumberContainingExactly16Digits()
        {
            
        }
    }
}