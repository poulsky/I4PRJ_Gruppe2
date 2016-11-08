using System;
using System.Collections.Generic;
using BargainBarterV2.Models;

namespace BargainBarterV2.Repository
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<ApplicationUser> GetUsers();
        ApplicationUser GetUserById(int userId);
        void InsertUser(ApplicationUser user);
        void DeleteUser(int userId);
        void UpdateUser(ApplicationUser user);
        void Save();
    }
}