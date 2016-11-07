using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BargainBarterV2;
using BargainBarterV2.Controllers;
using System.Threading.Tasks;
using NUnit.Framework;
using BargainBarterV2.Models;
using NSubstitute;


namespace BargainBarterV2.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTestUNitTest
    {
        private ApplicationDbContext _db;
        private HomeController _controller;
        private UnitOfWork _unitOfWork;
        [SetUp]
        public void Init()
        {
            _unitOfWork = Substitute.For<UnitOfWork>();
            _db = Substitute.For<ApplicationDbContext>();
            _controller = new HomeController(_db,_unitOfWork);        
        }


        [Test]
        public void Index_BarterAddRepository_Get_IsCalled()
        {
            _controller.Index();
            _unitOfWork.Received().BarterAddRepository.Get();
        }
      
        [Test]
        public void Index_DoesNotReturnNull()
        {
            // Arrange

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            //Assert.IsNotNull(result);
        }

        [Test]
        public void AboutViewBagDoesNotContainCorrectString()
        {
            // Arrange         
            // Act
            ViewResult result = _controller.About() as ViewResult;

            // Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
            Assert.That("Your application description page.", Is.EqualTo(result.ViewBag.Message));
        }

        [Test]
        public void ContactDoesNotReturnNull()
        {
            // Arrange
            

            // Act
            ViewResult result = _controller.Contact() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
            Assert.That(result,Is.Not.Null);
        }

        [Test]
        public void ContactViewBagDoesNotContainCorrectString()
        {
           

            ViewResult result = _controller.Contact() as ViewResult;

            Assert.That("Your contact page.", Is.EqualTo(result.ViewBag.Message));
        }

        [Test]
        public void UploadPictureDoesNotReturnNull()
        {
           

            ViewResult result = _controller.UploadPicture() as ViewResult;

            Assert.That(result, Is.Not.Null);
        }

        //[Test]
        //public void ShowPictureDoesNotReturnNull()
        //{
        //    int dummy = 6;

        //    HomeController controller = new HomeController();

        //    ViewResult result = controller.ShowPicture(dummy) as ViewResult;

        //    Assert.That(result, Is.Not.Null);

        //}


    }
}
