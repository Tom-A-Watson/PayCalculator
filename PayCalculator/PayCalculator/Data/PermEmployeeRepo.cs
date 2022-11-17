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

        public PermanentEmployee Create(PermanentEmployee Employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PermanentEmployee> GetAll()
        {
            return _permanentEmployeeList;
        }

        public PermanentEmployee GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public PermanentEmployee Update(PermanentEmployee Employee)
        {
            throw new NotImplementedException();
        }
    }
}
