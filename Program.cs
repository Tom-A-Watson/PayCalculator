namespace PayCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Employee e = new Employee("Jeff", 25000.00, 3000.00);
            Console.WriteLine("Jeff's total annual pay is: \t" + e.TotalAnnualPay());
            Console.WriteLine("His hourly rate is: \t\t" + e.HourlyPay());
        }
    }
}