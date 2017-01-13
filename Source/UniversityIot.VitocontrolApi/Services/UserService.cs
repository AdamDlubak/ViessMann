using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityIot.VitocontrolApi.Interfaces;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDataService _userDataService;
        
        public UserService(IUserDataService userDataService)
        {
            _userDataService = userDataService;           
        }

        public User GetUser(string userName)
        {
            try
            {
                return _userDataService.GetUser(userName);
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException("User does not exists");
            }            
        }

        public User CreateUser(string userName)
        {
            try
            {
                var user = new User(userName);
                _userDataService.AddUser(user);
                return user;
            }
            catch (UserAlreadyExistsException)
            {
                throw new UserAlreadyExistsException("User already exists");
            }
        }

        public bool DeleteUser(string userName)
        {
            try
            {
                _userDataService.DeleteUser(userName);
                return true;
            }
            catch (UserNotFoundException)
            {
                throw new UserNotFoundException("User does not exists");
            }
        }
    }
}