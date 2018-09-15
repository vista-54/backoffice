using System;
namespace backoffice_z.Models
{
    public class Workflow
    {
        public long Id { get; set; }
        public string Started_at { get; set; }
        public string Session { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string CurrentStage { get; set; }
        public string Applications { get; set; }
    }
}
