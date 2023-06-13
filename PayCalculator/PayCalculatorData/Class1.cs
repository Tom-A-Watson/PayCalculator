using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;

namespace PayCalculatorData
{
    public class EmployeeContext : DbContext
    {
        public DbSet<PermanentEmployee> PermanentEmployees { get; set; }
        public DbSet<TemporaryEmployee> TemporaryEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EmployeeDatabase"
            );
        }
    }
}