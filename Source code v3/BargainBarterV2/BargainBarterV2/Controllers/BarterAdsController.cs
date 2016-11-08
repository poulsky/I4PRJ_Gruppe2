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
        private UnitOfWork unitOfWork = new UnitOfWork();

        public BarterAdsController(ApplicationDbContext dbase, UnitOfWork unit)
        {
            unitOfWork = unit;
            db = dbase;
        }

        public BarterAdsController()
        {
            
        }

        public ActionResult Index()
        {
            return (RedirectToAction("Index", "Home"));
        }
        public ActionResult ShowBarterAdsOnMap()
        {
            var ads = unitOfWork.BarterAddRepository.Get();
            return View(ads.ToList());
        }
   
        public ActionResult ViewPhoto(int id)
        {
            var photo = unitOfWork.BarterAddRepository.GetByID(id).Thumbnail;
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

            BarterAdd barterAdd = unitOfWork.BarterAddRepository.GetByID(id);

            if (barterAdd == null)
            {
                return HttpNotFound();
            }
           
            ApplicationUser tempuser = unitOfWork.UserRepository.GetByID(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ApplicationUser user = db.Users.Where(M => M.Id == barterAdd.ApplicationUserId).Include("Address").FirstOrDefault();
            ApplicationUser LogUser = db.Users.Where(M => M.Id == barterAdd.ApplicationUserId).Include("Address").FirstOrDefault();


            if (user!=null)
            {
                double distance=user.Address.Coordinate.DistanceTo(LogUser.Address.Coordinate);
                ViewData["Distance"] = distance;
            }

            ViewData["Longitude"] = LogUser.Address.Coordinate.Longitude;
            ViewData["Latitude"] = LogUser.Address.Coordinate.Latitude;

            ApplicationUser User =
                  System.Web.HttpContext.Current.GetOwinContext()
                      .GetUserManager<ApplicationUserManager>()
                      .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (User != null)
            {
                List<SelectListItem> items = new List<SelectListItem>();

                foreach (var Ad in User.BarterAdds)
                {
                    items.Add(new SelectListItem { Text = Ad.Titel, Value = Ad.Titel.ToString() });
                }
                ViewBag.myAds = items;
            }


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
                    ApplicationUser User= unitOfWork.UserRepository.GetByID(user.Id);
                    User.BarterAdds.Add(barterAdd);
                    unitOfWork.Save();
                
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

            BarterAdd barterAdd = unitOfWork.BarterAddRepository.GetByID(id);
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

        public ActionResult RequestTrade(int id, string myAds)
        {
            try
            {
                TradeRequest tradeRequest = new TradeRequest();

                ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
                        .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                ApplicationUser User = db.Users.Find(user.Id);

                int tradeId = id;
                string dropValue = myAds;



                BarterAdd myAd = db.BarterAdds
                   .Single(p => p.Titel == dropValue);

                BarterAdd tradeAdd = db.BarterAdds
                  .Single(p => p.BarterAddId == tradeId);
                tradeRequest.BarterAdds.Add(myAd);
                tradeRequest.BarterAdds.Add(tradeAdd);
                tradeRequest.RequestStates = TradeRequest.States.Received;

                ApplicationUser otherUser = db.Users.Find(tradeAdd.ApplicationUserId);
                if (otherUser.Id == User.Id)
                    return RedirectToAction("ManageAds");

                //User.TradeRequests.Add(tradeRequest);
                otherUser.TradeRequests.Add(tradeRequest);
                db.SaveChanges();
                //return View(User.TradeRequests.ToList());
                return RedirectToAction("ManageAds");
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        public ActionResult AcceptTrade(int Id)
        {
            try
            {


                TradeRequest tradeRequest = db.TradeRequests.Find(Id);
                if (tradeRequest.RequestStates == TradeRequest.States.Received)
                {
                    tradeRequest.RequestStates = TradeRequest.States.Pending;

                    foreach (var ad in tradeRequest.BarterAdds)
                    {
                        if (ad.ApplicationUserId != tradeRequest.ApplicationUserId)
                        {
                            tradeRequest.ApplicationUserId = ad.ApplicationUserId;

                            db.SaveChanges();
                            break;
                        }

                    }

                }
                else
                {

                    TradeHistory myHistory = new TradeHistory();
                    TradeHistory theirHistory = new TradeHistory();

                    BarterAdd myAd = (tradeRequest.ApplicationUserId == tradeRequest.BarterAdds[0].ApplicationUserId
                        ? tradeRequest.BarterAdds[0]
                        : tradeRequest.BarterAdds[1]);
                    BarterAdd theirAd = (tradeRequest.ApplicationUserId == tradeRequest.BarterAdds[0].ApplicationUserId
                        ? tradeRequest.BarterAdds[1]
                        : tradeRequest.BarterAdds[0]);

                    ApplicationUser myUser = new ApplicationUser();
                    ApplicationUser theirUser = new ApplicationUser();

                    myUser = db.Users.Find(myAd.ApplicationUserId);
                    theirUser = db.Users.Find(theirAd.ApplicationUserId);



                    bool checkMyUser = false;
                    bool checkTheirUser = false;
                    foreach (var th in db.TradeHistory)
                    {
                        if (th.ApplicationUserId == myUser.Id)
                        {
                            myHistory = th;
                            checkMyUser = true;
                        }
                        if (th.ApplicationUserId == theirUser.Id)
                        {
                            theirHistory = th;
                            checkTheirUser = true;
                        }
                    }

                    myHistory.TradeRequests.Add(tradeRequest);
                    //myHistory.BarterAdds.Add(theirAd);
                    // theirHistory.BarterAdds.Add(theirAd);
                    theirHistory.TradeRequests.Add(tradeRequest);

                    if (checkMyUser != true)
                        myUser.TradeHistories.Add(myHistory);

                    if (checkTheirUser != true)
                        theirUser.TradeHistories.Add(theirHistory);

                    tradeRequest.RequestStates = TradeRequest.States.Traded;
                    db.SaveChanges();
                }
                return RedirectToAction("ManageAds");
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }

        public ActionResult ShowTrades()
        {
            var userId = User.Identity.GetUserId();
            var tradeRequests = from r in db.TradeRequests
                                where r.ApplicationUserId == userId && r.RequestStates != TradeRequest.States.Traded
                                select r;


            return View(tradeRequests.ToList());
        }

        public ActionResult ShowHistory()
        {


            ApplicationUser user =
                    System.Web.HttpContext.Current.GetOwinContext()
                        .GetUserManager<ApplicationUserManager>()
             .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            
            List<TradeHistory> myH = new List<TradeHistory>();

            


            return View(user.TradeHistories);
        }
    }
}


