namespace PayCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Employee emp1 = new Employee("Jeff", 27000, 3000);
            Console.WriteLine($"\nEnter 'annual' to see {emp1.Name}'s annual salary.\nOr enter 'hourly' to see his annual rate.");
            var options = Console.ReadLine();

            switch (options)
            {
                case "annual":
                    Console.WriteLine($"{emp1.Name}'s total annual pay is: \t £" + Math.Round(emp1.TotalAnnualPay(), 2));
                    break;
                case "hourly":
                    Console.WriteLine($"{emp1.Name}'s hourly rate is: \t\t £" + Math.Round(emp1.HourlyPay(), 2));
                    break;
            }
        }
    }
}