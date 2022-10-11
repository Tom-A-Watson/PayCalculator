namespace PayCalculator
{
    class Program
    {
        public static List<PermanentEmployee> _permEmps = new List<PermanentEmployee>()
        {
            new PermanentEmployee()
            {
                Name = "Joe Bloggs",
                Id = 001,
                ContractType = "Permanent",
                Salary = 25000,
                Bonus = 2500,
                HoursWorked = 35
            },

            new PermanentEmployee()
            {
                Name = "John Smith",
                Id = 002,
                ContractType = "Permanent",
                Salary = 35000,
                Bonus = 1000,
                HoursWorked = 35
            }
        };
        
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1 to get all employee information \nEnter 2 to get a single employee's information \nEnter 3 to exit\n");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    GetAllInfo(); 
                    break;
                case "2":
                    GetEmployee(); 
                    break;
                case "3": 
                    return;
                default:
                    Console.WriteLine("\nInvalid input!");
                    break;
            }
        }

        static void GetAllInfo()
        {
            foreach (var permanentEmployee in _permEmps)
            {
                PrintDetails(permanentEmployee);
            }
        }

        static void GetEmployee()
        {
            Console.WriteLine("\nEnter the ID of the employee you would like to view\n");
            
            string? idInput = Console.ReadLine();
            PrintDetails(_permEmps[Int32.Parse(idInput) - 1]);
        }
        
        static void PrintDetails(PermanentEmployee permanentEmployee)
        {
            Console.WriteLine($"\nStaff ID: {permanentEmployee?.Id} \nStaff Name: {permanentEmployee?.Name} \nStaff Contract Type: {permanentEmployee?.ContractType}" +
                $" \nStaff Salary: {permanentEmployee?.Salary} \nStaff Bonus: {permanentEmployee?.Bonus} \nHours Worked: {permanentEmployee?.HoursWorked}");
        }
    }
}