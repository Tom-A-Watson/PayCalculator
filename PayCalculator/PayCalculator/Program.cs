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
                HoursWorked = 1820
            },

            new PermanentEmployee()
            {
                Name = "John Smith",
                Id = 002,
                ContractType = "Permanent",
                Salary = 35000,
                Bonus = 1000,
                HoursWorked = 1820
            }
        };

        public static List<TemporaryEmployee> _temporaryEmployees = new List<TemporaryEmployee>()
        {
            new TemporaryEmployee()
            {
                Name = "Matt Burns",
                Id = 003,
                ContractType = "Temporary",
                DayRate = 250,
                WeeksWorked = 48
            },

            new TemporaryEmployee()
            {
                Name = "Emily Sanders",
                Id = 004,
                ContractType = "Temporary",
                DayRate = 250,
                WeeksWorked = 48
            }
        };

        static void Main(string[] args)
        {
            bool quitApp = false;
            while (!quitApp)
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
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        break;
                }

                Console.WriteLine("\nPress enter to continue");
                Console.ReadLine();
            }
        }

        static void GetAllInfo()
        {
            Console.Clear();

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
            Console.Clear();
            Console.WriteLine("\nEnter the ID of the employee you would like to view\n");
            var idInput = Console.ReadLine()!;
            bool idIsInvalid = !Int32.TryParse(idInput, out int validID);

            if (idIsInvalid)
            {
                Console.WriteLine("\nInvalid input! Please enter a number");
                return;
            }

            if (validID < 1 || validID > _permanentEmployees.Count + _temporaryEmployees.Count)
            {
                Console.WriteLine("\nID not found! Please enter a valid ID");
            }

            foreach (var permanentEmployee in _permanentEmployees)
            {
                if (validID == permanentEmployee.Id)
                {
                    PrintDetails(permanentEmployee);
                    Console.WriteLine($"\nWould you like to view {permanentEmployee.Name}'s total annual salary [1] or hourly rate [2]?\n");
                    string? option = Console.ReadLine();
                    PermPayCalc permEmployeePayCalculator = new();

                    switch (option)
                    {
                        case "1":
                            Console.WriteLine($"\n{permanentEmployee.Name}'s salary is: £" +
                                $"{Math.Round(permEmployeePayCalculator.TotalAnnualPay(permanentEmployee.Salary, permanentEmployee.Bonus), 2)}");
                            break;
                        case "2":
                            Console.WriteLine($"\n{permanentEmployee.Name}'s hourly rate is: £" + 
                                $"{Math.Round(permEmployeePayCalculator.HourlyRate(permanentEmployee.Salary, permanentEmployee.HoursWorked), 2)}");
                            break;
                        default:
                            Console.WriteLine("\nInvalid input!");
                            break;
                    }
                }
            }

            foreach (var temporaryEmployee in _temporaryEmployees)
            {
                if (validID == temporaryEmployee.Id)
                {
                    PrintDetails(temporaryEmployee);
                    Console.WriteLine($"\nWould you like to view {temporaryEmployee.Name}'s total annual salary [1] or hourly rate [2]?\n");
                    string? option = Console.ReadLine();
                    TempPayCalc tempEmployeePayCalculator = new();

                    switch (option)
                    {
                        case "1":
                            Console.WriteLine($"\n{temporaryEmployee.Name}'s salary is: £" + 
                                $"{Math.Round(tempEmployeePayCalculator.TotalAnnualPay(temporaryEmployee.DayRate, temporaryEmployee.WeeksWorked), 2)}");
                            break;
                        case "2":
                            Console.WriteLine($"\n{temporaryEmployee.Name}'s hourly rate is: £" + 
                                $"{Math.Round(tempEmployeePayCalculator.HourlyRate(temporaryEmployee.DayRate), 2)}");
                            break;
                        default:
                            Console.WriteLine("\nInvalid input!");
                            break;
                    }
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