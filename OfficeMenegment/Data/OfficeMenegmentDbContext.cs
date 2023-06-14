using Microsoft.EntityFrameworkCore;
using OfficeMenegment.Models;

namespace OfficeMenegment.Data
{
    public class OfficeMenegmentDbContext : DbContext
    {
        public OfficeMenegmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmployeeDbModel> Employees { get; set; }
        public DbSet<DepartmentDbModel> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDbModel>()
                .HasMany(e => e.Departments)
                .WithMany(d => d.Employees)
                .UsingEntity(x => x.ToTable("DepartmentEmployee"));
        }
    }
}
