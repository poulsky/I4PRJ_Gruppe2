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
            // Gør decimaltal sepereret med '.' i stedet for ','
            // Er for at stjerne kan vises i som halvt fyldte
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            //var results = from m in db.BarterAdds select m;
            var results = unitOfWork.BarterAddRepository.Get((p => p.Traded != true), includeProperties:"ApplicationUser");
            ViewBag.Users = unitOfWork.UserRepository.Get((p => p.BarterAdds.Count > 0));
            
            
            return View("Frontpage",results.ToList());
        }

        public ActionResult About()
        {
           return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Oplever du problemer? Så kontakt os her:";

            return View();
        }     
        
    }

}