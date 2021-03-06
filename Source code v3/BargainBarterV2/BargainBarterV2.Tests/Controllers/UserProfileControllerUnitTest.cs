﻿using System;
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
    public class UserProfileControllerUnitTest
    {
        private UserProfileController _controller;

        [SetUp]
        public void Init()
        {
            _controller = new UserProfileController();
        }


        [Test]
        public void Index_RedirectsToController_HomeActionIndex()
        {
            var result = _controller.Index() as RedirectToRouteResult;

            if (result == null)
                Assert.Fail("should have redirected");

            Assert.That(result.RouteValues["Controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["Action"], Is.EqualTo("Index"));

        }

        //[Test]
        //public void Index_NullId_Returns_BadRequest()
        //{
        //    var result = _controller.Index();

        //    Assert.That(result, Is.InstanceOf(typeof(HttpStatusCodeResult)));
        //}




    }
}