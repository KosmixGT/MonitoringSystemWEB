using CoursePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursePractice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(LoginModel lm)
        {
            return View(lm);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}