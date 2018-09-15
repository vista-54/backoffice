using System;
namespace backoffice_z.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
