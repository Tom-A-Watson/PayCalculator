using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Services;

namespace PayCalculatorLibrary.Repositories
{
    public class PersistentPermanentEmployeeRepo : IEmployeeRepository<PermanentEmployee>
    {
        private readonly EmployeeContext _context;
        private readonly IPermanentPayCalculator _payCalculaor;
        private readonly ITimeCalculator _timeCalculator;

        public PersistentPermanentEmployeeRepo(EmployeeContext context, IPermanentPayCalculator payCalculator, ITimeCalculator timeCalculator)
        {
            _context = context;
            _payCalculaor = payCalculator;
            _timeCalculator = timeCalculator;
        }

        public PermanentEmployee Create(PermanentEmployee employee)
        {
            employee.Contract = ContractType.Permanent;
            employee.HoursWorked = _timeCalculator.HoursWorked(employee.StartDate, DateTime.Now);
            employee.TotalAnnualPay = _payCalculaor.TotalAnnualPay(employee.Salary.Value, employee.Bonus.Value);
            employee.HourlyRate = _payCalculaor.HourlyRate(employee.Salary.Value, employee.HoursWorked);
            _context.PermanentEmployees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = _context.PermanentEmployees.Find(id);

            if (employee != null)
            {
                _context.PermanentEmployees.Remove(employee);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<PermanentEmployee> GetAll()
        {
            return _context.PermanentEmployees.ToList();
        }

        public PermanentEmployee? GetEmployee(int id)
        {
            var employee = _context.PermanentEmployees.Find(id);
            return employee;
        }

        public PermanentEmployee? Update(PermanentEmployee updated)
        {
            var existing = _context.PermanentEmployees.FirstOrDefault(x => x.Id == updated.Id);

            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.Salary = updated.Salary;
                existing.Bonus = updated.Bonus;
                existing.StartDate = updated.StartDate;
                _context.SaveChanges();
                return updated;
            }

            return null;
        }
    }
}