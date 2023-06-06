using PayCalculatorLibrary.Models;
using PayCalculatorMVC.Enums;

namespace PayCalculatorMVC.Models
{
    public class TempEmployeeAlertsViewModel
    {
        public IEnumerable<TemporaryEmployee> Employees { get; set; }
        public Alerts? Alerts { get; set; }
        public string? Name { get; set; }
    }
}
