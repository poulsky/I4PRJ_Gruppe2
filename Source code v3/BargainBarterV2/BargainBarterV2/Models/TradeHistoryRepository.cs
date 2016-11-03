using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BargainBarterV2.Models
{
    public class TradeHistoryRepository : IDisposable, ITradeHistoryRepository
    {
        private ApplicationDbContext context;

        public TradeHistoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TradeHistory> GetTradeHistories()
        {
            return context.TradeHistory.ToList();
        }

        public TradeHistory GetTradeHistoryById(int tradehisoryId)
        {
            return context.TradeHistory.Find(tradehisoryId);
        }

        public void InsertTradeHistory(TradeHistory tradeHistory)
        {
            context.TradeHistory.Add(tradeHistory);
        }

        public void DeleteTradeHistory(int tradehisoryId)
        {
            TradeHistory tradeHistory = context.TradeHistory.Find(tradehisoryId);
            context.TradeHistory.Remove(tradeHistory);
        }

        public void UpdateTradeHistory(TradeHistory tradeHistory)
        {
            context.Entry(tradeHistory).State = EntityState.Modified;

        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}