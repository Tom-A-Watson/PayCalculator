using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Services
{
    public class TemporaryEmployeeMapper : ITemporaryEmployeeMapper
    {
        public TemporaryEmployee Map(CreateOrUpdateTemporaryEmployee model)
        {
            var tempEmployee = new TemporaryEmployee();
            tempEmployee.Name = model.Name;
            tempEmployee.DayRate = model.DayRate;
            tempEmployee.StartDate = model.StartDate;
            return tempEmployee;
        }
    }
}
