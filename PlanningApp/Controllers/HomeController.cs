using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanningApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The MBC Planning Application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Marshall (Building Contractors)Ltd";

            return View();
        }
    }
}