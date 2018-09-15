using System;
using Microsoft.EntityFrameworkCore;
namespace backoffice_z.Models
{
    public class ModelContext:DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
          : base(options)
        { }

        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
