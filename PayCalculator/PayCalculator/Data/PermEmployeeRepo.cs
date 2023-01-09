using PayCalculator.Models;

namespace PayCalculator.Data
{
    public class PermEmployeeRepo : IEmployeeRepo<PermanentEmployee>
    {
        public List<PermanentEmployee> _permanentEmployeeList;

        public PermEmployeeRepo()
        {
            _permanentEmployeeList = new List<PermanentEmployee>()
            {
                new PermanentEmployee()
                {
                    Name = "Joe Bloggs",
                    Id = 001,
                    ContractType = "Permanent",
                    Salary = 25000,
                    Bonus = 2500,
                    HoursWorked = 1820
                },

                new PermanentEmployee()
                {
                    Name = "John Smith",
                    Id = 002,
                    ContractType = "Permanent",
                    Salary = 35000,
                    Bonus = 1000,
                    HoursWorked = 1820
                }
            };
        }

        public PermanentEmployee Create(PermanentEmployee employee)
        {
            PermanentEmployee newEmployee = new();
            Random r = new();

            newEmployee.Id = r.Next(3, 1000);
            newEmployee.Name = employee.Name;
            newEmployee.Salary = employee.Salary;
            newEmployee.Bonus = employee.Bonus;
            newEmployee.HoursWorked = employee.HoursWorked;
            _permanentEmployeeList.Add(newEmployee);
            return newEmployee;
        }

        public PermanentEmployee GetEmployee(int id)    // Read
        {
            var employee = _permanentEmployeeList.SingleOrDefault(x => x.Id == id);
            return employee!;
        }

        public PermanentEmployee? Update(PermanentEmployee employee)
        {
            int index = _permanentEmployeeList.FindIndex(x => x.Id == employee.Id);

            if (index < 0 || index > _permanentEmployeeList.Count)
            {
                return null;
            }
            else
            {
                _permanentEmployeeList[index].Name = employee.Name;
                _permanentEmployeeList[index].Salary = employee.Salary;
                _permanentEmployeeList[index].Bonus = employee.Bonus;
                _permanentEmployeeList[index].HoursWorked = employee.HoursWorked;
            }

            var Employee = GetEmployee(employee.Id);
            return Employee;
        }

        public bool Delete(int id)
        {
            _permanentEmployeeList.Remove(GetEmployee(id));
            return true;
        }

        public IEnumerable<PermanentEmployee> GetAll()  
        {
            return _permanentEmployeeList;
        }
    }
}
