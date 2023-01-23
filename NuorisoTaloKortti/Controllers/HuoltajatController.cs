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
        NuorisokorttiEntities2 db = new NuorisokorttiEntities2();
        public ActionResult Index()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                List<Huoltajat> model = db.Huoltajat.ToList();
                //MessageBox.Show(model.ToString());
                return View(model);

            }
            return RedirectToAction("Loginikkuna", "Home");
        }

        public ActionResult Create()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                var post = db.Postitoimipaikat;
                IEnumerable<SelectListItem> selectPostList = from p in post
                                                             select new SelectListItem
                                                             {
                                                                 Value = p.Postinumero,
                                                                 Text = p.Postinumero + " " + p.Postitoimipaikka
                                                             };

                ViewBag.Postinumero = new SelectList(selectPostList, "Value", "Text");

                return View();
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Etunimi, Sukunimi, Puhelinnumero, Osoite, Postinumero")] Huoltajat huoltaja)
        {
            if (!ModelState.IsValid) 
            {
                var post = db.Postitoimipaikat;
                IEnumerable<SelectListItem> selectPostList = from p in post
                                                             select new SelectListItem
                                                             {
                                                                 Value = p.Postinumero,
                                                                 Text = p.Postinumero + " " + p.Postitoimipaikka
                                                             };

                ViewBag.Postinumero = new SelectList(selectPostList, "Value", "Text");

                return View();
            }
                if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (ModelState.IsValid)
                {
                    db.Huoltajat.Add(huoltaja);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(huoltaja);

            }
           
            return RedirectToAction("Loginikkuna", "Home");

        }

        public ActionResult Edit(int? id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Huoltajat huoltaja = db.Huoltajat.Find(id);
                if (huoltaja == null) return HttpNotFound();
                var post = db.Postitoimipaikat;
                IEnumerable<SelectListItem> selectPostList = from p in post
                                                             select new SelectListItem
                                                             {
                                                                 Value = p.Postinumero,
                                                                 Text = p.Postinumero + " " + p.Postitoimipaikka
                                                             };

                ViewBag.Postinumero = new SelectList(selectPostList, "Value", "Text", huoltaja.Postinumero);

                return View(huoltaja);
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HuoltajaID, Etunimi, Sukunimi, Puhelinnumero, Osoite, Postinumero")] Huoltajat huoltaja)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {

                if (ModelState.IsValid)
                {
                    db.Entry(huoltaja).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                var post = db.Postitoimipaikat;
                IEnumerable<SelectListItem> selectPostList = from p in post
                                                             select new SelectListItem
                                                             {
                                                                 Value = p.Postinumero,
                                                                 Text = p.Postinumero + " " + p.Postitoimipaikka
                                                             };

                ViewBag.Postinumero = new SelectList(selectPostList, "Value", "Text", huoltaja.Postinumero);

                return View(huoltaja);
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        public ActionResult Delete(int? id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Huoltajat huoltaja = db.Huoltajat.Find(id);
                if (huoltaja == null) return HttpNotFound();
                return View(huoltaja);
            }
            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                try
                {
                    Huoltajat huoltaja = db.Huoltajat.Find(id);
                    db.Huoltajat.Remove(huoltaja);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    
                    MessageBox.Show("Huoltajan poistaminen on kieltettyä! Ennen kuin kaikki huolessa olevat lapset ovat poistettuja järjestelmästä");
                    return RedirectToAction("Index");

                }

            }
            return RedirectToAction("Loginikkuna", "Home");

        }


    }
}