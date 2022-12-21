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
        NuorisokorttiEntities db = new NuorisokorttiEntities();
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
            List<Nuoret> model = db.Nuoret.ToList();
            Nuoret nuoret = db.Nuoret.Find(id);
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False" && nuoret.Aktivointi.ToString() == "True")
                {
                if (id == null)
                {
                    return HttpNotFound();
                }

                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            
            List<Nuoret> model = db.Nuoret.ToList();
            Nuoret nuoret = db.Nuoret.Find(id);
            if (Session["Kayttajanimi"] != null && Session["Yllapito"].ToString() == "False" && nuoret.Aktivointi.ToString() == "True")
            {

                if (ModelState.IsValid && image1 != null && nuoret.Aktivointi.ToString() == "True" )
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
            List<Kayttajat> model2 = db.Kayttajat.ToList();
            foreach (var mode in model2)
            {
                if (nuoret.Kayttajanimi.ToString() == mode.Kayttajanimi)
                {
                    return View (mode);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult PsswordChange([Bind(Include = "KayttajaId,Kayttajanimi, Salasana, ErrorMessage,uusiSalasana,ToistaSalasana")] Kayttajat kayttajat)
        {
            List<Kayttajat> model = db.Kayttajat.ToList();
            Kayttajat kay  = db.Kayttajat.Find(kayttajat.KayttajaId);

            if (kayttajat.uusiSalasana.Length < 8 )
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
            if(kayttajat.Salasana == null || kay.Salasana != kayttajat.Salasana) 
            {
                kayttajat.LoginErrorMessage = "Vanha salasana puuttu tai väärin kirjoitettu";
                return View(kayttajat);
            }
            db.Entry(kayttajat).State = EntityState.Modified;
            kayttajat.Salasana = kayttajat.ToistaSalasana;
            db.SaveChanges();
            kayttajat.LoginErrorMessage = "Salasana Vaihdettu";
            return View(kayttajat);
        }
    }
}
