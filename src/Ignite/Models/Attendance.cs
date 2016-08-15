using System;
using System.Collections.Generic;

namespace Ignite.Models
{
    public class Attendance
    {
        public Attendance()
        {
            Comments = new List<Comment>();
        }



        public int Id { get; set; }
        public Guid Attendee { get; set; }
        public bool WillAttend { get; set; }
        public bool DidAttend { get; set; }
        public int Rating { get; set; }


        public virtual Meeting Meeting { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}