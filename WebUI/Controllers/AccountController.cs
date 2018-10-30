﻿using Core;
using Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Models.AccountModels;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUnitOfWork uow;

        public AccountController(IUnitOfWork repo)
        {
            this.uow = repo;
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
                string passwordHash = new HasherPassword().GetHash(model.Password);
                uow.Users.GetAll();
                User user = uow.Users.GetAll()
                    .FirstOrDefault(u => u.Nickname == model.Nickname && u.PasswordHash == passwordHash);

                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Nickname, true);
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
                User user = uow.Users.GetAll()
                    .FirstOrDefault(u => u.Nickname == model.Nickname || u.Email == model.Email);

                if (user == null)
                {
                    string passwordHash = new HasherPassword().GetHash(model.Password);
                    uow.Users.Create(new User { Nickname = model.Nickname, Email = model.Email, PasswordHash = passwordHash });
                    uow.Users.Save();

                    user = uow.Users.GetAll()
                        .FirstOrDefault(u => u.Nickname == model.Nickname && u.PasswordHash == passwordHash);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Nickname, true);
                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User with this login or email is exist");
                }
            }

            return View(model);
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
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        // ????
        //protected override void Dispose(bool disposing)
        //{
        //    uow.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}