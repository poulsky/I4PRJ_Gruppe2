using System;
using System.Collections.Generic;

namespace BargainBarterV2.Models
{
    public interface IAddressRepository : IDisposable
    {
        IEnumerable<Address> GetAddressess();
        Address GetAddressById(int addressId);
        void InsertAddress(Address address);
        void DeleteAddress(int addressId);
        void UpdateAddress(Address address);
        void Save();
    }
}