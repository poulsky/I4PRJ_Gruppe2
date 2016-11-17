using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using BargainBarterV2;
using BargainBarterV2.Controllers;
using System.Threading.Tasks;
using NUnit.Framework;
using BargainBarterV2.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;


namespace BargainBarterV2.Tests.Controllers
{

    [TestFixture]
    public class BarterAdsControllerUnitTest    
    {
        private IGenericRepository<BarterAdd> _repository;
        private IUnitOfWork _unitOfWork;
        private BarterAdsController _controller; 

        [SetUp]
        public void Init()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _repository = Substitute.For<IGenericRepository<BarterAdd>>();
            _controller = new BarterAdsController(_unitOfWork);
        }

        [Test]
        public void Index_RedirectsToController_HomeActionIndex()
        {   
            var result = _controller.Index() as RedirectToRouteResult;

            if (result == null)
                Assert.Fail("");

            Assert.That(result.RouteValues["Controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["Action"], Is.EqualTo("Index"));
        }

        [Test]
        public void ShowBarterAdsOnMap_BarterAddRepository_Get_IsCalled()
        {
            _controller.ShowBarterAdsOnMap();
            _unitOfWork.Received().UserRepository.Get();
        }

   [Test]
        public void Details_NullId_Returns_BadRequest()
        {
            var result = _controller.Details(null);

            Assert.That(result, Is.InstanceOf(typeof(HttpStatusCodeResult)));
        }

        //[Test]
        //public void Details_ReturnsHttpNotFoundResult()
        //{
        //    _controller = new BarterAdsController(_db,_unitOfWork);

        //    _db.BarterAdds.Find(3).Returns(new BarterAdd());

        //    var result = _controller.Details(3);

        //    Assert.That(result, Is.TypeOf(typeof(HttpNotFoundResult)));
        //}

        [Test]
        public void Create_DoesNot_Return_Null()
        {
            // Arrange
           
            // Act
            ViewResult result = _controller.Create() as ViewResult;

            // Assert

            Assert.That(result, Is.Not.Null);
            //Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_NullId_Returns_BadRequest()
        {
            var result = _controller.Edit(null);

            Assert.That(result, Is.InstanceOf(typeof(HttpStatusCodeResult)));
        }

        [Test]
        public void Delete_NullId_Returns_BadRequest()
        {
            var result = _controller.Delete(null);

            Assert.That(result, Is.InstanceOf(typeof(HttpStatusCodeResult)));
        }



    }
}