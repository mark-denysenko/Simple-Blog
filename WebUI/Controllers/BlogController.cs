﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using BusinessLayer.BusinessModelsDTO;
using BusinessLayer.Interfaces;
using WebUI.Models.BlogModels;
using WebUI.Util;
using WebUI.Validation.Filters;

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

        [HandleError(View = "IncorrectPageError")]
        [ValidatePageParameter]
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
        [ValidatePageParameter]
        [HandleError(View = "IncorrectPageError")]
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

        [OutputCache(Duration = 15, Location = OutputCacheLocation.Server, VaryByParam = "id")]
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