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

        public static List<TemporaryEmployee> _tempEmps = new List<TemporaryEmployee>()
        {
            new TemporaryEmployee()
            {
                Name = "Jeff Burns",
                Id = 003,
                ContractType = "Temporary",
                DayRate = 200,
                WeeksWorked = 42
            },

            new TemporaryEmployee()
            {
                Name = "Emily Sanders",
                Id = 004,
                ContractType = "Temporary",
                DayRate = 250,
                WeeksWorked = 38
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

            foreach (var temporaryEmployee in _tempEmps)
            {
                PrintDetails(temporaryEmployee);
            }
        }

        static void GetEmployee()
        {
            Console.WriteLine("\nEnter the ID of the employee you would like to view\n");
            var idInput = Int32.Parse(Console.ReadLine()!);

            foreach (var permanentEmployee in _permEmps)
            {
                if (idInput == permanentEmployee.Id)
                {
                    PrintDetails(permanentEmployee);
                }
                else
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID");
                    return;
                }
            }
        }

        static void PrintDetails(PermanentEmployee permanentEmployee)
        {
            Console.WriteLine($"\nStaff ID: {permanentEmployee?.Id} \nStaff Name: {permanentEmployee?.Name} \nStaff Contract Type: {permanentEmployee?.ContractType}" +
                $" \nStaff Salary: {permanentEmployee?.Salary} \nStaff Bonus: {permanentEmployee?.Bonus} \nHours Worked: {permanentEmployee?.HoursWorked}");
        }

        static void PrintDetails(TemporaryEmployee temporaryEmployee)
        {
            Console.WriteLine($"\nStaff ID: {temporaryEmployee?.Id} \nStaff Name: {temporaryEmployee?.Name} \nStaff Contract Type: {temporaryEmployee?.ContractType}" +
                $" \nWeeks Worked: {temporaryEmployee?.WeeksWorked} \nStaff Day Rate: {temporaryEmployee?.DayRate}");
        }
    }
}