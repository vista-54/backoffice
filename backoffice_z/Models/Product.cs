using System;
namespace backoffice_z.Models
{
    public class Product
    {
       public long Id { get; set; }
        public long CreatedBy { get; set; }
        public string Customer { get; set; }
        public string CustomePhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string PostAddress { get; set; }
        public string Plz { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public string ProductStatus { get; set; }
        public string CreatedAt { get; set; }
    }
}
