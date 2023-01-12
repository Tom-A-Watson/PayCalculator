namespace PayCalculator.Services
{
    public class PermanentPayCalculator
    {
        public decimal TotalAnnualPay(decimal Salary, decimal Bonus)
        {
            return Salary + Bonus;
        }

        public decimal HourlyRate(decimal Salary, int HoursWorked)
        {
            return Salary / HoursWorked;
        }
    }
}
