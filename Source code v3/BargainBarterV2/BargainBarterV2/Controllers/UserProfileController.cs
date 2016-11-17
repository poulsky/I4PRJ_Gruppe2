using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BargainBarterV2.Models;
using Microsoft.AspNet.Identity;

namespace BargainBarterV2.Controllers
{
    public class UserProfileController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private UnitOfWork _unitOfWork=new UnitOfWork();

        // GET: UserProfile
        public ActionResult Index()
        {
            return (RedirectToAction("Index", "Home"));
        }

        //GET: UserProfile/Edit
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (User.Identity.GetUserId() == id)
            {
                var user = _unitOfWork.UserRepository.Get(u => u.Id == id, includeProperties: "Address").First();
                //var user = db.Users.Include(a => a.Address).Single(u => u.Id == id);
                return View(user);
            }

            return View("ShowUserProfile");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser postedUser)
        {
            string id = User.Identity.GetUserId();

            //var user = db.Users.Include(a => a.Address).Single(u => u.Id == id);
            var user = _unitOfWork.UserRepository.Get(u => u.Id == id, includeProperties:"Address").First();

            user.Firstname = postedUser.Firstname;
            user.Lastname = postedUser.Lastname;
            user.PhoneNumber = postedUser.PhoneNumber;
            user.Address.StreetName = postedUser.Address.StreetName;
            user.Address.StreetNumber = postedUser.Address.StreetNumber;
            user.Address.City = postedUser.Address.City;
            user.Address.PostalCode = postedUser.Address.PostalCode;
            Coordinates _Coordinates = CoordinatesDistanceExtensions.GetCoordinates(user.Address);
            user.Address.Coordinate = _Coordinates;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
            //db.Users.AddOrUpdate(user);
            //db.SaveChanges();

            return RedirectToAction("ShowUserProfile","UserProfile",new {id = id});
        }

        public ActionResult ShowUserProfile(string id)
        {
            //var applicationUser = db.Users.Include(a => a.Address).Single(u => u.Id == id);
            var applicationUser = _unitOfWork.UserRepository.Get(u => u.Id == id, includeProperties:"Address").First();
            if (id == User.Identity.GetUserId())
                return View("ShowOwnUserProfile", applicationUser);
            return View(applicationUser);
        }
    }
}