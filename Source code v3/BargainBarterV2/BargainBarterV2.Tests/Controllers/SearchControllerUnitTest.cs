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

        


    }
}