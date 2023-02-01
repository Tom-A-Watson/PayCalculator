namespace PayCalculatorLibrary.Services
{
    public interface IPermanentPayCalculator
    {
        decimal TotalAnnualPay(decimal salary, decimal bonus);

        decimal HourlyRate(decimal salary, int hoursWorked);
    }
}