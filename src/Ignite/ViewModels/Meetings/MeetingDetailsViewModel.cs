using Ignite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ignite.ViewModels.Meetings
{
    public class MeetingDetailsViewModel
    {
        public MeetingDetailsViewModel(Meeting meeting)
        {
            Meeting = meeting;
            Attendees = new List<ApplicationUser>();
        }

        public Meeting Meeting { get; set; }

        public List<Attendance> Attendances { get; set; }

        public List<ApplicationUser> Attendees { get; set; }
    }
}
