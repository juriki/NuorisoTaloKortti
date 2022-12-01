using NuorisoTaloKortti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NuorisoTaloKortti.Controllers
{
    public class NuoretController : Controller
    {
        // GET: Nuoret
        NuorisokorttiEntities db = new NuorisokorttiEntities();
        public ActionResult Index()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                List<Nuoret> model = db.Nuoret.ToList();
                return View(model);
            }
            return RedirectToAction("Loginikkuna", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Nuoret nuori = db.Nuoret.Find(id);
            if (nuori == null) return HttpNotFound();

            return View(nuori);
        }
    }
}