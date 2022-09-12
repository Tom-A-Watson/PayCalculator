namespace PayCalculator
{
    public class Employee
    {
        string name;
        double annualSalary;
        double annualBonus;

        public Employee(string name, double annualSalary, double annualBonus)
        {
            this.name = name;
            this.annualSalary = annualSalary;
            this.annualBonus = annualBonus;
        }

        public double TotalAnnualPay()
        {
            return annualSalary += annualBonus;
        }

        public double HourlyPay()
        {
            return annualSalary / 1820;
        }
    }
}
