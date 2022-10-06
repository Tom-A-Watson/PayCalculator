namespace PayCalculator
{
    public class PermPayCalc : PermanentEmployee
    {
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
