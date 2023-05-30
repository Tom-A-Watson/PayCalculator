using PayCalculatorMVC.Enums;

namespace PayCalculatorMVC.Models
{
    public class PermDeleteAndAlertViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Bonus { get; set; }
        public DateTime StartDate { get; set; }
        public int HoursWorked { get; set; }
        public Alerts Alerts { get; set; }
    }
}
