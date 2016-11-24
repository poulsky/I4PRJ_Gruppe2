using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BargainBarterV2.Models;
using Microsoft.AspNet.Identity;

namespace BargainBarterV2.Controllers
{
    public class ChatController : Controller
    {
        
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Chat,
        [Authorize]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = unitOfWork.UserRepository.GetByID(id);

            return View(user);
        }
    }
}