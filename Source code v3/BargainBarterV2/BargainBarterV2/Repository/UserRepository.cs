using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BargainBarterV2.Models;

namespace BargainBarterV2.Repository
{
    class UserRepository : IUserRepository, IDisposable
    {
        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return context.Users.ToList();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return context.Users.Where(b=>b.Id==userId).Include("Address").FirstOrDefault(); ;
        }

        public void InsertUser(ApplicationUser user)
        {
            context.Users.Add(user);
        }

        public void DeleteUser(int userId)
        {
            ApplicationUser user = context.Users.Find(userId);
            context.Users.Remove(user);
        }

        public void UpdateUser(ApplicationUser user)
        {
            context.Entry(user).State=EntityState.Modified;
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