namespace PayCalculatorLibrary.Models
{
    public class CreateOrUpdateTemporaryEmployee
    {
        public string Name { get; set; }
        public decimal DayRate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
