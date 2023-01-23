using NuorisoTaloKortti.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace NuorisoTaloKortti.Controllers
{
    public class NuoretController : Controller
    {
        // GET: Nuoret
        NuorisokorttiEntities2 db = new NuorisokorttiEntities2();
        public ActionResult Index()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                List<Nuoret> model = db.Nuoret.ToList();
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

                var huoltajat = db.Huoltajat;
                IEnumerable<SelectListItem> selectHuoltajaList = from h in huoltajat
                                                                 select new SelectListItem
                                                                 {
                                                                     Value = h.HuoltajaId.ToString(),
                                                                     Text = "(ID: " + h.HuoltajaId.ToString() + ") " + h.Etunimi + " " + h.Sukunimi
                                                                 };

                ViewBag.Huoltaja = new SelectList(selectHuoltajaList, "Value", "Text");

                return View();
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Etunimi, Sukunimi, SyntymaAika, Puhelinnumero, Osoite, Postinumero, Huoltaja, SPosti, Allergiat, Kuvauslupa, Aktivointi, Kuva, Kayttajanimi")] Nuoret nuori)
        {
            var salasana = "38D0EC0B2A7AB61A8AA11FA145D68EDA";
            var username = (nuori.Etunimi.ToString() + nuori.Sukunimi.ToString()).ToLower();
            nuori.Kayttajanimi = username;

            Kayttajat kayttajat = new Kayttajat();

            kayttajat.Kayttajanimi = username;
            kayttajat.Salasana = salasana;
            kayttajat.uusiSalasana = salasana;
            kayttajat.ToistaSalasana = salasana;

            //        MessageBox.Show();

            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (ModelState.IsValid)
                {
                    var usernamcount = 1;
                    var lodstatus = false;
                    while (!lodstatus)
                    {
                        db.Kayttajat.Add(kayttajat);
                        db.Nuoret.Add(nuori);
                        try
                        {
                            db.SaveChanges();
                            lodstatus = true;
                        }
                        catch (Exception)
                        {
                            kayttajat.Kayttajanimi = username + usernamcount.ToString();
                            nuori.Kayttajanimi = username + usernamcount.ToString();
                            usernamcount++;
                        }
                    }

                    return RedirectToAction("Index");
                }
                return View(nuori);

            }

            return RedirectToAction("Loginikkuna", "Home");

        }


        public ActionResult Edit(int? id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Nuoret nuori = db.Nuoret.Find(id);
                if (nuori == null) return HttpNotFound();
                var post = db.Postitoimipaikat;
                IEnumerable<SelectListItem> selectPostList = from p in post
                                                             select new SelectListItem
                                                             {
                                                                 Value = p.Postinumero,
                                                                 Text = p.Postinumero + " " + p.Postitoimipaikka
                                                             };

                ViewBag.Postinumero = new SelectList(selectPostList, "Value", "Text", nuori.Postinumero);

                var huoltajat = db.Huoltajat;
                IEnumerable<SelectListItem> selectHuoltajaList = from h in huoltajat
                                                                 select new SelectListItem
                                                                 {
                                                                     Value = h.HuoltajaId.ToString(),
                                                                     Text = "(ID: " + h.HuoltajaId.ToString() + ") " + h.Etunimi + " " + h.Sukunimi
                                                                 };

                ViewBag.Huoltaja = new SelectList(selectHuoltajaList, "Value", "Text", nuori.Huoltaja);

                return View(nuori);
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NuoriId, Etunimi, Sukunimi, SyntymaAika, Puhelinnumero, Osoite, Postinumero, Huoltaja, SPosti, Allergiat, Kuvauslupa, Aktivointi, Kuva, Kayttajanimi")] Nuoret nuori)
        {
 
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {

                if (ModelState.IsValid)
                {
                    db.Entry(nuori).State = EntityState.Modified;
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

                ViewBag.Postinumero = new SelectList(selectPostList, "Value", "Text", nuori.Postinumero);

                var huoltajat = db.Huoltajat;
                IEnumerable<SelectListItem> selectHuoltajaList = from h in huoltajat
                                                                 select new SelectListItem
                                                                 {
                                                                     Value = h.HuoltajaId.ToString(),
                                                                     Text = h.Etunimi + " " + h.Sukunimi
                                                                 };

                ViewBag.Huoltaja = new SelectList(selectHuoltajaList, "Value", "Text", nuori.Huoltaja);

                return View(nuori);
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        public ActionResult Delete(int? id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Nuoret nuori = db.Nuoret.Find(id);
                if (nuori == null) return HttpNotFound();
                return View(nuori);
            }
            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                Nuoret nuori = db.Nuoret.Find(id);
                db.Nuoret.Remove(nuori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Loginikkuna", "Home");

        }
    }
}