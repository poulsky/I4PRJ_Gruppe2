using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BargainBarterV2.Models
{
    public class BarterAdd
    {
        public BarterAdd()
        {
            Comments = new List<Comment>();
        }
        public int BarterAddId { get; set; }
        [Required]
        public string Titel { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Thumbnail { get; set; }
        public string Category { get; set; }
        [ForeignKey("ApplicationUser")]

        public string ApplicationUserId { get; set; }
        //[Required] brækker opret barteradsiden
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual List<TradeRequest> TradeRequests { get; set; } = new List<TradeRequest>();
        public bool Traded { get; set; }


    }
}