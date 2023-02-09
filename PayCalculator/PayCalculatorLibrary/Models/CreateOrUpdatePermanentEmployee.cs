namespace PayCalculatorLibrary.Models
{
    public class CreateOrUpdatePermanentEmployee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public int HoursWorked { get; set; }
    }
}
