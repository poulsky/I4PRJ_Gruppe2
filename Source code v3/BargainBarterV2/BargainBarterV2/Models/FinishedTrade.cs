using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BargainBarterV2.Models
{
    public class FinishedTrade
    {
        public int FinishedTradeId { get; set; }
        public virtual List<BarterAdd> BarterAdds { get; set; } = new List<BarterAdd>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual List<TradeHistory> TradeHistories { get; set; } = new List<TradeHistory>();
    }
}