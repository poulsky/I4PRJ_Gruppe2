using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BargainBarterV2.Models
{
    public class BarterAdRepository : IBarterAdRepository, IDisposable
    {

        private ApplicationDbContext context;

        public BarterAdRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<BarterAdd> GetBarterAdds()
        {
            return context.BarterAdds.ToList();
        }

        public BarterAdd GetBarterAddById(int barterAddId)
        {
            return context.BarterAdds.Find(barterAddId);
        }

        public void InsertBarterAdd(BarterAdd barterAdd)
        {
            context.BarterAdds.Add(barterAdd);
        }

        public void DeleteBarter(int barterAddId)
        {
            BarterAdd barterAd = context.BarterAdds.Find(barterAddId);
            context.BarterAdds.Remove(barterAd);
        }

        public void UpdateBarterAdd(BarterAdd barterAdd)
        {
            context.Entry(barterAdd).State = EntityState.Modified;
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