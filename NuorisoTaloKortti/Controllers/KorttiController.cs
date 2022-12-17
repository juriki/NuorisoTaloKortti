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
            if (id == null)
            {
                return HttpNotFound();
            }

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Nuoret nuoret = db.Nuoret.Find(id);
            if (nuoret != null) 
            {
                SelectList huoltaja = new SelectList(db.Huoltajat, "HuoltajaId","Huoltaja", nuoret.Huoltaja);
                ViewBag.Huoltajat = huoltaja;   
                return View(nuoret);
            }
            return View(model);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nuoret nuoret, byte[] kuva)
        {
            if (ModelState.IsValid)
            { 
                db.Entry(nuoret).State = EntityState.Modified;
                db.SaveChanges();
                return View(nuoret);
            }
            return RedirectToAction("Index");
        }
    }
}
/*
 
         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NuoriId,Etunimi,Sukunimi,SyntymaAika,Puhelinnumero,Osoite,Postinumero,Huoltaja,SPosti,Allergiat,Kuvauslupa,Aktivointi,Kuva,Kayttajanimi")] Nuoret nuoret)
        {
            NuorisokorttiEntities db = new NuorisokorttiEntities();
   

            if (ModelState.IsValid)
            {
                int coun = Request.Files.Count;
                var file = Request.Files[0];

                string filename = file.FileName;
                MessageBox.Show(filename.ToString());
                byte[] buffer = new byte[file.InputStream.Length];
                file.InputStream.Read(buffer, 0, (int)file.InputStream.Length);

                db.Entry(nuoret).State = EntityState.Modified;
                nuoret.Kuva = buffer;
                //nuoret.Kuva = filename;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(nuoret);
        }
 

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NuorisokorttiEntities model,HttpPostedFileBase kuva1) 
        { 
            var db1 = new NuorisokorttiEntities();
   

            if (kuva1 == null)
            {
                model.Nuoret = new byte[kuva1.ContentLength];
                kuva1.InputStream.Read(model.Nuoret,0 kuva1.);
            }
            return View();
        }
    }
}




*/






/*



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NuorisokorttiEntities model,HttpPostedFileBase kuva1) 
        { 
            var db1 = new NuorisokorttiEntities();
   

            if (kuva1 == null)
            {
                model.k   
            }
            return View();
        }
 */