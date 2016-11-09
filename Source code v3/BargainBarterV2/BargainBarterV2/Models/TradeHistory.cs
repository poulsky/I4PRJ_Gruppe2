using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BargainBarterV2.Models
{
    public class TradeHistory
    {
        public int TradeHistoryId { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        public virtual List<TradeRequest> TradeRequests { get; set; } = new List<TradeRequest>();

        public virtual List<FinishedTrade> FinishedTrades { get; set; } = new List<FinishedTrade>();


    }
}