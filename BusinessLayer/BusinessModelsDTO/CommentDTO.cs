using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModelsDTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public int PostId { get; set; }
    }
}
