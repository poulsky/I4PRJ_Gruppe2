using System.Collections.Generic;

namespace BargainBarterV2.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public Coordinates Coordinate { get; set; }
    }


}