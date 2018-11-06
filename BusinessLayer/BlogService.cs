using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLayer.Interfaces;
using Interfaces;
using AutoMapper;
using BusinessLayer.BusinessModelsDTO;
using Core;

namespace BusinessLayer
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _uow;

        public BlogService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void DeleteComment(int id)
        {
            _uow.Comments.Delete(id);
        }

        public void DeleteComment(CommentDTO comment)
        {
            DeleteComment(comment.PostId);
        }

        public void DeletePost(int id)
        {
            _uow.Posts.Delete(id);
        }

        public void DeletePost(PostDTO post)
        {
            DeletePost(post.PostId);
        }

        public PostDTO GetPost(int id)
        {
            Post post = _uow.Posts.GetAll().Include(p => p.User).FirstOrDefault(p => p.PostId == id);

            if (post == null)
                return null;

            var postDto = Mapper.Map<Post, PostDTO>(post);
            postDto.Comments = _uow.Comments.GetAll()
                .Include(c => c.User)
                .Where(c => c.Post.PostId == post.PostId)
                .AsEnumerable()
                .Select(Mapper.Map<Comment, CommentDTO>)
                .OrderByDescending(c => c.Date);

            // it was like this
            //var postDto = new PostDTO
            //{
            //    PostId = post.PostId,
            //    Author = post.User.Nickname,
            //    Title = post.Title,
            //    Body = post.Body,
            //    Date = post.Date,
            //    Comments = _uow.Comments
            //        .GetAll()
            //        .Include(c => c.User)
            //        .Where(c => c.Post.PostId == id)
            //        .Select(c =>                        //Mapper.Map<CommentDTO>(c))
            //        new CommentDTO
            //        {
            //            CommentId = c.CommentId,
            //            Author = c.User.Nickname,
            //            Body = c.Body,
            //            Date = c.Date,
            //            PostId = post.PostId
            //        })
            //        .OrderByDescending(c => c.Date)
            //};

            return postDto;
        }

        public IEnumerable<PostDTO> GetPosts()
        {
            var posts = Mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_uow.Posts.GetAll().Include(p => p.User).ToList());

            // it was like this
            //IEnumerable<PostDTO> posts = _uow.Posts.GetAll()
            //    .Select(p =>
            //        new PostDTO
            //        {
            //            PostId = p.PostId,
            //            Author = p.User.Nickname,
            //            Title = p.Title,
            //            Body = p.Body,
            //            Date = p.Date,
            //            Comments = p.Comments
            //                .Select(c =>
            //                    new CommentDTO
            //                    {
            //                        CommentId = c.CommentId,
            //                        Body = c.Body,
            //                        Date = c.Date,
            //                        Author = p.User.Nickname,
            //                        PostId = p.PostId
            //                    })
            //        });

            return posts.OrderByDescending(p => p.Date);
        }

        public IEnumerable<PostDTO> GetPosts(int countPerPage, int page)
        {
            return GetPosts().Skip((page - 1) * countPerPage).Take(countPerPage);
        }

        public IEnumerable<PostDTO> GetUserPosts(string nickname)
        {
            if(nickname == null)
                throw new ArgumentException("Empty nickname. Cannot load user posts");

            return GetPosts().Where(p => p.Author == nickname);
        }

        public IEnumerable<PostDTO> GetUserPosts(string nickname, int countPerPage, int page)
        {
            return GetUserPosts(nickname).Skip((page - 1) * countPerPage).Take(countPerPage);
        }

        public void MakeComment(CommentDTO comment)
        {
            Post post = _uow.Posts.Get(comment.PostId);
            User user = _uow.Users.GetAll().Single(u => u.Nickname == comment.Author);

            if(comment == null || post == null || user == null)
                throw new ArgumentException("Comment, user or post doesn't exist! Cannot add comment!");

            var newComment = new Comment
            {
                Body = comment.Body,
                Date = DateTime.UtcNow,
                Post = post,
                User = user
            };

            _uow.Comments.Create(newComment);
            _uow.Comments.Save();

            // for using id in next code
            comment.CommentId = newComment.CommentId;
        }

        public void MakePost(PostDTO post)
        {
            var newPost = new Post
            {
                Title = post.Title,
                Body = post.Body,
                Date = DateTime.UtcNow,
                User = _uow.Users.GetAll().Single(u => u.Nickname == post.Author)
            };

            _uow.Posts.Create(newPost);
            _uow.Posts.Save();

            // for using id in next code
            post.PostId = newPost.PostId;
        }
    }
}
