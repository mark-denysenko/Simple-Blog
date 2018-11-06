using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModelsDTO
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
