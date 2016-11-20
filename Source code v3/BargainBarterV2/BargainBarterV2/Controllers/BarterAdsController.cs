using System;
using System.Collections;
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
using BargainBarterV2.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BargainBarterV2.Controllers
{
    public class BarterAdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IUnitOfWork unitOfWork = new UnitOfWork();

        public BarterAdsController(IUnitOfWork unit)
        {
            unitOfWork = unit;
            
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
            IEnumerable<ApplicationUser> users = unitOfWork.UserRepository.Get(includeProperties:"Address, BarterAdds").ToList();
            return View(users);
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
            ApplicationUser user = null;
            if (tempuser != null)
                user = unitOfWork.UserRepository.Get(b => b.Id == tempuser.Id, includeProperties: "Address").FirstOrDefault();
            ApplicationUser logUser = unitOfWork.UserRepository.Get(b=> b.Id==barterAdd.ApplicationUserId, includeProperties:"Address").FirstOrDefault();

            if (user == logUser)
                return RedirectToAction("DetailsOwn", "BarterAds", new { id = barterAdd.BarterAddId });

            //ApplicationUser tempuser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //ApplicationUser user = null;
            //if (tempuser != null)
            //    user = db.Users.Where(u=> u.Id==tempuser.Id).Include("Address").FirstOrDefault();
            //ApplicationUser logUser = db.Users.Where(u => u.Id == barterAdd.ApplicationUserId).Include("Address").FirstOrDefault();

            if (user!=null)
            {
                double distance=user.Address.Coordinate.DistanceTo(logUser.Address.Coordinate);
                ViewData["Distance"] = distance.ToString("#.##");

                List<SelectListItem> items = new List<SelectListItem>();

                foreach (var Ad in user.BarterAdds)
                {
                    if(Ad.Traded != true)
                         items.Add(new SelectListItem { Text = Ad.Titel, Value = Ad.Titel.ToString() });
                }
                ViewBag.myAds = items;
            }

            if (logUser.Address != null)
            {
                ViewData["Longitude"] = logUser.Address.Coordinate.Longitude;
                ViewData["Latitude"] = logUser.Address.Coordinate.Latitude;
            }
          
           return View(barterAdd);
        }

        // GET: BarterAds/Details/5
        public ActionResult DetailsOwn(int? id)
        {
            BarterAdd barterAdd= unitOfWork.BarterAddRepository.GetByID(id);
            //ApplicationUser logUser = db.Users.Where(u => u.Id == barterAdd.ApplicationUserId).Include("Address").FirstOrDefault();
            ApplicationUser logUser = unitOfWork.UserRepository.Get(u => u.Id == barterAdd.ApplicationUserId, includeProperties:"Address").FirstOrDefault();
            if (logUser.Address != null)
            {
                ViewData["Longitude"] = logUser.Address.Coordinate.Longitude;
                ViewData["Latitude"] = logUser.Address.Coordinate.Latitude;
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

            return RedirectToAction("ManageAds", "BarterAds");
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
                //ApplicationUser user1 =
                //    db.Users.Include(b => b.BarterAdds).Single(u => u.Id == user.Id);
                ApplicationUser user1 =
                    unitOfWork.UserRepository.Get(b => b.Id == user.Id, includeProperties: "BarterAdds").Single();

                var temp = user1.BarterAdds.Single(b => b.BarterAddId == barterAdd.BarterAddId);

                temp.Description = barterAdd.Description;
                temp.Titel = barterAdd.Titel;
                temp.Category = barterAdd.Category;
                temp.Picture = barterAdd.Picture;
                temp.Thumbnail = barterAdd.Thumbnail;

                //db.Users.AddOrUpdate(user1);
                
                //db.SaveChanges();
                unitOfWork.UserRepository.Update(user1);
                unitOfWork.Save();
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
                ApplicationUser = unitOfWork.UserRepository.GetByID(System.Web.HttpContext.Current.User.Identity.GetUserId())
                //System.Web.HttpContext.Current.GetOwinContext()
                //    .GetUserManager<ApplicationUserManager>()
                //    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId())
            };

            //BarterAdd ad2 = db.BarterAdds.Find(id);
            BarterAdd ad2 = unitOfWork.BarterAddRepository.GetByID(id);
            ad2.Comments.Add(comment);
            //db.SaveChanges();
            unitOfWork.Save();

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
            //BarterAdd barterAdd = db.BarterAdds.Find(id);
            BarterAdd barterAdd = unitOfWork.BarterAddRepository.GetByID(id);
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
            //BarterAdd barterAdd = db.BarterAdds.Find(id);
            //db.BarterAdds.Remove(barterAdd);
            unitOfWork.BarterAddRepository.Delete(id);
            unitOfWork.Save();
            //db.SaveChanges();
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

            //var barterAds = from r in db.BarterAdds
            //                where r.ApplicationUserId == userId
            //                select r;

            var barterAds = unitOfWork.BarterAddRepository.Get(r => r.ApplicationUserId == userId);

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
                //ApplicationUser User = db.Users.Find(user.Id);
                ApplicationUser User = unitOfWork.UserRepository.GetByID(user.Id);

                int tradeId = id;
                string dropValue = myAds;



                //BarterAdd myAd = db.BarterAdds
                //   .Single(p => p.Titel == dropValue);

                //BarterAdd tradeAdd = db.BarterAdds
                //  .Single(p => p.BarterAddId == tradeId);

                BarterAdd myAd = unitOfWork.BarterAddRepository.Get(p => p.Titel == dropValue).Single();
                BarterAdd tradeAdd = unitOfWork.BarterAddRepository.Get(p => p.BarterAddId == tradeId).Single();

                tradeRequest.BarterAdds.Add(myAd);
                tradeRequest.BarterAdds.Add(tradeAdd);
                tradeRequest.RequestStates = TradeRequest.States.Received;

                ApplicationUser otherUser = unitOfWork.UserRepository.GetByID(tradeAdd.ApplicationUserId);
                if (otherUser.Id == User.Id)
                    return RedirectToAction("ManageAds");

               // User.TradeRequests.Add(tradeRequest);
               
                otherUser.TradeRequests.Add(tradeRequest);
                //db.SaveChanges();
                unitOfWork.Save();
                //return View(User.TradeRequests.ToList());
                return RedirectToAction("ManageAds");
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        public ActionResult DeclineTrade(int Id)
        {
            //TradeRequest tradeRequest = db.TradeRequests.Find(Id);
            //tradeRequest.BarterAdds.Clear();
            //db.TradeRequests.Remove(tradeRequest);
            //db.SaveChanges();
            TradeRequest tradeRequest = unitOfWork.TradeRequestRepository.GetByID(Id);
            tradeRequest.BarterAdds.Clear();
            unitOfWork.TradeRequestRepository.Delete(tradeRequest.TradeRequestId);
            return RedirectToAction("ShowTrades");
        }

        public ActionResult AcceptTrade(int Id)
        {
            try
            {


                //TradeRequest tradeRequest = db.TradeRequests.Find(Id);
                TradeRequest tradeRequest = unitOfWork.TradeRequestRepository.GetByID(Id);
                if (tradeRequest.RequestStates == TradeRequest.States.Received)
                {
                    tradeRequest.RequestStates = TradeRequest.States.Pending;

                    foreach (var ad in tradeRequest.BarterAdds)
                    {
                        if (ad.ApplicationUserId != tradeRequest.ApplicationUserId)
                        {
                            tradeRequest.ApplicationUserId = ad.ApplicationUserId;

                            //db.SaveChanges();
                            unitOfWork.Save();
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
                    myAd.Traded = true;
                    theirAd.Traded = true;
                    FinishedTrade finTrade = new FinishedTrade();
                    finTrade.BarterAdds.Add(myAd);
                    finTrade.BarterAdds.Add(theirAd);

                    ApplicationUser myUser = new ApplicationUser();
                    ApplicationUser theirUser = new ApplicationUser();

                    //myUser = db.Users.Find(myAd.ApplicationUserId);
                    //theirUser = db.Users.Find(theirAd.ApplicationUserId);
                    myUser = unitOfWork.UserRepository.GetByID(myAd.ApplicationUserId);
                    theirUser = unitOfWork.UserRepository.GetByID(theirAd.ApplicationUserId);

                    bool checkMyUser = false;
                    bool checkTheirUser = false;
                    foreach (var th in unitOfWork.TradeHistoryRepository.Get())
                    {
                        if (th.ApplicationUser == myUser)
                        {
                            myHistory = th;
                            checkMyUser = true;
                        }
                        if (th.ApplicationUser == theirUser)
                        {
                            theirHistory = th;
                            checkTheirUser = true;
                        }
                    }

                    myHistory.FinishedTrades.Add(finTrade);
                    theirHistory.FinishedTrades.Add(finTrade);

                    if (checkMyUser != true)
                        myUser.TradeHistory = myHistory;

                    if (checkTheirUser != true)
                        theirUser.TradeHistory = theirHistory;

                    tradeRequest.RequestStates = TradeRequest.States.Traded;

                    // tradeRequest.BarterAdds.Clear();
                    unitOfWork.TradeRequestRepository.Delete(tradeRequest.TradeRequestId);
                    // db.TradeRequests.Remove(tradeRequest);
                    unitOfWork.Save();
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
            var tradeRequests = from r in db.TradeRequests.Include(u => u.ApplicationUser)
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
            var item = db.TradeHistory.Include(u => u.ApplicationUser)
                .FirstOrDefault(b => b.ApplicationUser.Id == user.Id);

            if (item == null)
                item = new TradeHistory();

            return View(item);
        }

        public ActionResult ShowTheirTradeHistory(string id)
        {

            var item = db.TradeHistory
                .FirstOrDefault(b => b.ApplicationUser.Id == id);


            if (id == User.Identity.GetUserId())
                return RedirectToAction("ShowHistory");

            if (item == null)
                item = new TradeHistory();

            item.ApplicationUser = db.Users.Find(id);

            return View(item);
        }

        [HttpPost]
        public ActionResult ShowNearest(double afstand)
        {
            if (User.Identity.GetUserId() != null)
            {
                var users = unitOfWork.UserRepository.Get(includeProperties: "BarterAdds, Address");
                var userid = User.Identity.GetUserId();
                
                var currentUser = unitOfWork.UserRepository.Get(user => user.Id == userid,
                    includeProperties: "BarterAdds, Address").Single();
                List<ApplicationUser> neighbours = new List<ApplicationUser>();
                List<BarterAdd> barterAdsCloseToYou = new List<BarterAdd>();
                foreach (var user in users)
                {
                    double distance = currentUser.Address.Coordinate.DistanceTo(user.Address.Coordinate);
                    if (distance < afstand)
                    {
                        neighbours.Add(user);
                    }
                }

                foreach (var user in neighbours)
                {
                    foreach (var ad in user.BarterAdds)
                    {
                        if(!ad.Traded)
                            barterAdsCloseToYou.Add(ad);
                    }
                }

                return View("Frontpage", barterAdsCloseToYou);
            }
            return View("DefaultNearestView");
        }
    }
}


