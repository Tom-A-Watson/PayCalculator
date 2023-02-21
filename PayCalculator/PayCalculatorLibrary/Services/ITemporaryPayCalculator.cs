namespace PayCalculatorLibrary.Services
{
    public interface ITemporaryPayCalculator
    {
        decimal TotalAnnualPay(decimal dayRate, int weeksWorked);

        decimal HourlyRate(decimal dayRate);
    }
}