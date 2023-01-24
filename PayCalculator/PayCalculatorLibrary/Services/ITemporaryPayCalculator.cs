namespace PayCalculatorLibrary.Services
{
    public interface ITemporaryPayCalculator
    {
        decimal TotalAnnualPay(decimal DayRate, int WeeksWorked);

        decimal HourlyRate(decimal DayRate);
    }
}