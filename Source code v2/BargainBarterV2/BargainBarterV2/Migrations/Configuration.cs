using BargainBarterV2.Models;
using Microsoft.AspNet.Identity;

namespace BargainBarterV2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BargainBarterV2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BargainBarterV2.Models.ApplicationDbContext context)
        {

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "Steve@Steve.com",
                    PasswordHash = password,
                    PhoneNumber = "12349873",
                    PhoneNumberConfirmed = true,
                    Email = "Steve@Steve.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Firstname = "Steve",
                    Lastname = "Hansen",
                    Address = new Address()
                    {
                        StreetName = "Jensbaggesensvej",
                        City = "Aarhus",
                        PostalCode = "8200",
                        StreetNumber = "13",
                    }

                });
        }
    }
}
