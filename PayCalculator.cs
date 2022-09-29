namespace PayCalculator
{
    public class PayCalculator : PermanentEmployee
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
