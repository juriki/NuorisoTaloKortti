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
using System.IO;
// List Generator

namespace NuorisoTaloKortti.Controllers
{
    public class KorttiController : Controller
    {
        NuorisokorttiEntities2 db = new NuorisokorttiEntities2();
        // GET: Kortti
        public ActionResult Index()
        {
            // Tarkistetan Onko joku kirjautunut.  Session["Yllapito"].ToString() Tarkista onko oikeuskia  muokka tietoja.
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
            {
                NuorisokorttiEntities2 db = new NuorisokorttiEntities2();

                List<Nuoret> model = db.Nuoret.ToList();

                foreach (var item in model)
                {
                    if (item.Kayttajanimi.ToString() == Session["Kayttajanimi"].ToString())
                    {
                        var newlist = model.Where(x => x.Kayttajanimi.Contains(Session["Kayttajanimi"].ToString()));
                        //var newlist2 = model.Where(x => x.Kayttajanimi.Contains(db.Postitoimipaikat.ToString()));
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

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public ActionResult View()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            // Tarkistetan Onko joku kirjautunut.  Session["Yllapito"].ToString() Tarkista onko oikeuskia  muokka tietoja.
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
            {
                NuorisokorttiEntities2 db = new NuorisokorttiEntities2();
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

            return RedirectToAction("Loginikkuna", "Home");

        }


        public ActionResult Edit(int? id)
        {
            List<Nuoret> model = db.Nuoret.ToList();
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
                {

                if (id == null)
                {
                    return RedirectToAction("oops", "Home");
                }

                if (id == null )
                {
                    return RedirectToAction("oops", "Home");
                }
                Nuoret nuoret = db.Nuoret.Find(id);
                if(nuoret.Kayttajanimi != Session["Kayttajanimi"].ToString())
                {
                    return RedirectToAction("oops", "Home");
                }
                if (nuoret != null) 
                {
                    SelectList huoltaja = new SelectList(db.Huoltajat, "HuoltajaId","Huoltaja", nuoret.Huoltaja);
                    ViewBag.Huoltajat = huoltaja;   
                    return View(nuoret);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Loginikkuna", "Home");
            }
        }


        

        [HttpPost]
        public ActionResult Edit(int? id, HttpPostedFileBase image1)
        {
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False")
            {
                    List<Nuoret> model = db.Nuoret.ToList();
                Nuoret nuoret = db.Nuoret.Find(id);
                if (ModelState.IsValid && image1 != null)
                {
                    byte[] filebyte = new byte[image1.ContentLength];
                    image1.InputStream.Read(filebyte, 0, image1.ContentLength);
                    nuoret.Kuva = filebyte;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
                }
            else
            {
                return RedirectToAction("Loginikkuna", "Home");
            }
        }


        public ActionResult PsswordChange(int? id)
        {
            List<Nuoret> model = db.Nuoret.ToList();
            Nuoret nuoret = db.Nuoret.Find(id);
            if(nuoret == null)
            { 
                return RedirectToAction("Loginikkuna", "Home");
            }
            List<Kayttajat> model2 = db.Kayttajat.ToList();
            foreach (var mode in model2)
            {

                if (nuoret.Kayttajanimi.ToString() == mode.Kayttajanimi && Session["Kayttajanimi"].ToString() == mode.Kayttajanimi.ToString())
                {
                    return View (mode);
                }
                          
            }
            return RedirectToAction("oops", "Home");
        }

        [HttpPost]
        public ActionResult PsswordChange([Bind(Include = "KayttajaId,Kayttajanimi, Salasana, ErrorMessage,uusiSalasana,ToistaSalasana")] Kayttajat kayttajat)
        {
            PasswordHash passw = new PasswordHash();
            string password;

            if (kayttajat.uusiSalasana == null || kayttajat.uusiSalasana.Length < 8)
            {
                kayttajat.LoginErrorMessage = "Salasana Pitä olla vähintäin 8 merkkinen";
                return View(kayttajat);
            }

            if (kayttajat.uusiSalasana != kayttajat.ToistaSalasana)
            {
                kayttajat.LoginErrorMessage = "Salasanat eivät täsmä";
                MessageBox.Show(kayttajat.uusiSalasana.ToString());
                return View(kayttajat);
            }
            if (kayttajat.Salasana == null)
            {
                kayttajat.LoginErrorMessage = "Vanha salasana puuttu tai väärin kirjoitettu";
                return View(kayttajat);
            }
            db.Entry(kayttajat).State = EntityState.Modified;
            password = passw.encodePassword(kayttajat.ToistaSalasana);
            kayttajat.Salasana = password;
            db.SaveChanges();
            kayttajat.LoginErrorMessage = "Salasana Vaihdettu";
            return View(kayttajat);
        }
    }
}
