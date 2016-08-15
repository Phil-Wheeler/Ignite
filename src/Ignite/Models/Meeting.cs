using System;
using System.Collections.Generic;

namespace Ignite.Models
{
    public class Meeting
    {
        public Meeting()
        {
            Attendances = new List<Attendance>();
        }



        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}