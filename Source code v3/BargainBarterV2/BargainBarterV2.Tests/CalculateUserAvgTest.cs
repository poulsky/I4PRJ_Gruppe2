using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BargainBarterV2.Helperfunctions;
using BargainBarterV2.Models;
using NSubstitute;
using NUnit.Framework;

namespace BargainBarterV2.Tests
{
    
    [TestFixture]
    class CalculateUserAvgTest
    {

        private List<FinishedTrade> FinishedTradedsList;
        private List<BarterAdd> BarterList;
        private List<Rating> RatingList;

        [SetUp]
        public void Init()
        {
            FinishedTradedsList = new List<FinishedTrade>();
            BarterList= new List<BarterAdd>();
            RatingList= new List<Rating>();
            RatingList.Add(new Rating
            {
                ApplicationUser = new ApplicationUser(),
                ApplicationUserId = "4",
                RatingComment = "Dejlig handel",
                RatingId = 3,
                RatingValue = 1

            });
            RatingList.Add(new Rating
            {
                ApplicationUser = new ApplicationUser(),
                ApplicationUserId = "4",
                RatingComment = "Langsom leveringstid",
                RatingId = 4,
                RatingValue = 2

            });
            RatingList.Add(new Rating
            {
                ApplicationUser = new ApplicationUser(),
                ApplicationUserId = "4",
                RatingComment = "Langsom leveringstid",
                RatingId = 4,
                RatingValue = 2

            });
            FinishedTradedsList.Add(new FinishedTrade {
                BarterAdds = BarterList,
                FinishedTradeId = 3,
                Ratings = RatingList,
                TradeHistories =null, 
            });

        }

        [TestCase(0, 0, 4, 4)]
        [TestCase(2.5, 4, 5,3)]
        public void CalculateAverage_Test(double curAvg, int nRatings, double newRatingValue, double expectedResult)
        {
            Assert.That(Helper.CalculateAverage(curAvg, nRatings, newRatingValue), Is.EqualTo(expectedResult));
        }

        [Test]
        public void CalculateUserAvg_Test()
        {
            
            Assert.That(Helper.CalculateUserAvgRating(4, 2, "4", FinishedTradedsList), Is.EqualTo(2.5));
          
        }

       
    }
}
