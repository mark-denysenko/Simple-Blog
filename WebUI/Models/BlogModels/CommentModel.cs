using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.BlogModels
{
    public class CommentModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}