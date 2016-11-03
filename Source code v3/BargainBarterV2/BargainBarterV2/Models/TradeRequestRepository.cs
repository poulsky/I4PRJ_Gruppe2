using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BargainBarterV2.Models
{
    public class TradeRequestRepository: ITradeRequestRepository, IDisposable
    {

        private ApplicationDbContext context;

        public TradeRequestRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<TradeRequest> GetTradeRequests()
        {
            return context.TradeRequests.ToList();
        }

        public TradeRequest GetTradeRequestById(int tradeRequestId)
        {
            return context.TradeRequests.Find(tradeRequestId);
        }

        public void InsertTradeRequest(TradeRequest tradeRequest)
        {
            context.TradeRequests.Add(tradeRequest);
        }

        public void DeleteTradeRequest(int tradeRequestId)
        {
            TradeRequest tradeRequest = context.TradeRequests.Find(tradeRequestId);
            context.TradeRequests.Remove(tradeRequest);
        }

        public void UpdateTradeRequest(TradeRequest tradeRequest)
        {
            context.Entry(tradeRequest).State = EntityState.Modified;

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