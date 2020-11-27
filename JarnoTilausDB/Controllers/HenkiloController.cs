using JarnoTilausDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JarnoTilausDB.Controllers
{
    
    public class HenkiloController : Controller
    {
        TilausDBEntities4 db = new TilausDBEntities4();
        // GET: Henkilo
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {

                List<Henkilot> model = db.Henkilot.ToList();
                db.Dispose();
                return View(model);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Henkilot asiakas = db.Henkilot.Find(id);
            if (asiakas == null) return HttpNotFound();
            return View(asiakas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite")] Henkilot henkilo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilo);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite")] Henkilot henkilo)
        {
            if (ModelState.IsValid)
            {
                db.Henkilot.Add(henkilo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Henkilot henkilo = db.Henkilot.Find(id);
            if (henkilo== null) return HttpNotFound();
            return View(henkilo);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Henkilot shippers = db.Henkilot.Find(id);
            db.Henkilot.Remove(shippers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}