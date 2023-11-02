using Microsoft.EntityFrameworkCore;
using MyProject.Core.Entities;

namespace MyProject.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions options) : base(options)

        {
               
        }
      
        

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
