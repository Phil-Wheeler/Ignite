using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ignite.ViewModels.Manage
{
    public class IndexViewModel : ChangePasswordViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }



        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Current password")]
        //public string OldPassword { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "New password")]
        //public string NewPassword { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm new password")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

    }
}
