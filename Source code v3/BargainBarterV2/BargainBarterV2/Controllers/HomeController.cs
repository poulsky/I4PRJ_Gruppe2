using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BargainBarterV2.Models;
using System.Drawing;
using System.Net;

namespace BargainBarterV2.Controllers
{

    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        public HomeController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }

        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            //var results = from m in db.BarterAdds select m;
            var results = unitOfWork.BarterAddRepository.Get((p => p.Traded != true));
            return View("Frontpage",results.ToList());
        }

        public ActionResult About()
        {
           return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }     
        
    }

}