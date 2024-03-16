using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDictionary.Models;

namespace WpfDictionary.Services
{
    public class AuthService
    {
        public static bool Login(string username, string password)
        {
            var users = DataService.Instance.Users;
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
