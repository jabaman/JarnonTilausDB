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
    public class AsiakkaatController : Controller
    {
        // GET: Asiakkaat
        TilausDBEntities4 db = new TilausDBEntities4();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {

                List<Asiakkaat> model = db.Asiakkaat.ToList();
                db.Dispose();
                return View(model);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakas = db.Asiakkaat.Find(id);
            if (asiakas == null) return HttpNotFound();
            return View(asiakas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakas);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Nimi,Postinumero")] Asiakkaat asiakas)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakas);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat shippers = db.Asiakkaat.Find(id);
            if (shippers == null) return HttpNotFound();
            return View(shippers);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat shippers = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(shippers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}