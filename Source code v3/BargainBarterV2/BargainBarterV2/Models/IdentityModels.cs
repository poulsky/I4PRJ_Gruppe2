using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace BargainBarterV2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
        public virtual List<BarterAdd> BarterAdds { get; set; }

        public virtual List<TradeRequest> TradeRequests { get; set; }

        public virtual List<TradeHistory> TradeHistories { get; set; }
    }

    public class BarterAdd
    {
        public BarterAdd()
        {
            Comments = new List<Comment>();
        }
        public int BarterAddId { get; set; }
        [Required]
        public string Titel { get; set; }
        public string Description { get; set; }
        public byte[] Picture{ get; set; }
        public byte[] Thumbnail { get; set; }
        public string Category { get; set; }
        [ForeignKey("ApplicationUser")]
        
        public string ApplicationUserId { get; set; }
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual List<TradeRequest> TradeRequests { get; set; } = new List<TradeRequest>();
        public virtual List<TradeHistory> TradeHistories { get; set; } = new List<TradeHistory>();
    }

    public class Address
    {
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public Coordinates Coordinate{get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        [MinLength(1),MaxLength(500)]
        public string CommentText { get; set; }
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }

    public class TradeRequest
    {
        public int TradeRequestId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [EnumDataType(typeof(States))]
        public States RequestStates { get; set; }


        public enum States { Traded, Pending, Received }
        
        public virtual List<BarterAdd> BarterAdds { get; set; } = new List<BarterAdd>();

        public virtual List<TradeHistory> TradeHistories { get; set; } = new List<TradeHistory>();

    }

    public class TradeHistory
    {
        public int TradeHistoryId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual List<TradeRequest> TradeRequests { get; set; } = new List<TradeRequest>();

      
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApplicationUser>()
        //    .HasOptional(a=> a.BarterAdds)
        //    .WithOptionalDependent()
        //    .WillCascadeOnDelete(true);                              
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BargainBarterV2.Models.BarterAdd> BarterAdds { get; set; }
        public System.Data.Entity.DbSet<BargainBarterV2.Models.TradeRequest> TradeRequests { get; set; }
        public System.Data.Entity.DbSet<BargainBarterV2.Models.TradeHistory> TradeHistory { get; set; }
    }
}