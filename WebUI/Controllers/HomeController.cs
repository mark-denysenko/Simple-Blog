using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork uow;

        public HomeController(IUnitOfWork repo)
        {
            uow = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}