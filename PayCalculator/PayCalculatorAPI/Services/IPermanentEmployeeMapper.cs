using PayCalculatorLibrary.Models;

namespace PayCalculatorAPI.Services
{
    public interface IPermanentEmployeeMapper
    {
        PermanentEmployee Map(CreateOrUpdatePermanentEmployee model);
    }
}