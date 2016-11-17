using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BargainBarterV2.Models;
using NSubstitute;
using NUnit.Framework;

namespace BargainBarterV2.Tests.Repository
{
    [TestFixture]
    class RepositoryTest
    {
        private IGenericRepository<BarterAdd> _repository;

        [SetUp]
        public void Setup()
        {

            ApplicationUser user1 = new ApplicationUser
            {
                UserName = "Steve@Steve.com",
                PhoneNumber = "12349873",
                PhoneNumberConfirmed = true,
                Email = "Steve@Steve.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Firstname = "Steve",
                Lastname = "Hansen",
                Address = new Address()
                {
                    StreetName = "Jensbaggesensvej",
                    City = "Aarhus",
                    PostalCode = "8200",
                    StreetNumber = "13",
                    Coordinate = CoordinatesDistanceExtensions.GetCoordinates("13 Jensbaggesensvej, 8200")

                },

                BarterAdds = new List<BarterAdd>()

            };


            _repository = Substitute.For<IGenericRepository<BarterAdd>>();
            _repository.Get().Returns(new List<BarterAdd>
            {
                new BarterAdd {}
            });
        }
    }
}
