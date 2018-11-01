using Core;
using EFProvider; // need only for transform -> (UnitOfWork)IUnitOfWork
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.BlogModels;

namespace WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IUnitOfWork uow;
        private const int POST_PER_PAGE = 5;

        public BlogController(IUnitOfWork repo)
        {
            uow = repo;
        }

        public ActionResult AllPosts(int page = 1)
        {
            PostListViewModel model = new PostListViewModel
            {
                Posts = GetPosts()
                .OrderByDescending(p => p.Date)
                .Skip((page - 1) * POST_PER_PAGE)
                .Take(POST_PER_PAGE)
                .ToList(),
                PagingInfo = new PostPagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = POST_PER_PAGE,
                    TotalItems = uow.Posts.GetAll().Count()
                }
            };

            return View(model);
        }

        [Authorize]
        public ActionResult UserPosts(int page = 1)
        {
            PostListViewModel model = new PostListViewModel
            {
                Posts = GetPosts(User.Identity.Name)
                .OrderByDescending(p => p.Date)
                .Skip((page - 1) * POST_PER_PAGE)
                .Take(POST_PER_PAGE)
                .ToList(),
                PagingInfo = new PostPagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = POST_PER_PAGE,
                    TotalItems = GetPosts().Where(p => p.Author == User.Identity.Name).Count()
                }
            };

            return View(model);
        }

        private IEnumerable<PostModel> GetPosts(string userName = null)
        {
            EFUnitOfWork db = uow as EFUnitOfWork;

            var posts = db.PostsDB
                .Include("User.Comments")
                .Where(p => userName == null ? true : p.User.Nickname == userName)
                .Select(p =>
                new PostModel
                {
                    Id = p.PostId,
                    Author = p.User.Nickname,
                    Title = p.Title,
                    Body = p.Body,
                    Date = p.Date,
                    Comments = p.Comments
                    .Select(c =>
                    new CommentModel
                    {
                        Author = c.User.Nickname,
                        Body = c.Body,
                        Date = c.Date
                    }).ToList()
                }
                );

            return posts;
        }

        public ActionResult PostInfo(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("AllPosts");

            PostModel post = GetPosts().Single(p => p.Id == id);

            return View(post);
        }

        [Authorize]
        public ActionResult MakePost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakePost(PostModel model)
        {
            if(ModelState.IsValid)
            {
                Post newPost = new Post
                {
                    Title = model.Title,
                    Body = model.Body,
                    Date = DateTime.Now,
                    User = uow.Users.GetAll().Single(u => u.Nickname == User.Identity.Name)
                };

                uow.Posts.Create(newPost);
                uow.Posts.Save();
                return RedirectToAction("PostInfo", new { id = newPost.PostId });
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakeComment(CommentModel model)
        {
            Post post = uow.Posts.Get(model.PostId);
            Comment comment = new Comment
            {
                Body = model.Body,
                Date = DateTime.Now,
                Post = post,
                User = uow.Users.GetAll().Single(u => u.Nickname == User.Identity.Name)
            };

            uow.Comments.Create(comment);
            uow.Comments.Save();

            return RedirectToAction("PostInfo", new { id = model.PostId });
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            string postAuthor = GetPosts().Single(p => p.Id == id).Author;

            if(postAuthor == User.Identity.Name)
            {
                uow.Posts.Delete(id);
                uow.Posts.Save();
                return RedirectToAction("AllPosts", "Blog");
            }

            return View("PostInfo", GetPosts().Single(p => p.Id == id));
        }
    }
}