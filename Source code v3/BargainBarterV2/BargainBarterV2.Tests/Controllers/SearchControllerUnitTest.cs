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
    public class SearchControllerUnitTest
    {
        private SearchController _controller;
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<BarterAdd> _barterAddRepo;

        [SetUp]
        public void Init()
        {
            _unitOfWork = new UnitOfWork();
            _barterAddRepo = Substitute.For<IGenericRepository<BarterAdd>>();
            _controller = new SearchController(_unitOfWork, _barterAddRepo);
        }

        [Test]
        public void Index_GetIsCalled()
        {
            _controller.Index("");
            _barterAddRepo.Received().Get();
        }

        [Test]
        public void Index_DoesNotReturnNull()
        {
            ViewResult result = _controller.Index("") as ViewResult;
            
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Index_SearchstringIsNull_ReturnsNotNull()
        {
            string n = null;
            var result = _controller.Index(n);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Categorysearch_SearchstringIsNonEmpty_ResultIsNotNull()
        {
            var s = "";
            var result = _controller.CategorySearch(s);
            Assert.That(result, Is.Not.Null);
        }
    }
}