using System;
using Moq;
using UniversityIot.VitocontrolApi.Interfaces;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;
using Xunit;

namespace UniversityIot.VitoControlApi.Tests
{
    public class UserServiceTest
    {

        //----------------Get User
        [Fact]
        public void ShouldReturnUserByNameWhenUserExists()
        {
            //Arrange
            var userName = "Adam";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s.GetUser(userName)).Returns(new User(userName));

            var userService = new UserService(dataServiceMock.Object);

            //Act
            var user = userService.GetUser(userName);

            //Assert
            Assert.Equal(userName, user.Name);
        }

        [Fact]
        public void ShouldFailWhenUserDoesNotExists()
        {
            //Arrange
            var userName = "Adam";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s.GetUser(userName)).Throws<UserNotFoundException>();

            var userService = new UserService(dataServiceMock.Object);

            //Act      
            Exception ex = Assert.Throws<UserNotFoundException>(() => userService.GetUser(userName));

            //Assert
            Assert.Equal("User does not exists", ex.Message);
        }







        //-------------CreateUser
        [Fact]
        public void ShouldNotAllowCreateUserWithExistingName()
        {
            //Arrange
            string userName = "Adam";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s.AddUser(It.Is<User>(u => u.Name == userName))).Throws<UserAlreadyExistsException>();

            var userService = new UserService(dataServiceMock.Object);

            //Act      
            Exception ex = Assert.Throws<UserAlreadyExistsException>(() => userService.CreateUser(userName));

            //Assert
            Assert.Equal("User already exists", ex.Message);
        }
        [Fact]
        public void ShouldCreateUser()
        {
            string userName = "Konstanty";
            //Arrange
            var dataServiceMock = new Mock<IUserDataService>();

            var userService = new UserService(dataServiceMock.Object);

            //Act
            var newUser = userService.CreateUser(userName);
            //Assert
            Assert.Equal(newUser.Name, userName);

        }



        



        //---------------DeleteUser
        [Fact]
        public void ShouldDeleteUserWhenUserExists()
        {
            //Arrange
            string userName = "Adam";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s.DeleteUser(userName));
            var userService = new UserService(dataServiceMock.Object);

            //Act      
            bool success = userService.DeleteUser(userName);

            //Assert
            Assert.Equal(success, true);
        }

        [Fact]
        public void ShouldFailWhenDeletingUserDoesNotExists()
        {
            //Arrange
            string userName = "Adam";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s
                .DeleteUser(userName))
                .Throws<UserNotFoundException>();
            var userService = new UserService(dataServiceMock.Object);

            //Act      
            Exception ex = Assert.Throws<UserNotFoundException>(() => userService.DeleteUser(userName));

            //Assert
            Assert.Equal("User does not exists", ex.Message);
        }
    }
}