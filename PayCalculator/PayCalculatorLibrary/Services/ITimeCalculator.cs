namespace PayCalculatorLibrary.Services
{
    public interface ITimeCalculator
    {
        int DaysWorked(DateTime startDate, DateTime currentDate, int daysInAWeek = 7);
        int HoursWorked(DateTime startDate, DateTime currentDate);
        int WeeksWorked(DateTime startDate, DateTime currentDate);
    }
}
