namespace PayCalculator
{
    class Program
    {
        public static List<PermanentEmployee> _permanentEmployees = new List<PermanentEmployee>()
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

        public static List<TemporaryEmployee> _temporaryEmployees = new List<TemporaryEmployee>()
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
            foreach (var permanentEmployee in _permanentEmployees)
            {
                PrintDetails(permanentEmployee);
            }

            foreach (var temporaryEmployee in _temporaryEmployees)
            {
                PrintDetails(temporaryEmployee);
            }
        }

        static void GetEmployee()
        {
            Console.WriteLine("\nEnter the ID of the employee you would like to view\n");
            var idInput = Console.ReadLine()!;
            bool idIsInvalid = !Int32.TryParse(idInput, out int validID);

            foreach (var permanentEmployee in _permanentEmployees)
            {
                if (validID == permanentEmployee.Id)
                {
                    PrintDetails(permanentEmployee);
                    return;
                }
            }

            foreach (var temporaryEmployee in _temporaryEmployees)
            {
                if (validID == temporaryEmployee.Id)
                {
                    PrintDetails(temporaryEmployee);
                    return;
                }
            }

            if (idIsInvalid)
            {
                Console.WriteLine("\nInvalid input! Please enter a number");
                return;
            }

            if (validID < 1 || validID > _permanentEmployees.Count + _temporaryEmployees.Count)
            {
                Console.WriteLine("\nInvalid ID! Please enter a valid one");
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