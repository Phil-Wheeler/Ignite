using System;

namespace Ignite.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Posted { get; set; }
        public int Parent { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public string Commenter { get; set; }

        public virtual Attendance Meeting { get; set; }
    }
}