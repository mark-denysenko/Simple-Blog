using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.BusinessModelsDTO;

namespace BusinessLayer.Interfaces
{
    public interface IBlogService
    {
        // return id post or whole post maybe
        void MakePost(PostDTO post);
        void DeletePost(int id);
        void DeletePost(PostDTO post);
        void MakeComment(CommentDTO comment);
        void DeleteComment(int id);
        void DeleteComment(CommentDTO comment);
        PostDTO GetPost(int id);
        // parameters for pagination ??
        IEnumerable<PostDTO> GetPosts();
        IEnumerable<PostDTO> GetPosts(int countPerPage, int page);
        IEnumerable<PostDTO> GetUserPosts(string nickname);
        IEnumerable<PostDTO> GetUserPosts(string nickname, int countPerPage, int page);
    }
}
