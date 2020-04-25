using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qwer.Models
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool BlockedStatus { get; set; }

        public User()
        {
            RegistrationDate = DateTime.Now;
            LastLoginDate = DateTime.Now;
            BlockedStatus = false;
        }
    }
}
