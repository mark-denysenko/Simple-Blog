using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.BlogModels
{
    public class PostModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }

        public ICollection<CommentModel> Comments { get; set; }
    }
}