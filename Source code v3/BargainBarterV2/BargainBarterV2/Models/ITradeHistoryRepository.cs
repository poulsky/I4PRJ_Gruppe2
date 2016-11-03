using System;
using System.Collections.Generic;

namespace BargainBarterV2.Models
{
    public interface ITradeHistoryRepository : IDisposable
    {
        IEnumerable<TradeHistory> GetTradeHistories();
        TradeHistory GetTradeHistoryById(int tradehisoryId);
        void InsertTradeHistory(TradeHistory tradeHistory);
        void DeleteTradeHistory(int tradehisoryId);
        void UpdateTradeHistory(TradeHistory tradeHistory);
        void Save();
    }
}