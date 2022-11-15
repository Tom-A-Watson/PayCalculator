namespace PayCalculator
{
    public class PermPayCalc
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
