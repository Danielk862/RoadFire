using Microsoft.EntityFrameworkCore;
using MS.RoadFire.DataAccess.Contracts.Entities;

namespace MS.RoadFire.DataAccess.Context
{
    public class DbRoadFireContext : DbContext
    {
        public DbRoadFireContext(DbContextOptions<DbRoadFireContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
        }
    }
}
