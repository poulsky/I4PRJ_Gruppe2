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

       

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string adcatagory, string searchString)
        {
            var results = from m in db.BarterAdds select m;
            
            return View(results.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home/UploadPicture
        public ActionResult UploadPicture()
        {
            return View();
        }

        // POST: BarterAds//5
        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase BarterPicture)
        {
            BarterAdd add = new BarterAdd();
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            if (BarterPicture.ContentLength > 0)
            {
                using (BinaryReader reader = new BinaryReader(BarterPicture.InputStream))
                {
                    add.Picture = reader.ReadBytes((int) BarterPicture.InputStream.Length);
                }
    
            }
            db.BarterAdds.Add(add);
            db.SaveChanges();

            return RedirectToAction("ShowPicture", "Home", new { id = db.BarterAdds.Count() });
        }

        public ActionResult ShowPicture(int? id)
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
            string imagepath = Convert.ToBase64String(imagedata);
            string imagedataURL = string.Format("data:image/png; base64, {0}", imagepath);
            ViewBag.image = imagedataURL;
            return View();
        }

      
        public Image byteArrayToImage(byte[] barteraddPicture)
        {
            var ms = new MemoryStream(barteraddPicture);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        
    }

    public class ImageHandler: IHttpHandler
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public void ProcessRequest(HttpContext context)
        {
            byte[] pic = db.BarterAdds.Find(12).Picture;
            context.Response.OutputStream.Write(pic,0, pic.Length);
            context.Response.ContentType = "image/JPEG";
        }

        public bool IsReusable { get; }
    }
}