using System;
using NuorisoTaloKortti.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NuorisoTaloKortti.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Kayttajanimi"] != null)
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else
            {
                return RedirectToAction("LoginIkkuna", "Home");
            }
        }

        public ActionResult About()
        {
          //  ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
          //  ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoginIkkuna()
        {
            if (Session["Kayttajanimi"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Authorize(Kayttajat kayttajat)
        {
            NuorisokorttiEntities db = new NuorisokorttiEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Kayttajat.SingleOrDefault(x => x.Kayttajanimi == kayttajat.Kayttajanimi && x.Salasana == kayttajat.Salasana);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["Kayttajanimi"] = LoggedUser.Kayttajanimi;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                //Kayttajat.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", kayttajat);
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