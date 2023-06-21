using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Repositories
{
    public class PersistentPermanentEmployeeRepo : DbContext, IEmployeeRepository<PermanentEmployee>
    {
        public DbSet<PermanentEmployee> PermanentEmployees { get; set; }

        public PermanentEmployee Create(PermanentEmployee employee)
        {
            employee.Contract = ContractType.Permanent;
            PermanentEmployees.Add(employee);
            SaveChanges();
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = PermanentEmployees.Find(id);

            if (employee != null)
            {
                PermanentEmployees.Remove(employee);
                SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<PermanentEmployee> GetAll()
        {
            return PermanentEmployees.ToList();
        }

        public PermanentEmployee? GetEmployee(int id)
        {
            var employee = PermanentEmployees.Find(id);
            return employee;
        }

        public PermanentEmployee? Update(PermanentEmployee employee)
        {
            var existing = PermanentEmployees.FirstOrDefault(x => x.Id == employee.Id);
            var updated = employee;

            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.Salary = updated.Salary;
                existing.Bonus = updated.Bonus;
                existing.StartDate = updated.StartDate;
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
