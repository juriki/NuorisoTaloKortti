using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;
using NuorisoTaloKortti.Models;
using System.Net;
// List Generator

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

                List<Nuoret> model = db.Nuoret.ToList();


                foreach (var item in model)
                {
                    if (item.Kayttajanimi.ToString() == Session["Kayttajanimi"].ToString())
                    {
                        var newlist = model.Where(x => x.Kayttajanimi.Contains(Session["Kayttajanimi"].ToString()));
                        return View(newlist);
                    }
                }
                return RedirectToAction("Loginikkuna", "Home");
            }
            else
            {
                return RedirectToAction("Loginikkuna", "Home");
            }
        }

        public ActionResult View()
        {
            // Tarkistetan Onko joku kirjautunut.  Session["Yllapito"].ToString() Tarkista onko oikeuskia  muokka tietoja.
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
            {
                NuorisokorttiEntities db = new NuorisokorttiEntities();

                List<Nuoret> model = db.Nuoret.ToList();


                foreach (var item in model)
                {
                    if (item.Kayttajanimi.ToString() == Session["Kayttajanimi"].ToString())
                    {
                        var newlist = model.Where(x => x.Kayttajanimi.Contains(Session["Kayttajanimi"].ToString()));
                        return View(newlist);
                    }
                }
                return RedirectToAction("Loginikkuna", "Home");
            }
            else
            {
                return RedirectToAction("Loginikkuna", "Home");
            }
        }



        public ActionResult Edit(int? id)
        {

            NuorisokorttiEntities db = new NuorisokorttiEntities();
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Nuoret nuoret = db.Nuoret.Find(id);
            if (nuoret == null) return HttpNotFound();
            return View(nuoret);
        }
        NuorisokorttiEntities db = new NuorisokorttiEntities();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Etunimi,Sukunimi,Puhelinnumero")] Nuoret nuoret)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nuoret).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        return View(nuoret);    
        }
    }
}