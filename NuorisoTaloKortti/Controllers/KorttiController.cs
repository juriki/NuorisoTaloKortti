using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NuorisoTaloKortti.Models;

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
                NuorisokorttiEntities db = new NuorisokorttiEntities();

                var nuoriIdo = Session["Kayttajanimi"].ToString();

                List<Nuoret> model = db.Nuoret.ToList();
                foreach (var item in model)
                {
                    var joku = item.Kayttajanimi;
                    if (joku.ToString() == nuoriIdo)
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