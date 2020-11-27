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
    public class TilausrivitController : Controller
    {
        // GET: Tilausrivit
        TilausDBEntities4 db = new TilausDBEntities4();
        
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {

                List<Tilausrivit> model = db.Tilausrivit.ToList();
                db.Dispose();
                return View(model);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilausrivit pp = db.Tilausrivit.Find(id);
            if (pp == null) return HttpNotFound();
            return View(pp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit pp)
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
        public ActionResult Create([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit pp)
        {
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(pp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pp);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilausrivit pp = db.Tilausrivit.Find(id);
            if (pp == null) return HttpNotFound();
            return View(pp);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilausrivit pp = db.Tilausrivit.Find(id);
            db.Tilausrivit.Remove(pp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}