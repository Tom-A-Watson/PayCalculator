using PayCalculatorLibrary.Models;

namespace PayCalculatorAPI.Services
{
    public class PermanentEmployeeMapper : IPermanentEmployeeMapper
    {
        public PermanentEmployee Map(CreateOrUpdatePermanentEmployee model)
        {
            var permEmployee = new PermanentEmployee();
            permEmployee.Name = model.Name;
            permEmployee.Salary = model.Salary;
            permEmployee.Bonus = model.Bonus;
            permEmployee.HoursWorked = model.HoursWorked;
            return permEmployee;
        }
    }
}
