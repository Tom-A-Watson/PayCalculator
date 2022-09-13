namespace PayCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Employee e = new Employee("Jeff", 27000.00, 3000.00);
            Console.WriteLine($"{e.Name}'s total annual pay is: \t £{String.Format("{0:0.00}", e.TotalAnnualPay())}");
            Console.WriteLine($"{e.Name}'s hourly rate is: \t\t £{String.Format("{0:0.00}", e.HourlyPay())}");
        }
    }
}