namespace PayCalculatorLibrary.Models
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public decimal TotalAnnualPay { get; set; }
        public decimal HourlyRate { get; set; }
        public DateOnly StartDate { get; set; }
        public ContractType Contract { get; set; }
    }
}
