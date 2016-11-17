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
            _repository = Substitute.For<IGenericRepository<BarterAdd>>();
            _repository.Get().Returns(new List<BarterAdd>
            {
               new BarterAdd()
               {
                Titel = "Boremaskine",
                Description = "Bosch reklamerer selv med at den er til gør-det-selv manden," +
                              " men du skal vide at du også går på kompromis med både kvalitet" +
                              " og holdbarhed. Jeg har selv haft flere grønne fra Bosch og kan" +
                              " konstatere at pris og kvalitet hænger sammen. Eksempelvis har jeg" +
                              " nogle gange oplevet at aklsen er blevet skæv, og så er det umuligt" +
                              " at bore et lige hul.",
                Category = "Elektronik",
                CreatedDateTime = new DateTime(2016, 8, 4, 12, 30, 45),

                Picture = null,

                Thumbnail =null,
            },
            new BarterAdd()
            {
                Titel = "Arduino nano",
                Description = "The Arduino Nano is a small, complete, and breadboard-friendly" +
                              " board based on the ATmega328 (Arduino Nano 3.x) or ATmega168 " +
                              "(Arduino Nano 2.x). It has more or less the same functionality of the " +
                              "Arduino Duemilanove, but in a different package. It lacks only a" +
                              " DC power jack, and works with a Mini-B USB cable instead of a " +
                              "standard one. The Nano was designed and is being produced by Gravitech.",
                Category = "Elektronik",
                CreatedDateTime = new DateTime(2016, 8, 9, 12, 30, 45),

                Picture = null,

                Thumbnail = null

            }
        });
        }

        [Test]
        public void Get_Returns_TwoAds()
        {
            Assert.That(_repository.Get().Count(),Is.EqualTo(2));
        }

        
    }
}
