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
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Index(string searchstring)
        {
            var results = from m in db.BarterAdds select m;

            if (!String.IsNullOrEmpty(searchstring))
                results = results.Where(s => s.Titel.Contains(searchstring));
            return View("Frontpage", results.ToList());
        }

        public ActionResult CategorySearch(string searchstring)
        {
            var results = from m in db.BarterAdds
                          where (m.Category == searchstring)
                          select m;

            return View("Frontpage", results.ToList());
        }
        //// GET: Search/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BarterAdd barterAdd = db.BarterAdds.Find(id);
        //    if (barterAdd == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(barterAdd);
        //}

        //// GET: Search/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Search/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "BarterAddId,Titel,Description,Picture,Category")] BarterAdd barterAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BarterAdds.Add(barterAdd);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(barterAdd);
        //}

        //// GET: Search/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BarterAdd barterAdd = db.BarterAdds.Find(id);
        //    if (barterAdd == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(barterAdd);
        //}

        //// POST: Search/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "BarterAddId,Titel,Description,Picture,Category")] BarterAdd barterAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(barterAdd).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(barterAdd);
        //}

        //// GET: Search/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BarterAdd barterAdd = db.BarterAdds.Find(id);
        //    if (barterAdd == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(barterAdd);
        //}

        //// POST: Search/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    BarterAdd barterAdd = db.BarterAdds.Find(id);
        //    db.BarterAdds.Remove(barterAdd);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
