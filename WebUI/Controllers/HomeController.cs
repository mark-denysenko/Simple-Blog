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
        //[HandleError(View = "DefaultError")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DefaultError()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }
    }
}