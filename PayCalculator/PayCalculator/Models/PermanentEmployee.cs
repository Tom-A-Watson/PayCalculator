namespace PayCalculator.Models
{
    public class PermanentEmployee : Employee
    {
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public int HoursWorked { get; set; }
    }
}
