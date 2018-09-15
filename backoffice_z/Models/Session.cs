using System;
namespace backoffice_z.Models
{
    public class Session
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string CreatedAt { get; set; }
        public string Token { get; set; }
    }
}
