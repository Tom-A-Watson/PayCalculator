using PayCalculator.Models;

namespace PayCalculator.Repositories
{
    public class PermanentEmployeeRepository : IEmployeeRepo<PermanentEmployee>
    {
        public List<PermanentEmployee> _permanentEmployeeList;

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

        public PermanentEmployee GetEmployee(int id)
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

            var updatedEmployee = GetEmployee(employee.Id);
            return updatedEmployee;
        }

        public bool Delete(int id)
        {
            var employee = GetEmployee(id);
            
            if (GetEmployee(id) == null)
            {
                return false;
            }
            else
            {
                _permanentEmployeeList.Remove(employee);
                return true;
            }
        }

        public IEnumerable<PermanentEmployee> GetAll()  
        {
            return _permanentEmployeeList;
        }
    }
}
