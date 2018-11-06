using Core;
using EFProvider; // need only for transform -> (UnitOfWork)IUnitOfWork
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.BusinessModelsDTO;
using BusinessLayer.Interfaces;
using WebUI.Models.BlogModels;
using WebUI.Util;

namespace WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private const int POST_PER_PAGE = MyConfiguration.POST_PER_PAGE;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult AllPosts(int page = 1)
        {
            var model = new PostListViewModel
            {
                Posts = _blogService.GetPosts(POST_PER_PAGE, page),
                PagingInfo = new PostPagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = POST_PER_PAGE,
                    TotalItems = _blogService.GetPosts().Count()
                }
            };

            return View(model);
        }

        [Authorize]
        public ActionResult UserPosts(int page = 1)
        {
            var model = new PostListViewModel
            {
                Posts = _blogService.GetUserPosts(User.Identity.Name, POST_PER_PAGE, page),
                PagingInfo = new PostPagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = POST_PER_PAGE,
                    TotalItems = _blogService.GetUserPosts(User.Identity.Name).Count()
                }
            };

            return View(model);
        }

        public ActionResult PostInfo(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("AllPosts");

            PostDTO post = _blogService.GetPost(id.Value);

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
                PostDTO newPost = new PostDTO
                {
                    Title = model.Title,
                    Body = model.Body,
                    Date = DateTime.UtcNow,
                    Author = User.Identity.Name
                };

                _blogService.MakePost(newPost);

                return RedirectToAction("PostInfo", new { id = newPost.PostId });
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakeComment(CommentModel model)
        {
            PostDTO post = _blogService.GetPost(model.PostId);
            var comment = new CommentDTO
            {
                Body = model.Body,
                Date = DateTime.UtcNow,
                PostId = model.PostId,
                Author = User.Identity.Name
            };

            _blogService.MakeComment(comment);
            return RedirectToAction("PostInfo", new { id = model.PostId });
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            PostDTO postToDelete = _blogService.GetPost(id);

            if(postToDelete != null && postToDelete.Author == User.Identity.Name)
            {
                foreach(var comment in postToDelete.Comments)
                {
                    _blogService.DeleteComment(comment.CommentId);
                }
                _blogService.DeletePost(id);
                return RedirectToAction("AllPosts", "Blog");
            }

            return View("PostInfo", _blogService.GetPost(id));
        }
    }
}