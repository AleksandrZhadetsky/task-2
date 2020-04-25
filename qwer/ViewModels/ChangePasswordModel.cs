using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qwer.ViewModels
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
