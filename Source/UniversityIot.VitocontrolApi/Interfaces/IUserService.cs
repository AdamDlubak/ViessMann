using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Interfaces
{
    public interface IUserService
    {
        User GetUser(string userName);
        User CreateUser(string userName);
        bool DeleteUser(string userName);
    }
}
