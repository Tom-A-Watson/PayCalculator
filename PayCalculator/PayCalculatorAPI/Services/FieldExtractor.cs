using PayCalculatorLibrary.Models;

namespace PayCalculatorAPI.Services
{
    public class FieldExtractor
    {
        TemporaryEmployee tempEmployee;
        PermanentEmployee permEmployee;

        public PermanentEmployee ExtractPermEmployeeDetails(CreateOrUpdatePermanentEmployee model)
        {
            permEmployee = new();
            permEmployee.Name = model.Name;
            permEmployee.Salary = model.Salary;
            permEmployee.Bonus = model.Bonus;
            permEmployee.HoursWorked = model.HoursWorked;
            return permEmployee;
        }

        public TemporaryEmployee ExtractTempEmployeeDetails(CreateOrUpdateTemporaryEmployee model)
        {
            tempEmployee = new();
            tempEmployee.Name = model.Name;
            tempEmployee.DayRate = model.DayRate;
            tempEmployee.WeeksWorked = model.WeeksWorked;
            return tempEmployee;
        }
    }
}
