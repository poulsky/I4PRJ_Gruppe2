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
        private IUnitOfWork unitOfWork=new UnitOfWork();
        private IGenericRepository<BarterAdd> repository;

        public SearchController()
        {
            repository = unitOfWork.BarterAddRepository;
        }


        public SearchController(IUnitOfWork unit, IGenericRepository<BarterAdd> repository)
        {
            unitOfWork = unit;
            this.repository = repository;
        }

        // GET: Search
        public ActionResult Index(string searchstring)
        {
            var results = repository.Get();

            if (!String.IsNullOrEmpty(searchstring))
                results = repository.Get(p=> p.Titel.Contains(searchstring) && p.Traded != true);
           
            return View("Frontpage", results.ToList());
        }

        public ActionResult CategorySearch(string searchstring)
        {
            var results = repository.Get(a => a.Category == searchstring);
                
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
