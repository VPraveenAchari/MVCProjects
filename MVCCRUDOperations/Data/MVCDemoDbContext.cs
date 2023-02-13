using Microsoft.EntityFrameworkCore;
using MVCCRUDOperations.Models.Domain;

namespace MVCCRUDOperations.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> EmployeeDetails { get; set; }
    }
}
