using NuorisoTaloKortti.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;

namespace NuorisoTaloKortti.Controllers
{
    public class HuoltajatController : Controller
    {
        // GET: Huoltajat
        NuorisokorttiEntities db = new NuorisokorttiEntities();
        public ActionResult Index()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                List<Huoltajat> model = db.Huoltajat.ToList();
                MessageBox.Show(model.ToString());
                return View(model);

            }
            return RedirectToAction("Loginikkuna", "Home");
        }

        public ActionResult Edit(int? id)
        {
            MessageBox.Show(id.ToString());
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Huoltajat huoltaja = db.Huoltajat.Find(id);
            if (huoltaja == null) return HttpNotFound();
            ViewBag.Postinro = new SelectList(db.Postitoimipaikat, "Postinumero", "Postinumero", huoltaja.Postinumero);

            return View(huoltaja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HuoltajaID, Etunimi, Sukunimi, Puhelinnumero, Osoite, Postinumero")] Huoltajat huoltaja)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(huoltaja).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(huoltaja);
            
        }
    }
}