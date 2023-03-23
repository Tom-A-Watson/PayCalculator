using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;

namespace PayCalculatorMVC.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<PermanentEmployee> PermanentEmployees { get; set; }
        public DbSet<TemporaryEmployee> TemporaryEmployees { get; set; }
    }
}
