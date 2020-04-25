using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qwer.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool BlockedStatus { get; set; }
    }
}
