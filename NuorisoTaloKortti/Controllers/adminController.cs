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

    public class adminController : Controller
    {
        NuorisokorttiEntities2 db = new NuorisokorttiEntities2();

        // GET: admin
        public ActionResult Index()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                List<Kayttajat> model = db.Kayttajat.ToList();
                return View(model);
            }
            return RedirectToAction("Loginikkuna", "Home");

        }



        public ActionResult AdminTable()
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                List<Kayttajat> model = db.Kayttajat.ToList();
                return View(model);
            }
            return RedirectToAction("Loginikkuna", "Home");
        }

        public ActionResult DeleteAdmin(int? id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Kayttajat kayttaja = db.Kayttajat.Find(id);
                if (kayttaja == null) return HttpNotFound();
                return View(kayttaja);
            }
            return RedirectToAction("Loginikkuna", "Home");

        }




        public ActionResult CreateAdmin()
        {

            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                return View();
            }

            return RedirectToAction("Loginikkuna", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin([Bind(Include = "Kayttajanimi, Salasana, ToistaSalasana, Yllapito")] Kayttajat kayttajat)
        {
            var hahmot = db.Kayttajat;

            if (!ModelState.IsValid)
            {
                if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
                {
                    if (kayttajat.ToistaSalasana == null || kayttajat.Salasana.ToString() != kayttajat.ToistaSalasana.ToString())
                    {
                        kayttajat.LoginErrorMessage = "Salasanat eivät Täsmä";
                        return View(kayttajat);
                    }
                    if (kayttajat.Salasana.Length  < 8) 
                    {
                        kayttajat.LoginErrorMessage = "Salasana Pitä olla Vähintäin 8 merkkinen";
                        return View(kayttajat);
                    }
                    foreach (var hahmo in hahmot)
                    {
                        if (hahmo.Kayttajanimi.ToString() == kayttajat.Kayttajanimi)
                        {
                            kayttajat.LoginErrorMessage = "Käyttäjä : " + kayttajat.Kayttajanimi.ToString() + " on jo olemassa";
                            return View(kayttajat);
                        }
                    }
                    var password = new PasswordHash();
                    var hashpassword = password.EncodePassword(kayttajat.Salasana.ToString());
                    kayttajat.Salasana = hashpassword;
                    kayttajat.uusiSalasana = hashpassword;
                    kayttajat.ToistaSalasana = hashpassword;
                    kayttajat.Yllapito = true;
                    db.Kayttajat.Add(kayttajat);
                    db.SaveChanges();
                    return RedirectToAction("AdminTable");
                }

                return RedirectToAction("AdminTable");
            }
            return RedirectToAction("AdminTable");
        }


        public ActionResult Delete(int? id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Kayttajat kayttaja = db.Kayttajat.Find(id);
                if (kayttaja == null) return HttpNotFound();
                return View(kayttaja);
            }
            return RedirectToAction("Loginikkuna", "Home");

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "True")
            {
                Kayttajat kayttajat = db.Kayttajat.Find(id);
                db.Kayttajat.Remove(kayttajat);
                if (kayttajat.Kayttajanimi.ToString() == Session["Kayttajanimi"].ToString())
                {
                    MessageBox.Show("Et voi poista tiliä joka on tällä hetkellä käytössä");
                    return RedirectToAction("AdminTable");
                }
                db.SaveChanges();
                return RedirectToAction("AdminTable");
            }
            return RedirectToAction("AdminTable");

        }
    }



}