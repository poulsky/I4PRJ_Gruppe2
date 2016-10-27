using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BargainBarterV2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BargainBarterV2.Controllers
{
    public class BarterAdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BarterAds - Show all BarterAds
        public ActionResult Index()
        {
            return View(db.BarterAdds.ToList());
        }
        

        // GET: BarterAds for a specific User
        public ActionResult UserList(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<BarterAdd> barterAds =new List<BarterAdd>();

            foreach (var ad in db.BarterAdds)
            {
                if (ad.ApplicationUser.Id == userId)
                    barterAds.Add(ad);

            }
            return View(barterAds.ToList());
        }


        


        public ActionResult ViewPhoto(int id)
        {
            var photo = db.BarterAdds.Find(id).Thumbnail;
            if (photo!=null)
            {
                return File(photo, "image/jpeg");
            }
            return File("~/Content/img/no-photo.jpg", "image/jpeg");
            
        }

        // GET: BarterAds/Details/5
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

        // GET: BarterAds/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte [] array = ms.GetBuffer();
                }
            }
            return RedirectToAction("Create", "BarterAdsController");
        }

        // POST: BarterAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarterAddId,Titel,Description,Picture,Category")] BarterAdd barterAdd, HttpPostedFileBase BarterPicture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (BarterPicture.ContentLength > 0)
                    {
                        using (BinaryReader reader = new BinaryReader(BarterPicture.InputStream))
                        {
                            barterAdd.Picture = reader.ReadBytes((int) BarterPicture.InputStream.Length);
                            barterAdd.Thumbnail = Helperfunctions.Helper.MakeThumbnail(barterAdd.Picture, 320, 150);
                        }

                    }
                }
                catch
                {
                    return HttpNotFound();
                }

                    ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    ApplicationUser User= db.Users.Find(user.Id);
                    User.BarterAdds.Add(barterAdd);
                    db.SaveChanges();
                
            }

            return RedirectToAction("Index", "BarterAds");//new { id = db.BarterAdds.Last().ApplicationUser.Id }
        }

        // GET: BarterAds/Edit/5
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

            byte[] imagedata = db.BarterAdds.Find(id).Picture;
            if (imagedata != null)
            {
                string imagepath = Convert.ToBase64String(imagedata);
                string imagedataURL = string.Format("data:image/png; base64, {0}", imagepath);
                ViewBag.image = imagedataURL;
            }

            return View();
        }

        // POST: BarterAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarterAddId,Titel,Description,Picture,Category")] BarterAdd barterAdd, HttpPostedFileBase BarterPicture)
        {
            if (ModelState.IsValid)
            {
                if (BarterPicture.ContentLength > 0)
                {
                    using (BinaryReader reader = new BinaryReader(BarterPicture.InputStream))
                    {
                        barterAdd.Picture = reader.ReadBytes((int)BarterPicture.InputStream.Length);
                    }

                }

                db.Entry(barterAdd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: BarterAds/Delete/5
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

        // POST: BarterAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            db.BarterAdds.Remove(barterAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ShowBarterAd(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarterAdd CurrentAd = db.BarterAdds.Find(id);

            if (CurrentAd == null)
            {
                return HttpNotFound();
            }
            ViewData["Titel"] = CurrentAd.Titel;
            ViewData["Description"] = CurrentAd.Description;
            ViewBag.Id = CurrentAd.BarterAddId;
            ViewData["Category"] = CurrentAd.Category;
            

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult ManageAds()
        {
            var userId = User.Identity.GetUserId();

            var barterAds = from r in db.BarterAdds
                            where r.ApplicationUserId == userId
                            select r;

            if(barterAds.Any())
                return View(barterAds.ToList());
            return View("ManageAdsNoAds");
        }
    }
}


