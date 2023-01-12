using PayCalculator.Models;

namespace PayCalculator.Repositories
{
    public class TemporaryEmployeeRepository : IEmployeeRepository<TemporaryEmployee>
    {
        private List<TemporaryEmployee> _temporaryEmployeeList;
        
        public TemporaryEmployeeRepository()
        {
            _temporaryEmployeeList = new List<TemporaryEmployee>()
            {
                new TemporaryEmployee()
                {
                    Name = "Matt Burns",
                    Id = 003,
                    Contract = ContractType.Temporary,
                    DayRate = 250,
                    WeeksWorked = 48
                },

                new TemporaryEmployee()
                {
                    Name = "Emily Sanders",
                    Id = 004,
                    Contract = ContractType.Temporary,
                    DayRate = 250,
                    WeeksWorked = 48
                }
            };
        }

        public TemporaryEmployee Create(TemporaryEmployee employee)
        {
            Random r = new();
            
            employee.Id = r.Next(5, 1000);
            employee.Contract = ContractType.Temporary;
            _temporaryEmployeeList.Add(employee);
            return employee;
        }

        public TemporaryEmployee GetEmployee(int id)
        {
            var employee = _temporaryEmployeeList.SingleOrDefault(x => x.Id == id);
            return employee!;
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
                _temporaryEmployeeList.Remove(employee);
                return true;
            }
        }

        public IEnumerable<TemporaryEmployee> GetAll()
        {
            return _temporaryEmployeeList;
        }
    }
}
