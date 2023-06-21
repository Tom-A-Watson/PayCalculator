using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Repositories
{
    public class PersistentTemporaryEmployeeRepo : DbContext, IEmployeeRepository<TemporaryEmployee>
    {
        public DbSet<TemporaryEmployee> TemporaryEmployees { get; set; }

        public TemporaryEmployee Create(TemporaryEmployee employee)
        {
            employee.Contract = ContractType.Temporary;
            TemporaryEmployees.Add(employee);
            SaveChanges();
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = TemporaryEmployees.Find(id);

            if (employee != null)
            {
                TemporaryEmployees.Remove(employee);
                SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<TemporaryEmployee> GetAll()
        {
            return TemporaryEmployees.ToList();
        }

        public TemporaryEmployee? GetEmployee(int id)
        {
            var employee = TemporaryEmployees.Find(id);
            return employee;
        }

        public TemporaryEmployee? Update(TemporaryEmployee employee)
        {
            var existing = TemporaryEmployees.FirstOrDefault(x  => x.Id == employee.Id);
            var updated = employee;

            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.DayRate = updated.DayRate;
                existing.StartDate = updated.StartDate;
                SaveChanges();
                return employee;
            }

            return null;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EmployeeDatabase"
            );
        }
    }
}
