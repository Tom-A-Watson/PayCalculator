namespace PayCalculatorLibrary.Models
{
    public class TemporaryEmployee : Employee
    {
        public decimal DayRate { get; set; }
        public int WeeksWorked { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} \nName: {Name} \nContract Type: {ContractType.Temporary} \nDay Rate: {DayRate} \nStart Date: {StartDate.ToShortDateString()}" +
                $"\nWeeks Worked: {WeeksWorked} \nHours Worked: {HoursWorked} \nTotal Annual Pay: {TotalAnnualPay} \nHourly Rate: {HourlyRate} \n";
        }
    }
}
