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
        public void GetCoordinates_Test()
        {
            Address A1 = new Address
            {
                City = "Aarhus C",
                PostalCode = "8000",
                StreetName = "Dalgas Avenue",
                StreetNumber = "10"
            };

            Coordinates coordinates = CoordinatesDistanceExtensions.GetCoordinates(A1);


        }


    }
}
