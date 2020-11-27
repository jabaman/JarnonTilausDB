using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JarnoTilausDB.Models;

namespace JarnoTilausDB.Controllers
{
    public class TilauksetController : Controller
    {
        TilausDBEntities4 db = new TilausDBEntities4();
        // GET: Tilaukset
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {

                List<Tilaukset> model = db.Tilaukset.ToList();
                db.Dispose();
                return View(model);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset pp = db.Tilaukset.Find(id);
            if (pp == null) return HttpNotFound();
            return View(pp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,postinumero,Tilauspvm,Toimituspvm")] Tilaukset pp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pp);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,postinumero,Tilauspvm,Toimituspvm")] Tilaukset pp)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(pp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pp);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset pp = db.Tilaukset.Find(id);
            if (pp == null) return HttpNotFound();
            return View(pp);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset pp = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(pp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}