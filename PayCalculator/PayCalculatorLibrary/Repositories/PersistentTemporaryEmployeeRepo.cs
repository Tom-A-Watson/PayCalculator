using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Services;

namespace PayCalculatorLibrary.Repositories
{
    public class PersistentTemporaryEmployeeRepo : DbContext, IEmployeeRepository<TemporaryEmployee>
    {
        private readonly EmployeeContext _context;
        private readonly ITemporaryPayCalculator _payCalculator;
        private readonly ITimeCalculator _timeCalculator;

        public PersistentTemporaryEmployeeRepo(EmployeeContext context, ITemporaryPayCalculator payCalculator, ITimeCalculator timeCalculator)
        {
            _context = context;
            _payCalculator = payCalculator;
            _timeCalculator = timeCalculator;
        }

        public TemporaryEmployee Create(TemporaryEmployee employee)
        {
            employee.Contract = ContractType.Temporary;
            employee.HoursWorked = _timeCalculator.HoursWorked(employee.StartDate, DateTime.Now);
            employee.WeeksWorked = _timeCalculator.WeeksWorked(employee.StartDate, DateTime.Now);
            employee.TotalAnnualPay = _payCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            employee.HourlyRate = _payCalculator.HourlyRate(employee.DayRate);
            _context.TemporaryEmployees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = _context.TemporaryEmployees.Find(id);

            if (employee != null)
            {
                _context.TemporaryEmployees.Remove(employee);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<TemporaryEmployee> GetAll()
        {
            return _context.TemporaryEmployees.ToList();
        }

        public TemporaryEmployee? GetEmployee(int id)
        {
            var employee = _context.TemporaryEmployees.Find(id);
            return employee;
        }

        public TemporaryEmployee? Update(TemporaryEmployee employee)
        {
            var existing = _context.TemporaryEmployees.FirstOrDefault(x  => x.Id == employee.Id);
            var updated = employee;

            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.DayRate = updated.DayRate;
                existing.StartDate = updated.StartDate;
                _context.SaveChanges();
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
