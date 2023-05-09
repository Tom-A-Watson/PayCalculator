namespace PayCalculatorLibrary.Services
{
    public class TimeCalculator : ITimeCalculator
    {
        public int HoursWorked(DateTime startDate)
        {
            var currentDate = DateTime.Now.Date;
            startDate = startDate.Date;

            if (startDate > currentDate) 
            {
                throw new ArgumentException("Cannot calculate hours worked as the employee has not started work yet!");
            }

            TimeSpan span = currentDate - startDate;
            var businessDays = span.Days + 1;
            var fullWeekCount = businessDays / 7;

            // Check if there are weekends during the time outside of the full weeks
            if (businessDays > fullWeekCount * 7) 
            { 
                // Check if 1 or both weekend days are in the time period remaining after subtracting complete weeks
                var startDateDayOfWeek = (int) startDate.DayOfWeek;
                var currentDayOfWeek = (int) currentDate.DayOfWeek;
                if (currentDayOfWeek < startDateDayOfWeek) { currentDayOfWeek += 7; }
                
                if (startDateDayOfWeek <= 6)
                {
                    // Both Saturday and Sunday are in the remaining time interval
                    if (currentDayOfWeek >= 7) { businessDays -= 2; }
                    // Only Saturday is in the interval
                    else if (currentDayOfWeek >= 6) { businessDays -= 1; }
                }

                // Subtract the weekends during the full weeks in the interval
                businessDays -= fullWeekCount * 2;
            }
            var hoursWorked = businessDays * 7;
            return hoursWorked;
        }
    }



























}