using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BargainBarterV2.Models
{
    public class AddressRepository : IAddressRepository, IDisposable
    {

        private ApplicationDbContext context;

        public AddressRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Address> GetAddressess()
        {
            return context.Addresses.ToList();
        }

        public Address GetAddressById(int addressId)
        {
            return context.Addresses.Find(addressId);
        }

        public void InsertAddress(Address address)
        {
            context.Addresses.Add(address);
        }

        public void DeleteAddress(int addressId)
        {
            Address address = context.Addresses.Find(addressId);
            context.Addresses.Remove(address);
        }

        public void UpdateAddress(Address address)
        {
            context.Entry(address).State = EntityState.Modified;
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