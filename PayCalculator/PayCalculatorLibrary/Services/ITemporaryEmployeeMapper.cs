using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Services
{
    public interface ITemporaryEmployeeMapper
    {
        TemporaryEmployee Map(CreateOrUpdateTemporaryEmployee model);
    }
}