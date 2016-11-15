namespace BargainBarterV2.Models
{
    public interface IUnitOfWork
    {
        IGenericRepository<Address> AddressRepository { get; }
        IGenericRepository<BarterAdd> BarterAddRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<TradeHistory> TradeHistoryRepository { get; }
        IGenericRepository<TradeRequest> TradeRequestRepository { get; }
        IGenericRepository<ApplicationUser> UserRepository { get; }

        void Dispose();
        void Save();
    }
}