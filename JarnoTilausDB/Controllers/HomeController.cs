using JarnoTilausDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JarnoTilausDB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        TilausDBEntities4 db = new TilausDBEntities4();
        public ActionResult Index()
        {
            
                return View();
            
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult TuotteidenNaytto()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Tuotteet> model = db.Tuotteet.ToList();
                db.Dispose();
                return View(model);
            }
            
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }

    }
}