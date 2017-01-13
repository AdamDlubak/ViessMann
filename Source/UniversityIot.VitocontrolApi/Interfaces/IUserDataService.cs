using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Interfaces
{
    public interface IUserDataService
    {
        User GetUser(string userName);
        void AddUser(User user);
        void DeleteUser(string userName);
    }
}
