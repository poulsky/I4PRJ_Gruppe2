using System;

namespace BargainBarterV2.Models
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = ApplicationDbContext.Create();
        private GenericRepository<BarterAdd> barterAddRepository;
        private GenericRepository<Address> addressRepository;

        public GenericRepository<BarterAdd> BarterAddRepository
        {
            get
            {
                if (this.barterAddRepository == null)
                {
                    this.barterAddRepository = new GenericRepository<BarterAdd>(context);
                }
                return barterAddRepository;
            }
        }

        public GenericRepository<Address> AddressRepository
        {
            get
            {

                if (this.addressRepository == null)
                {
                    this.addressRepository = new GenericRepository<Address>(context);
                }
                return addressRepository;
            }
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