using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BargainBarterV2.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int RatingValue { get; set; }
        public string RatingComment { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}