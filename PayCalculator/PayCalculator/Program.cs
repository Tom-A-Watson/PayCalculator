using PayCalculator.Data;
using PayCalculator.Models;
using PayCalculator.Services;

namespace PayCalculator
{
    class Program
    {
        private static IEmployeeRepo<PermanentEmployee> _perm = new PermEmployeeRepo();
        private static IEmployeeRepo<TemporaryEmployee> _temp = new TempEmployeeRepo();

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
                        UpdateEmployee();
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!\n");
                        break;
                }

                Console.WriteLine("\nPress enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void UpdateEmployee()
        {
            Console.WriteLine("\nEnter the ID of the staff member you would like to update\n");
            int option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nDo you want to update this entry? Y/N");
            Console.WriteLine(_perm.GetEmployee(option));
            Console.WriteLine(_temp.GetEmployee(option));
            var yesOrNo = Console.ReadLine();

            if (yesOrNo == "Y" || yesOrNo == "y")
            {
                Console.WriteLine("Please select the field you would like to update");
                Console.WriteLine("1: Name    2: Annual Salary    3: Annual Bonus    4: Hours Worked\n");
                var input = Console.ReadLine();
                var currentData = _perm.GetEmployee(option);

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter the new name\n");
                        var updatedName = Console.ReadLine();
                        currentData.Name = updatedName!;
                        _perm.Update(currentData);
                        break;
                    case "2":
                        Console.WriteLine("Enter the new annual salary\n");
                        var updatedSalary = Convert.ToDecimal(Console.ReadLine());
                        currentData.Salary = updatedSalary;
                        _perm.Update(currentData);
                        break;
                    case "3":
                        Console.WriteLine("Enter the new annual bonus\n");
                        var updatedBonus = Convert.ToDecimal(Console.ReadLine());
                        currentData.Bonus = updatedBonus;
                        _perm.Update(currentData);
                        break;
                    case "4":
                        Console.WriteLine("Enter the new annual salary\n");
                        var updatedHoursWorked = Convert.ToInt32(Console.ReadLine());
                        currentData.Salary = updatedHoursWorked;
                        _perm.Update(currentData);
                        break;
                }
            }
        }

        static void GetAllInfo()
        {
            Console.Clear();
            Console.WriteLine("\n-=-=-=-=- Permanent Employees -=-=-=-=-");
            Console.WriteLine(string.Concat(_perm.GetAll()));
            Console.WriteLine("-=-=-=-=- Temporary Employees -=-=-=-=-");
            Console.WriteLine(string.Concat(_temp.GetAll()));
        }

        static void GetEmployee()
        {
            Console.Clear();
            Console.WriteLine("\nEnter the ID of the employee you would like to view\n");
            bool idIsInvalid = !Int32.TryParse(Console.ReadLine(), out int validID);
            var permanentEmployee = _perm.GetEmployee(validID);
            var temporaryEmployee = _temp.GetEmployee(validID);

            if (idIsInvalid)
            {
                Console.WriteLine("\nNon-numerical input is invalid! Please enter a number");
                return;
            }

            if (validID <= _perm.GetAll().Count())
            {
                Console.WriteLine(permanentEmployee);
                Console.WriteLine($"Would you like to view {permanentEmployee.Name}'s total annual salary [1] or hourly rate [2]?\n");
                PermPayCalc permEmployeePayCalculator = new();

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine($"\n{permanentEmployee.Name}'s salary is: £" +
                        $"{Math.Round(permEmployeePayCalculator.TotalAnnualPay(permanentEmployee.Salary, permanentEmployee.Bonus), 2)}\n");
                        break;
                    case "2":
                        Console.WriteLine($"\n{permanentEmployee.Name}'s hourly rate is: £" +
                        $"{Math.Round(permEmployeePayCalculator.HourlyRate(permanentEmployee.Salary, permanentEmployee.HoursWorked), 2)}\n");
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        break;
                }
            }
            else if (validID <= _temp.GetAll().Count())
            {
                Console.WriteLine(temporaryEmployee);
                Console.WriteLine($"\nWould you like to view {temporaryEmployee.Name}'s total annual salary [1] or hourly rate [2]?\n");
                TempPayCalc tempEmployeePayCalculator = new();

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine($"\n{temporaryEmployee.Name}'s salary is: £" +
                        $"{Math.Round(tempEmployeePayCalculator.TotalAnnualPay(temporaryEmployee.DayRate, temporaryEmployee.WeeksWorked), 2)}\n");
                        break;
                    case "2":
                        Console.WriteLine($"\n{temporaryEmployee.Name}'s hourly rate is: £" +
                        $"{Math.Round(tempEmployeePayCalculator.HourlyRate(temporaryEmployee.DayRate), 2)}\n");
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        break;
                }
            }
        }
    }
}