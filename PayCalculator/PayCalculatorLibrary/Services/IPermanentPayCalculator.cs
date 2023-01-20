namespace PayCalculatorLibrary.Services
{
    public interface IPermanentPayCalculator
    {
        decimal TotalAnnualPay(decimal Salary, decimal Bonus);

        decimal HourlyRate(decimal Salary, int HoursWorked);
    }
}
