using System;
using BargainBarterV2.Repository;

namespace BargainBarterV2.Models
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationDbContext context = ApplicationDbContext.Create();
        private IGenericRepository<BarterAdd> barterAddRepository;
        private IGenericRepository<Address> addressRepository;
        private IGenericRepository<Comment> commentRepository;
        private IGenericRepository<TradeRequest> tradeReqeustRepository;
        private IGenericRepository<ApplicationUser> userRepository;
        private IGenericRepository<TradeHistory> tradeHistoryRepository;


        public IGenericRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<ApplicationUser>(context);
                }
                return userRepository;
            }
        }

        public IGenericRepository<BarterAdd> BarterAddRepository
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

        public IGenericRepository<Address> AddressRepository
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

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<Comment>(context);
                }
                return commentRepository;
            }
        }

        public IGenericRepository<TradeRequest> TradeRequestRepository
        {
            get
            {

                if (this.tradeReqeustRepository == null)
                {
                    this.tradeReqeustRepository = new GenericRepository<TradeRequest>(context);
                }
                return tradeReqeustRepository;
            }
        }

        public IGenericRepository<TradeHistory> TradeHistoryRepository
        {
            get
            {
                if (this.tradeHistoryRepository == null)
                {
                    this.tradeHistoryRepository = new GenericRepository<TradeHistory>(context);
                }
                return tradeHistoryRepository;
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