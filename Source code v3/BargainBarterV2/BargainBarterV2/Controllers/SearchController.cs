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
        private UnitOfWork unitOfWork=new UnitOfWork();

        public SearchController(UnitOfWork unit)
        {
            unitOfWork = unit;
        }

        public SearchController()
        { }

        // GET: Search
        public ActionResult Index(string searchstring)
        {
            var results = unitOfWork.BarterAddRepository.Get();

            if (!String.IsNullOrEmpty(searchstring))
                results = unitOfWork.BarterAddRepository.Get(p=> p.Titel.Contains(searchstring) && p.Traded != true);
           
            return View("Frontpage", results.ToList());
        }

        public ActionResult CategorySearch(string searchstring)
        {
            var results = unitOfWork.BarterAddRepository.Get(a => a.Category == searchstring);
                
            return View("Frontpage", results.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
