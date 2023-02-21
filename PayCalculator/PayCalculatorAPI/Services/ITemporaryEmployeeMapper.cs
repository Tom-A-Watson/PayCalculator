using PayCalculatorLibrary.Models;

namespace PayCalculatorAPI.Services
{
    public interface ITemporaryEmployeeMapper
    {
        TemporaryEmployee Map(CreateOrUpdateTemporaryEmployee model);
    }
}