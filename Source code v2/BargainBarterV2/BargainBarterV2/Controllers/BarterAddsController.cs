using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BargainBarterV2.Models;

namespace BargainBarterV2.Controllers
{
    public class BarterAddsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BarterAdds
        public ActionResult Index()
        {
            return View(db.BarterAdds.ToList());
        }

        // GET: BarterAdds/Details/5
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

        // GET: BarterAdds/Create
        public ActionResult Create()
        {
            BarterAdd add1=new BarterAdd();
            return View(add1);
        }

        // POST: BarterAdds/Create
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

        // GET: BarterAdds/Edit/5
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

        // POST: BarterAdds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarterAddId,Titel,Description,Category")] BarterAdd barterAdd, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
                {
                    if (image1 != null && image1.ContentLength > 0)
                    {
                        var picture = new File
                        {
                            FileName = System.IO.Path.GetFileName(image1.FileName);
                            ContentType = image1.ContentType;
                        };
                        using (var reader = new System.IO.BinaryReader(image1.InputStream))
                        {
                            picture.Content = reader.ReadBytes(image1.ContentLength);
                        }

                        barterAdd.Files = new List<File> {picture};

                    }
                    //db.Entry(barterAdd).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return View("Index");

        }

        // GET: BarterAdds/Delete/5
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

        // POST: BarterAdds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            db.BarterAdds.Remove(barterAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
