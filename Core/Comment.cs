using System;

namespace Core
{
    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime Date { get; set; }

        public string Body { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
