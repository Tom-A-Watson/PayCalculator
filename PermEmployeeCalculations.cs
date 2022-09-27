namespace PayCalculator
{
    public class PermEmployeeCalculations : Employee
    {
        public string? ContractType { get; set; }
        public decimal Salary { get; set; }
        public int HoursWorked { get; set; }

        public decimal TotalAnnualPay()
        {
            return Salary + Bonus;
        }

        public decimal HourlyRate()
        {
            return Salary / 1820;
        }
    }
}
