using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BargainBarterV2.Models;
using BargainBarterV2;

namespace BargainBarterV2.Tests
{
    [TestFixture]
    class TestCalculateDistance
    {

        [Test]
        public void GetDistance_Test()
        {
            Address A1 = new Address
            {
                City = "Aarhus C",
                PostalCode = "8000",
                StreetName = "Dalgas Avenue",
                StreetNumber = "10"
            };

            Address A2 = new Address
            {
                City = "Aarhus N",
                PostalCode = "8200",
                StreetName = "Kalmargade",
                StreetNumber = "42"
            };

            A1.Coordinate = CoordinatesDistanceExtensions.GetCoordinates(A1);
            A2.Coordinate= CoordinatesDistanceExtensions.GetCoordinates(A2);
            double distance= A1.Coordinate.DistanceTo(A2.Coordinate);

            Assert.That(distance,Is.LessThan(10));

        }


    }
}
