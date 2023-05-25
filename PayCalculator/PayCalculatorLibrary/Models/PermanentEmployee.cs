namespace PayCalculatorLibrary.Models
{
    public class PermanentEmployee : Employee
    {
        public decimal? Salary { get; set; }
        public decimal? Bonus { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} \nName: {Name} \nContract Type: {ContractType.Permanent} \nSalary: {Salary} \nBonus: {Bonus}" +
                $"\nStart Date: {StartDate.ToShortDateString()} \nHours Worked: {HoursWorked} \nTotal Annual Pay: {TotalAnnualPay} \nHourly Rate: {HourlyRate} \n";
        }
    }
}
