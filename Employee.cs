namespace PayCalculator
{
    public class Employee
    {
        string name;
        decimal annualSalary, bonus;

        public Employee(string name, decimal annualSalary, decimal bonus)
        {
            this.name = name;
            this.annualSalary = annualSalary;
            this.bonus = bonus;
        }

        public string Name
        {
            get { return name; }
        }

        public decimal TotalAnnualPay()
        {
            return annualSalary + bonus;
        }

        public decimal HourlyPay()
        {
            return annualSalary / 1820;
        }
    }
}
