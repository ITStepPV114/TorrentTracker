using DateBase.Interface;
using DateBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace DateBase.Service
{
    public class LoginService : ILoginService
    {
        public User Login(string username, string password)
        {
            User check = null;

            using (StoreUserContext db = new StoreUserContext())
            {
                check = db.Users.Where(x => x.LoginUser == username && x.PaswwordUser == password).FirstOrDefault();

            }

            return check;
        }

        public bool SingUp(string username, string password)
        {
            using (StoreUserContext db = new StoreUserContext())
            {

                User newUser = new User() { LoginUser = username, PaswwordUser = password };

                db.Add(newUser);
                db.SaveChanges();

                return true;
            }
        }
    }
}
