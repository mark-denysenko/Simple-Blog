using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessModelsDTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }

        public IEnumerable<PostDTO> Posts { get; set; } = new List<PostDTO>();
        public IEnumerable<CommentDTO> TotalComments { get; set; } = new List<CommentDTO>();
    }
}
