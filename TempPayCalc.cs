namespace PayCalculator
{
    public class TempPayCalc : TemporaryEmployee
    {
        public decimal TotalAnnualPay()
        {
            return DayRate * 5 * WeeksWorked;
        }

        public decimal HourlyRate()
        {
            return DayRate / 7;
        }
    }
}
