using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            byte hour = (byte)DateTime.Now.Hour;
            if (hour>=18)
            {
                ViewBag.Greeting = "Tere õhtust";
                ViewBag.Hour = "aeg3.jpg";
            }
            else if (hour>=14)
            {
                ViewBag.Greeting = "Tere päevast";
                ViewBag.Hour = "aeg2.jpg";
            }
            else if (hour>=6)
            {
                ViewBag.Greeting = "Tere hommikust";
                ViewBag.Hour = "aeg1.jpg";
            }
            else
            {
                ViewBag.Greeting = "Head ööd";
                ViewBag.Hour = "aeg4.jpeg";
            }
            return View();
        }

        #region "Guest"
        [HttpGet]
        public ActionResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                return View("Thanks", guest);
            }
            else
                return View();
        }

        private void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl= true;
                WebMail.UserName = "timur.denisenko.work@gmail.com";
                WebMail.Password="duto ahun xrzh hjsq";
                WebMail.From = "timur.denisenko.work@gmail.com";
                WebMail.Send("timur.denisenko.work@gmail.com", "Vastus kutsele ", guest.Name+"vastas "+((guest?.WillAttend ?? false) ? "tuleb peole" : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!";
            }
        }

        public ActionResult Kutse()
        {
            byte hour = (byte)DateTime.Now.Hour;
            if (hour>=18)
                ViewBag.Greeting = "Tere õhtust";
            else if (hour>=12)
                ViewBag.Greeting = "Tere päevast";
            else if (hour>=6)
                ViewBag.Greeting = "Tere hommikust";
            else
                ViewBag.Greeting = "Head ööd";
            byte month = (byte)DateTime.Now.Month;
            ViewBag.Message = "Ootan sind puhkusele - ";
            switch (month)
            {
                case 12: ViewBag.Message+="uus aasta! 1 Jaanuar"; break;
                case 1: ViewBag.Message+="iseseisvuspäev! 24 Veebruar"; break;
                case 2: ViewBag.Message+="naistepäev! 8 Märts"; break;
                case 3: ViewBag.Message+="narripäev! 1 Aprill"; break;
                case 4: ViewBag.Message+="kevadpäev! 1 Mai"; break;
                case 5: ViewBag.Message+="ivani päev! 24 Juuni"; break;
                case 6: ViewBag.Message+="alice Imedemaal päev! 4 Juuli"; break;
                case 7: ViewBag.Message+="iseseisvuse taastamise päev! 20 August"; break;
                case 8: ViewBag.Message+="teadmiste päev 1 September"; break;
                case 9: ViewBag.Message+="kohalike omavalitsuste päev! 1 Oktoober"; break;
                case 10: ViewBag.Message+="isadepäev! 10 November"; break;
                case 11: ViewBag.Message+="jõulud! 24-26 Detsembrid"; break;
            }
            ViewBag.Month = "p"+month+".jpg";

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
        GuestContext db = new GuestContext();
        [Authorize(Roles = "User,Admin")]
        public ActionResult Guests()
        {
            ViewBag.Will = null;
            IEnumerable<Guest> guest = db.Guests;
            return View(guest);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g==null)
                return HttpNotFound();
            return View(g);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
                return HttpNotFound();
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
                return HttpNotFound();
            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            ViewBag.Will = true;
            return View("Guests", guests);
        }
        [HttpGet]
        public ActionResult Reject()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == false);
            ViewBag.Will = false;
            return View("Guests", guests);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
                return HttpNotFound();
            return View(g);
        }
        #endregion

        #region "Holiday"

        [Authorize(Roles = "User,Admin")]
        public ActionResult Holidays()
        {
            IEnumerable<Holiday> holidays = db.Holidays;
            return View(holidays);
        }
        [HttpGet]
        public ActionResult hCreate() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult hCreate(Holiday holiday)
        {
            db.Holidays.Add(holiday);
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpGet]
        public ActionResult hDelete(int id) 
        {
            Holiday h = db.Holidays.Find(id);
            if (h==null)
                return HttpNotFound();
            return View(h);
        }
        [HttpPost,ActionName("hDelete")]
        public ActionResult hDeleteConfirmed(int id)
        {
            Holiday h = db.Holidays.Find(id);
            if (h==null)
                return HttpNotFound();
            db.Holidays.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpGet]
        public ActionResult hEdit(int? id)
        {
            Holiday h = db.Holidays.Find(id);
            if (h==null)
                return HttpNotFound();
            return View(h);
        }
        [HttpPost, ActionName("hEdit")]
        public ActionResult hEditConfirmed(Holiday holiday)
        {
            db.Entry(holiday).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        #endregion
    }
}