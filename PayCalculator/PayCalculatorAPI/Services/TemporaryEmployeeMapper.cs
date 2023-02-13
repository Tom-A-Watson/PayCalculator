using PayCalculatorLibrary.Models;

namespace PayCalculatorAPI.Services
{
    public class TemporaryEmployeeMapper : ITemporaryEmployeeMapper
    {
        public TemporaryEmployee Map(CreateOrUpdateTemporaryEmployee model)
        {
            var tempEmployee = new TemporaryEmployee();
            tempEmployee.Name = model.Name;
            tempEmployee.DayRate = model.DayRate;
            tempEmployee.WeeksWorked = model.WeeksWorked;
            return tempEmployee;
        }
    }
}
