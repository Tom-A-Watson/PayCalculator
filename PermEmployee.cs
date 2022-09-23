namespace PayCalculator
{
    public class PermEmployee : Employee
    {
        private decimal Bonus { get; set; }

        public PermEmployee(decimal bonus)
        {
            Bonus = bonus;
        }

        public decimal TotalAnnualPay()
        {
            return Salary + Bonus;
        }

        public decimal HourlyRate()
        {
            return Salary / 1820;
        }
    }
}
