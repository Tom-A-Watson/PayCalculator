namespace PayCalculatorLibrary.Services
{
    public class TimeCalculator : ITimeCalculator
    {
        public int DaysWorked(DateTime startDate, DateTime currentDate, int daysInAWeek = 7)
        {
            currentDate = currentDate.Date;
            startDate = startDate.Date;

            if (startDate > currentDate)
            {
                return 0;
            }

            TimeSpan span = currentDate - startDate;
            var businessDays = span.Days + 1;

            if (businessDays % daysInAWeek > 0)
            {
                var businessDaysCount = 0;

                for (DateTime date = startDate; date <= currentDate; date = date.AddDays(1))
                {
                    if (IsWeekDay(date))
                    {
                        businessDaysCount++;
                    }
                }

                businessDays = businessDaysCount;
            }
            else if (businessDays % daysInAWeek == 0)
            {
                var fullWeekCount = businessDays / daysInAWeek;
                businessDays -= fullWeekCount * 2;
            }

            return businessDays;
        }

        public int HoursWorked(DateTime startDate, DateTime currentDate)
        {
            var hoursWorked = DaysWorked(startDate, currentDate) * 7;
            return hoursWorked;
        }

        public int WeeksWorked(DateTime startDate, DateTime currentDate)
        {
            var weeksWorked = DaysWorked(startDate, currentDate) / 7;
            return weeksWorked;
        }

        private static bool IsWeekDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Tuesday || date.DayOfWeek == DayOfWeek.Wednesday ||
                        date.DayOfWeek == DayOfWeek.Thursday || date.DayOfWeek == DayOfWeek.Friday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}