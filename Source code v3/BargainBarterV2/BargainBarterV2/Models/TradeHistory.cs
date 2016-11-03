using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BargainBarterV2.Models
{
    public class TradeHistory
    {
        public int TradeHistoryId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual List<TradeRequest> TradeRequests { get; set; } = new List<TradeRequest>();



      
    }
}