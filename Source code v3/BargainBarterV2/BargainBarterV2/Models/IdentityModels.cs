using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public System.Data.Entity.DbSet<BargainBarterV2.Models.Address> Addresses { get; set; }
        public System.Data.Entity.DbSet<BargainBarterV2.Models.Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<BargainBarterV2.Models.TradeRequest> TradeRequests { get; set; }
        public System.Data.Entity.DbSet<BargainBarterV2.Models.TradeHistory> TradeHistory { get; set; }
    }

}