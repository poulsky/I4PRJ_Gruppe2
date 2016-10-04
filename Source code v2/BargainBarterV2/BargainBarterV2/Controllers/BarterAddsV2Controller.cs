using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BargainBarterV2.Models;

namespace BargainBarterV2.Controllers
{
    public class BarterAddsV2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BarterAddsV2
        public ActionResult Index()
        {
            return View(db.BarterAdds.ToList());
        }

        // GET: BarterAddsV2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            if (barterAdd == null)
            {
                return HttpNotFound();
            }
            return View(barterAdd);
        }

        // GET: BarterAddsV2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BarterAddsV2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarterAddId,Titel,Description,Picture,Category")] BarterAdd barterAdd)
        {
            if (ModelState.IsValid)
            {
                db.BarterAdds.Add(barterAdd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(barterAdd);
        }

        // GET: BarterAddsV2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            if (barterAdd == null)
            {
                return HttpNotFound();
            }
            return View(barterAdd);
        }

        // POST: BarterAddsV2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarterAddId,Titel,Description,Picture,Category")] BarterAdd barterAdd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barterAdd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barterAdd);
        }

        // GET: BarterAddsV2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            if (barterAdd == null)
            {
                return HttpNotFound();
            }
            return View(barterAdd);
        }

        // POST: BarterAddsV2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            db.BarterAdds.Remove(barterAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
