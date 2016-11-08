using System;
using BargainBarterV2.Repository;

namespace BargainBarterV2.Models
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = ApplicationDbContext.Create();
        private GenericRepository<BarterAdd> barterAddRepository;
        private GenericRepository<Address> addressRepository;
        private GenericRepository<Comment> commentRepository;
        private GenericRepository<TradeRequest> tradeReqeustRepository;
        private GenericRepository<ApplicationUser> userRepository;


        public GenericRepository<ApplicationUser> UserRepository
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

        public GenericRepository<Comment> CommentRepository
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

        public GenericRepository<TradeRequest> TradeRequestRepository
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