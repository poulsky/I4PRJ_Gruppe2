using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BargainBarterV2.Controllers;
using BargainBarterV2.Models;
using NSubstitute;
using NUnit.Framework;

namespace BargainBarterV2.Tests.Controllers
{
    class ChatControllerUnitTest
    {
        private IGenericRepository<ApplicationUser> _repository;
        private IUnitOfWork _unitOfWork;
        private ChatController _controller;

        [SetUp]
        public void Init()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _repository = Substitute.For<IGenericRepository<ApplicationUser>>();
            _controller = new ChatController(_unitOfWork);
        }

        [Test]
        public void Index_DoesNot_Return_Null()
        {
            _unitOfWork.UserRepository.Returns(_repository);
            _repository.GetByID(Arg.Any<int>()).Returns(new ApplicationUser());
            // Act
            var result = _controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Index_UnitOfWork_GetsCalled()
        {
            _controller.Index();

            // Assert
            _unitOfWork.Received().UserRepository.GetByID(Arg.Any<string>());
        }



    }
}
