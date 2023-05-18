using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Repositories
{
    public class PermanentEmployeeRepository : IEmployeeRepository<PermanentEmployee>
    {
        private List<PermanentEmployee> _permanentEmployeeList;

        public PermanentEmployeeRepository()
        {
            _permanentEmployeeList = new List<PermanentEmployee>()
            {
                new PermanentEmployee()
                {
                    Name = "Joe Bloggs",
                    Id = 001,
                    Salary = 25000,
                    Contract = ContractType.Permanent,
                    Bonus = 2500,
                    StartDate = new DateTime(2022, 8, 15),
                },

                new PermanentEmployee()
                {
                    Name = "John Smith",
                    Id = 002,
                    Salary = 35000,
                    Contract = ContractType.Permanent,
                    Bonus = 1000,
                    StartDate = new DateTime(2023, 5, 8),
                }
            };
        }

        public PermanentEmployee Create(PermanentEmployee employee)
        {
            Random r = new();
            employee.Id = r.Next(3, 1000);
            employee.Contract = ContractType.Permanent;
            _permanentEmployeeList.Add(employee);
            return employee;
        }

        public PermanentEmployee? GetEmployee(int id)
        {
            var employee = _permanentEmployeeList.SingleOrDefault(x => x.Id == id);
            return employee;
        }

        public PermanentEmployee? Update(PermanentEmployee employee)
        {
            var existing = _permanentEmployeeList.FirstOrDefault(x => x.Id == employee.Id);
            var updated = employee;
            existing.Name = updated.Name;
            existing.Salary = updated.Salary; 
            existing.Bonus = updated.Bonus; 
            existing.HoursWorked = updated.HoursWorked; 
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = GetEmployee(id);
            
            if (employee == null)
            {
                return false;
            }
           
            _permanentEmployeeList.Remove(employee);
            return true;
        }

        public IEnumerable<PermanentEmployee> GetAll()  
        {
            return _permanentEmployeeList;
        }
    }
}
