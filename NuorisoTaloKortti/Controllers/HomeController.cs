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
            // Tarkistetan Onko joku kirjautunut.  Session["Yllapito"].ToString() Tarkista onko oikeuskia  muokka tietoja.
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                ViewBag.LoggedStatus = "Out";
                return View();
            }
            else if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
            {
                return RedirectToAction("Index", "Kortti");
            }
            else 
            { 
                return RedirectToAction("Loginikkuna", "Home");
            } 
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult YllapitoNuoret()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                return View();
            }
            return RedirectToAction("Loginikkuna", "Home");
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
            nurisokorttiEntities1 db = new nurisokorttiEntities1();
            PasswordHash password = new PasswordHash();
            string passwordHash = password.EncodePassword(kayttajat.Salasana);
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Kayttajat.SingleOrDefault(x => x.Kayttajanimi == kayttajat.Kayttajanimi && x.Salasana == passwordHash);
            if (LoggedUser != null)
            {
                Session["Salasana"] = "false";
                if (LoggedUser.Salasana.ToString() == "38D0EC0B2A7AB61A8AA11FA145D68EDA")
                {
                    Session["Salasana"] = "true";
                }
                ViewBag.LoginMessage = "Successfull login";
                Session["Yllapito"] = LoggedUser.Yllapito;
                Session["KayttajaId"] = LoggedUser.KayttajaId;
                Session["Kayttajanimi"] = LoggedUser.Kayttajanimi;
                if (Session["Yllapito"].ToString() == "True")
                {
                    return RedirectToAction("YllapitoNuoret", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
                }
                else
                {
                    return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
                }
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                kayttajat.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
          //      return RedirectToAction("Index", "Home");
                return View("LoginIkkuna",kayttajat);
            }
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
        public ActionResult Oops()
        {
           return View();   
        }
    }


}

