using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.BlogModels
{
    public class CommentModel
    {
        public string Author { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }
    }
}