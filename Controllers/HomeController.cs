using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Ankeet()
        {
            return View();
        }

        public ActionResult Kutse()
        {
            ViewBag.Greeting = DateTime.Now.Hour<10 ? "Tere hommikust" : "Tere paevast";
            ViewBag.Message = "Ootan sind!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}