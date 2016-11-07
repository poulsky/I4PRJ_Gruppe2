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

namespace BargainBarterV2.Tests.Controllers
{
    [TestFixture]
    public class ManageControllerUnitTest
    {
        private ManageController _controller;

        [SetUp]
        public void Init()
        {
            _controller = new ManageController();
        }

        [Test]
        public void ChangePassword_DoesNotReturnNull()
        {          
            // Act
            ViewResult result = _controller.ChangePassword() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            //Assert.IsNotNull(result);
        }


    }
}