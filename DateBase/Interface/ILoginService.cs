using DateBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBase.Interface
{
    public interface ILoginService
    {
        public User Login(string username, string password);
        public bool SingUp(string username, string password);
    }
}
