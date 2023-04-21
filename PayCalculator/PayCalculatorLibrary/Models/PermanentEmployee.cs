namespace PayCalculatorLibrary.Models
{
    public class PermanentEmployee : Employee
    {
        public decimal? Salary { get; set; }
        public decimal? Bonus { get; set; }
        public int? HoursWorked { get; set; }

        public override string ToString()
        {
            return $"\nID: {Id} \nName: {Name} \nContract Type: {ContractType.Permanent} \nSalary: {Salary} \nBonus: {Bonus} \nHours Worked: {HoursWorked}" + "\n";
        }
    }
}
