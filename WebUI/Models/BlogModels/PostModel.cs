using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.BlogModels
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public string Author { get; set; }

        public ICollection<CommentModel> Comments { get; set; }
    }
}