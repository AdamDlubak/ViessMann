using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityIot.VitocontrolApi.Models
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() {}
        public UserNotFoundException(string message) : base(message) { }
    }

    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base() { }
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}