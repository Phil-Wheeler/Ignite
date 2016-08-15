using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ignite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Twitter { get; set; }
        public string Web { get; set; }
        public string Bio { get; set; }
    }
}
