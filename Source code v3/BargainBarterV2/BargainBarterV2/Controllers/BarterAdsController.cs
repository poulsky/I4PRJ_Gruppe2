using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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


        public ActionResult ShowBarterAdsOnMap()
        {
            return View(db.BarterAdds.ToList());
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
           
            ApplicationUser tempuser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ApplicationUser user = db.Users.Where(M => M.Id == barterAdd.ApplicationUserId).Include("Address").FirstOrDefault();
            ApplicationUser LogUser = db.Users.Where(M => M.Id == barterAdd.ApplicationUserId).Include("Address").FirstOrDefault();


            if (user!=null)
            {
                double distance=user.Address.Coordinate.DistanceTo(LogUser.Address.Coordinate);
                ViewData["Distance"] = distance;
            }

            ViewData["Longitude"] = LogUser.Address.Coordinate.Longitude;
            ViewData["Latitude"] = LogUser.Address.Coordinate.Latitude;


            return View(barterAdd);
        }

        // GET: BarterAds/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
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

            return RedirectToAction("ManageAds", "BarterAds");//new { id = db.BarterAdds.Last().ApplicationUser.Id }
        }

        // GET: BarterAds/Edit/5
        [Authorize]
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

            //byte[] imagedata = db.BarterAdds.Find(id).Picture;
            //if (imagedata != null)
            //{
            //    string imagepath = Convert.ToBase64String(imagedata);
            //    string imagedataURL = string.Format("data:image/png; base64, {0}", imagepath);
            //    ViewBag.image = imagedataURL;
            //}

            return View(barterAdd);
        }

        // POST: BarterAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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
                        barterAdd.Thumbnail = Helperfunctions.Helper.MakeThumbnail(barterAdd.Picture, 320, 150);
                    }

                }

                //db.Entry(barterAdd).State = EntityState.Modified;

                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                ApplicationUser user1 =
                    db.Users.Include(b => b.BarterAdds).Single(u => u.Id == user.Id);

                var temp = user1.BarterAdds.Single(b => b.BarterAddId == barterAdd.BarterAddId);

                temp.Description = barterAdd.Description;
                temp.Titel = barterAdd.Titel;
                temp.Category = barterAdd.Category;
                temp.Picture = barterAdd.Picture;
                temp.Thumbnail = barterAdd.Thumbnail;

                db.Users.AddOrUpdate(user1);
                
                db.SaveChanges();
                return RedirectToAction("ManageAds");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Comment(int? id , string commentstring)
        {
            if (id == null || commentstring == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = new Comment()
            {
                CommentText = commentstring,
                ApplicationUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId())
                //System.Web.HttpContext.Current.GetOwinContext()
                //    .GetUserManager<ApplicationUserManager>()
                //    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId())
            };

            BarterAdd ad2 = db.BarterAdds.Find(id);
            ad2.Comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Details", "BarterAds", new { id = id});
        }

        // GET: BarterAds/Delete/5
        [Authorize]
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
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BarterAdd barterAdd = db.BarterAdds.Find(id);
            db.BarterAdds.Remove(barterAdd);
            db.SaveChanges();
            return RedirectToAction("ManageAds");
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


