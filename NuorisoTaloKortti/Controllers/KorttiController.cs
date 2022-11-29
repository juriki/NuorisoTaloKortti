using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NuorisoTaloKortti.Models;
// List Generator
using System.Collections.Generic;

namespace NuorisoTaloKortti.Controllers
{
    public class KorttiController : Controller
    {
        // GET: Kortti
        public ActionResult Index()
        {
            // Tarkistetan Onko joku kirjautunut.  Session["Yllapito"].ToString() Tarkista onko oikeuskia  muokka tietoja.
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
            {
                NuorisokorttiEntities1 db = new NuorisokorttiEntities1();

                List<Nuoret> model = db.Nuoret.ToList();
                foreach (var item in model)
                {
                    if (item.Kayttajanimi.ToString() == Session["Kayttajanimi"].ToString())
                    {
                        return View(model);
                    }
                }
                return View();
            }

            else
            {
                return RedirectToAction("Loginikkuna", "Home");
            }
        }
    }
}