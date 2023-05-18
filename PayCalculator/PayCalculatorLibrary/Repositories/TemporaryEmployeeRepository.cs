using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Services;

namespace PayCalculatorLibrary.Repositories
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
                    StartDate = new DateTime(2022, 3, 23),
                    WeeksWorked = 48
                },

                new TemporaryEmployee()
                {
                    Name = "Emily Sanders",
                    Id = 004,
                    Contract = ContractType.Temporary,
                    DayRate = 350,
                    StartDate = new DateTime(2020, 1, 1),
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

        public TemporaryEmployee? GetEmployee(int id)
        {
            var employee = _temporaryEmployeeList.SingleOrDefault(x => x.Id == id);
            return employee;
        }

        public TemporaryEmployee? Update(TemporaryEmployee employee)
        {
            var existing = _temporaryEmployeeList.FirstOrDefault(x => x.Id == employee.Id);
            var updated = employee;

            if (updated.Name == "string") existing.Name = existing.Name;
            else existing.Name = updated.Name;
                
            if (updated.DayRate == 0) existing.DayRate = existing.DayRate;
            else existing.DayRate = updated.DayRate;
                
            if (updated.WeeksWorked == 0) existing.WeeksWorked = existing.WeeksWorked; 
            else existing.WeeksWorked = updated.WeeksWorked;

            return employee;
        }

        public bool Delete(int id)
        {
            var employee = GetEmployee(id);

            if (employee == null)
            {
                return false;
            }
           
            _temporaryEmployeeList.Remove(employee);
            return true;
        }

        public IEnumerable<TemporaryEmployee> GetAll()
        {
            return _temporaryEmployeeList;
        }
    }
}