using Microsoft.EntityFrameworkCore;
using OfficeMenegment.Models;

namespace OfficeMenegment.Data
{
    public class OfficeMenegmentDbContext : DbContext
    {
        public OfficeMenegmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
