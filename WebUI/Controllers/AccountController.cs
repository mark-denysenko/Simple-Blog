using Core;
using Interfaces;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.BusinessModelsDTO;
using BusinessLayer.Interfaces;
using WebUI.Models.AccountModels;
using WebUI.Util;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.Login(model.Nickname, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Nickname, true);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "No user with password and login like this");
                }
            }

            return View(model);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.Register(model.Nickname, model.Password, model.Email))
                {
                    FormsAuthentication.SetAuthCookie(model.Nickname, true);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "User with this login or email is exist");
                }
            }

            return View(model);
        }

        [Authorize]
        public new ActionResult Profile()
        {
            UserProfile profile = _accountService.GetUserProfile(User.Identity.Name);

            return View(profile);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (returnUrl != null && Url.IsLocalUrl(returnUrl))
            {
                //return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SetUserPhoto()
        {
            HttpPostedFileBase file = HttpContext.Request.Files["UserAvatar"];
            if (file?.ContentLength > 0)
            {
                string avatarName = User.Identity.Name + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(MyConfiguration.USER_AVATAR_PATH), avatarName);
                file.SaveAs(path);
            }

            return RedirectToAction("Profile");
        }

        public ActionResult ListOfUsers()
        {
            return View(_accountService.GetAllNicknames());
        }

        public JsonResult JsonUserList()
        {
            return Json(_accountService.GetAllNicknames(), JsonRequestBehavior.AllowGet);
        }
    }
}