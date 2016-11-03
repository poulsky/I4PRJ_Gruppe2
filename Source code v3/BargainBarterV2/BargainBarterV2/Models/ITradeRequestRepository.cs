using System;
using System.Collections.Generic;

namespace BargainBarterV2.Models
{
    public interface ITradeRequestRepository : IDisposable
    {
        IEnumerable<TradeRequest> GetTradeRequests();
        TradeRequest GetTradeRequestById(int tradeRequestId);
        void InsertTradeRequest(TradeRequest tradeRequest);
        void DeleteTradeRequest(int tradeRequestId);
        void UpdateTradeRequest(TradeRequest tradeRequest);
        void Save();
    }
}