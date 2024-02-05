using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using System.Collections.ObjectModel;
using System.Xml.Linq;

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
        [Authorize]
        public ActionResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
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

        [Authorize]
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
                WebMail.Send("timur.denisenko.work@gmail.com", "Vastus kutsele ", guest.Name+" vastas "+((guest?.WillAttend ?? false) ? "tuleb peole" : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!";
            }
        }
        [Authorize]
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
            string message = string.Join(", ",db.Holidays.Select(holiday => holiday.Name));
            ViewBag.Message = "Registreeri inimene järgmisteks pühadeks: - " + message;

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
        [Authorize]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guest = db.Guests;
            return View(guest);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g==null)
                return HttpNotFound();
            return View(g);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
                return HttpNotFound();
            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        [Authorize]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            return View("Guests", guests);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Reject()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == false);
            return View("Guests", guests);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Details(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
                return HttpNotFound();
            return View(g);
        }
        #endregion

        #region "Holiday"

        [Authorize]
        public ActionResult Holidays()
        {
            IEnumerable<Holiday> holidays = db.Holidays;
            return View(holidays);
        }
        [HttpGet]
        [Authorize]
        public ActionResult hCreate() 
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult hCreate(Holiday holiday)
        {
            db.Holidays.Add(holiday);
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpGet]
        [Authorize]
        public ActionResult hDelete(int id) 
        {
            Holiday h = db.Holidays.Find(id);
            if (h==null)
                return HttpNotFound();
            return View(h);
        }
        [HttpPost,ActionName("hDelete")]
        [Authorize]
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
        [Authorize]
        public ActionResult hEdit(int? id)
        {
            Holiday h = db.Holidays.Find(id);
            if (h==null)
                return HttpNotFound();
            return View(h);
        }
        [HttpPost, ActionName("hEdit")]
        [Authorize]
        public ActionResult hEditConfirmed(Holiday holiday)
        {
            db.Entry(holiday).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpGet]
        [Authorize]
        public ActionResult hReg(int? id)
        {
            Holiday h = db.Holidays.Find(id);
            if (h == null)
                return HttpNotFound();
            return View(h);
        }
        [HttpPost, ActionName("hReg")]
        [Authorize]
        public ActionResult hRegConfirmed(Holiday holiday)
        {
            db.Entry(holiday).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpGet]
        [Authorize]
        public ActionResult hUnReg(int? id)
        {
            Holiday h = db.Holidays.Find(id);
            if (h == null)
                return HttpNotFound();
            return View(h);
        }
        [HttpPost, ActionName("hUnReg")]
        [Authorize]
        public ActionResult hUnRegConfirmed(Holiday holiday)
        {
            db.Entry(holiday).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpGet]
        [Authorize]
        public ActionResult hAccept()
        {
            string name = User.Identity.GetUserName();
            IEnumerable<Holiday> holidays = db.Holidays.Where(g => g.User == name);
            return View("Holidays", holidays);
        }
        [HttpGet]
        [Authorize]
        public ActionResult hReject()
        {
            string name = User.Identity.GetUserName();
            IEnumerable<Holiday> holidays = db.Holidays.Where(g => g.User != name);
            return View("Holidays", holidays);
        }
        [HttpGet]
        [Authorize]
        public ActionResult EpostLast(string Email)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "timur.denisenko.work@gmail.com";
                WebMail.Password = "duto ahun xrzh hjsq";
                WebMail.From = "timur.denisenko.work@gmail.com";
                WebMail.Send(Email, "Vastus kutsele ", "Meeldetuletus!");
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!";
            }
            IEnumerable<Guest> guests = db.Guests;
            return View("Guests", guests);
        }
        #endregion
    }
}