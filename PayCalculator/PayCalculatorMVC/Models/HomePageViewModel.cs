using PayCalculatorLibrary.Models;

namespace PayCalculatorMVC.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<PermanentEmployee> PermEmployeeList { get; set; }
        public IEnumerable<TemporaryEmployee> TempEmployeeList { get; set; }
    }
}