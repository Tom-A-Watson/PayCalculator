using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Services;

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
                    HoursWorked = 1820
                },

                new PermanentEmployee()
                {
                    Name = "John Smith",
                    Id = 002,
                    Salary = 35000,
                    Contract = ContractType.Permanent,
                    Bonus = 1000,
                    HoursWorked = 1820
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
            int index = _permanentEmployeeList.FindIndex(x => x.Id == employee.Id);
            var theEmployee = _permanentEmployeeList[index];

            if (index < 0 || index > _permanentEmployeeList.Count)
            {
                return null;
            }
            else
            {
                var updated = employee;
                var existing = theEmployee;

                if (updated.Name == "string") theEmployee.Name = existing.Name;
                else theEmployee.Name = updated.Name;
                
                if (updated.Salary == 0) theEmployee.Salary = existing.Salary; 
                else theEmployee.Salary = updated.Salary; 
                
                if (updated.Bonus == 0) theEmployee.Bonus = existing.Bonus; 
                else theEmployee.Bonus = updated.Bonus; 
                
                if (updated.HoursWorked == 0) theEmployee.HoursWorked = existing.HoursWorked; 
                else theEmployee.HoursWorked = updated.HoursWorked; 
            }

            return employee;
        }

        public bool Delete(int id)
        {
            var employee = GetEmployee(id);
            
            if (employee == null)
            {
                return false;
            }
           
            _permanentEmployeeList.Remove(employee!);
            return true;
        }

        public IEnumerable<PermanentEmployee> GetAll()  
        {
            return _permanentEmployeeList;
        }
    }
}
