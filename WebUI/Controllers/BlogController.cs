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
            EFUnitOfWork db = uow as EFUnitOfWork;

            var posts = db.PostsDB
                .Include("User.Comments")
                .OrderByDescending(p => p.Date)
                .Skip(page - 1)
                .Take(POST_PER_PAGE)
                .Select(p =>
                new PostModel {
                    Author = p.User.Nickname,
                    Title = p.Title,
                    Body = p.Body,
                    Date = p.Date,
                    Comments = p.Comments
                    .Select(c => 
                    new CommentModel {
                        Author = c.User.Nickname,
                        Body = c.Body,
                        Date = c.Date}).ToList()
                    }
                );

            return View(posts.ToList());
        }

        //[Authorize]
        //public ActionResult UserPosts(int page = 1)
        //{

        //}

        //public ActionResult PostInfo(int id)
        //{

        //}

        //[Authorize]
        //public ActionResult MakePost()
        //{

        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult MakePost(PostModel newPost)
        //{

        //}
    }
}