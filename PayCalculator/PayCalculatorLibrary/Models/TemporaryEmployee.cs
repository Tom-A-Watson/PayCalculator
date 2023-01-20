namespace PayCalculator.Models
{
    public class TemporaryEmployee : Employee
    {
        public decimal DayRate { get; set; }
        public int WeeksWorked { get; set; }

        public override string ToString()
        {
            return $"\nID: {Id} \nName: {Name} \nContract Type: {ContractType.Temporary} \nDay Rate: {DayRate} \nWeeks Worked: {WeeksWorked}" + "\n";
        }
    }
}
