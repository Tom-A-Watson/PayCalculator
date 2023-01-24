namespace PayCalculatorLibrary.Services
{
    public class TemporaryPayCalculator : ITemporaryPayCalculator
    {
        public decimal TotalAnnualPay(decimal DayRate, int WeeksWorked)
        {
            return DayRate * 5 * WeeksWorked;
        }

        public decimal HourlyRate(decimal DayRate)
        {
            return DayRate / 7;
        }
    }
}
