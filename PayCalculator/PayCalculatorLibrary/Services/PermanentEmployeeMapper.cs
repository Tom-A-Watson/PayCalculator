using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Services
{
    public class PermanentEmployeeMapper : IPermanentEmployeeMapper
    {
        public PermanentEmployee Map(CreateOrUpdatePermanentEmployee model)
        {
            var permEmployee = new PermanentEmployee();
            permEmployee.Name = model.Name;
            permEmployee.Salary = model.Salary;
            permEmployee.Bonus = model.Bonus;
            permEmployee.StartDate = model.StartDate;
            return permEmployee;
        }
    }
}
