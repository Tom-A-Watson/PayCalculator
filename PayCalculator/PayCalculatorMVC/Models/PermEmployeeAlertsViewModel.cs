using PayCalculatorLibrary.Models;
using PayCalculatorMVC.Enums;

namespace PayCalculatorMVC.Models
{
    public class PermEmployeeAlertsViewModel 
    {
        public IEnumerable<PermanentEmployee> Employees { get; set; }
        public Alerts? Alerts { get; set; }
        public string? Name { get; set; }
    }
}