using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BargainBarterV2.Models;

namespace BargainBarterV2.Helperfunctions
{
    public class Helper
    {
        
        public static byte[] MakeThumbnail(byte[] myImage, int thumbWidth, int thumbHeight)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(myImage)).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }


        public static double CalculateUserAvgRating(double newRating, double currentRating, string userId, List<FinishedTrade> finishedTrades)
        {
            int nRatings = 0;

            foreach (var finTrade in finishedTrades)
            {

                foreach (var rating in finTrade.Ratings)
                {
                    if (userId == rating.ApplicationUserId)
                    {
                        nRatings++;
                    }
                }
            }
            
            return CalculateAverage(currentRating, nRatings, newRating);
        }
        
        public static double CalculateAverage(double? curAvg, int nRatings, double newRatingValue)
        {
            var d = ((curAvg*nRatings) + newRatingValue)/(nRatings + 1);
            //if (d != null)
                return (double) d;
            //else return -1;
        }
    }
}