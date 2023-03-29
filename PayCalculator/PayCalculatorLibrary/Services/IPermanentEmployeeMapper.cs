using PayCalculatorLibrary.Models;

namespace PayCalculatorLibrary.Services
{
    public interface IPermanentEmployeeMapper
    {
        PermanentEmployee Map(CreateOrUpdatePermanentEmployee model);
    }
}