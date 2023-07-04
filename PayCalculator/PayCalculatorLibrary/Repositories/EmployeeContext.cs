using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Repositories
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() { } 

        public EmployeeContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<PermanentEmployee> PermanentEmployees { get; set; }
        public virtual DbSet<TemporaryEmployee> TemporaryEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EmployeeDatabase"
                );
            }
        }
    }
}