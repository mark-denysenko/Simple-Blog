using System;
using System.Collections.Generic;

namespace Core
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public string Body { get; set; }

        public User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
