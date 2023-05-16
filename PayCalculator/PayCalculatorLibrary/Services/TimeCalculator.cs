namespace PayCalculatorLibrary.Services
{
    public class TimeCalculator : ITimeCalculator
    {
        public int HoursWorked(DateTime startDate, DateTime currentDate)
        {
            currentDate = currentDate.Date;
            startDate = startDate.Date;

            if (startDate > currentDate) 
            {
                throw new ArgumentException("Cannot calculate hours worked as the employee has not started work yet!");
            }

            TimeSpan span = currentDate - startDate;
            var businessDays = span.Days + 1;
            var daysInAWeek = 7;

            if (businessDays % daysInAWeek > 0) 
            { 
                var dates = new List<DateTime>();

                for (DateTime date = startDate; date <= currentDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Tuesday || date.DayOfWeek == DayOfWeek.Wednesday ||
                        date.DayOfWeek == DayOfWeek.Thursday || date.DayOfWeek == DayOfWeek.Friday)
                    {
                        dates.Add(date);
                    }
                }

                businessDays = dates.Count;
            }
            else if (businessDays % daysInAWeek == 0)
            {
                var fullWeekCount = businessDays / daysInAWeek;
                businessDays -= fullWeekCount * 2;
            }

            var hoursWorked = businessDays * 7;
            return hoursWorked;
        }
    }



























}