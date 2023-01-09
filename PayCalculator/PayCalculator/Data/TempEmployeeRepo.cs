using PayCalculator.Models;

namespace PayCalculator.Data
{
    public class TempEmployeeRepo : IEmployeeRepo<TemporaryEmployee>
    {
        private List<TemporaryEmployee> _temporaryEmployeeList;
        
        public TempEmployeeRepo()
        {
            _temporaryEmployeeList = new List<TemporaryEmployee>()
            {
                new TemporaryEmployee()
                {
                    Name = "Matt Burns",
                    Id = 003,
                    ContractType = "Temporary",
                    DayRate = 250,
                    WeeksWorked = 48
                },

                new TemporaryEmployee()
                {
                    Name = "Emily Sanders",
                    Id = 004,
                    ContractType = "Temporary",
                    DayRate = 250,
                    WeeksWorked = 48
                }
            };
        }

        public TemporaryEmployee Create(TemporaryEmployee employee)
        {
            TemporaryEmployee newEmployee = new();
            Random r = new();

            newEmployee.Id = r.Next(3, 1000);
            newEmployee.Name = employee.Name;
            newEmployee.DayRate = employee.DayRate;
            newEmployee.WeeksWorked = employee.WeeksWorked;
            return newEmployee;
        }

        public TemporaryEmployee GetEmployee(int id)    // Read
        {
            var employee = _temporaryEmployeeList.SingleOrDefault(x => x.Id == id);
            return employee;
        }

        public TemporaryEmployee? Update(TemporaryEmployee employee)
        {
            int index = _temporaryEmployeeList.FindIndex(x => x.Id == employee.Id);

            if (index < 0 || index > _temporaryEmployeeList.Count)
            {
                return null;
            }
            else
            {
                _temporaryEmployeeList[index].Name = employee.Name;
                _temporaryEmployeeList[index].DayRate = employee.DayRate;
                _temporaryEmployeeList[index].WeeksWorked = employee.WeeksWorked;
            }

            var Employee = GetEmployee(employee.Id);
            return Employee;
        }

        public bool Delete(int id)
        {
            _temporaryEmployeeList.Remove(GetEmployee(id));
            return true;
        }

        public IEnumerable<TemporaryEmployee> GetAll()
        {
            return _temporaryEmployeeList;
        }
    }
}
