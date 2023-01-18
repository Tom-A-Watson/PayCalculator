using PayCalculator.Repositories;
using PayCalculator.Models;
using PayCalculator.Services;

namespace PayCalculator
{
    class Program
    {
        private static IEmployeeRepository<PermanentEmployee> _permEmployeeRepo = new PermanentEmployeeRepository();
        private static IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo = new TemporaryEmployeeRepository();
        private static string enterID = "\nEnter the ID of the employee you would like to ";
        private static string confirmationDeclined = "\nConfirmation was declined, returning to the main menu";
        private static string nonNumericalError = "\nNon-numerical input is invalid! Please enter a number";
        private static string nullInputError = "The confirmation was left blank, returning to the main menu";

        static void Main(string[] args)
        {
            bool quitApp = false;
            while (!quitApp)
            {
                Console.WriteLine("Enter 1 to get all employee information\n \nEnter 2 to get a single employee's information\n \nEnter 3 to create a new employee\n" +
                    "\nEnter 4 to update an employee's details\n \nEnter 5 to delete an employee\n \nEnter 6 to exit\n");
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
                        CreateEmployee();
                        break;
                    case "4":
                        UpdateEmployee();
                        break;
                    case "5":
                        DeleteEmployee();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!\n");
                        break;
                }

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("\nWould you like to delete [1] a permanent or [2] a temporary employee?\n");
            var option = Console.ReadLine();
            string confirmation = "\nPlease confirm that you want to delete this entry (Y/N)\n";
            string deleted = " has been deleted from the database";

            if (option == "1")
            {
                Console.WriteLine(enterID + "delete\n");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(_permEmployeeRepo.GetEmployee(id) + confirmation);
                var input = Console.ReadLine()!.ToUpper();

                if (input == "Y" || input == "YES")
                {
                    Console.WriteLine("\n" + _permEmployeeRepo.GetEmployee(id)!.Name + deleted);
                    _permEmployeeRepo.Delete(id);
                    return;
                }
                else if (input == "N" || input == "NO")
                {
                    Console.WriteLine(confirmationDeclined);
                    return;
                } 
                else if (input == "")
                {
                    Console.WriteLine(nullInputError);
                    return;
                }
            }
            else if (option == "2")
            {
                Console.WriteLine(enterID + "delete\n");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(_tempEmployeeRepo.GetEmployee(id) + confirmation);
                var input = Console.ReadLine()!.ToUpper();

                if (input == "Y" || input == "YES")
                {
                    Console.WriteLine("\n" + _tempEmployeeRepo.GetEmployee(id)!.Name + deleted);
                    _tempEmployeeRepo.Delete(id);
                    return;
                }
                else if (input == "N" || input == "NO")
                {
                    Console.WriteLine(confirmationDeclined);
                    return;
                }
                else if (input == "")
                {
                    Console.WriteLine(nullInputError);
                    return;
                }
            }
        }

        static void CreateEmployee()
        {
            Console.Clear();
            Console.WriteLine("\nWould you like to create [1] a permanent or [2] a temporary employee?\n");
            var option = Console.ReadLine();
            string confirmation = "\nThe new entry is as follows:\n";
            
            if (option == "1")
            {
                PermanentEmployee permanentEmployee = new();
                Console.WriteLine("\nEnter the name of the new employee");
                permanentEmployee.Name = Console.ReadLine()!;
                Console.WriteLine("\nEnter the salary of the new employee");
                permanentEmployee.Salary = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("\nEnter the bonus of the new employee");
                permanentEmployee.Bonus = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("\nEnter the amount of hours the new employee has worked");
                permanentEmployee.HoursWorked = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(confirmation + _permEmployeeRepo.Create(permanentEmployee));
            }
            else if (option == "2")
            {
                TemporaryEmployee temporaryEmployee = new();
                Console.WriteLine("\nEnter the name of the new employee");
                temporaryEmployee.Name = Console.ReadLine()!;
                Console.WriteLine("\nEnter the day rate of the new employee");
                temporaryEmployee.DayRate = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("\nEnter the number of weeks this employee has worked");
                temporaryEmployee.WeeksWorked = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(confirmation + _tempEmployeeRepo.Create(temporaryEmployee));
            }
        }

        static void UpdateEmployee()
        {
            Console.Clear();
            Console.WriteLine("\nWould you like to update [1] a permanent employee or [2] a temporary employee?\n");
            var option = Console.ReadLine();
            string selectField = "\nPlease select the field you would like to update";
            string confirmation = "\nPlease confirm that you want to update this entry (Y/N)\n";
            string updated = "\nThe field has been updated";

            if (option == "1")
            {
                Console.WriteLine(enterID + "update\n");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(_permEmployeeRepo.GetEmployee(id) + confirmation);
                var permConfirmation = Console.ReadLine()!.ToUpper();

                if (permConfirmation == "Y" || permConfirmation == "YES")
                {
                    Console.WriteLine(selectField + "\n1: Name    2: Annual Salary    3: Annual Bonus    4: Hours Worked\n");
                    var input = Console.ReadLine();
                    var permEmployee = _permEmployeeRepo.GetEmployee(id);

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("\nEnter the new name\n");
                            var updatedName = Console.ReadLine();
                            permEmployee!.Name = updatedName!;
                            _permEmployeeRepo.Update(permEmployee);
                            break;
                        case "2":
                            Console.WriteLine("\nEnter the new annual salary\n");
                            bool salaryIsNotNumerical = !decimal.TryParse(Console.ReadLine(), out decimal updatedSalary);

                            if (salaryIsNotNumerical)
                            {
                                Console.WriteLine(nonNumericalError);
                                return;
                            }

                            permEmployee!.Salary = updatedSalary;
                            _permEmployeeRepo.Update(permEmployee);
                            break;
                        case "3":
                            Console.WriteLine("\nEnter the new annual bonus\n");
                            bool bonusIsNotNumerical = !decimal.TryParse(Console.ReadLine(), out decimal updatedBonus);
                            
                            if (bonusIsNotNumerical)
                            {
                                Console.WriteLine(nonNumericalError);
                                return;
                            }

                            permEmployee!.Bonus = updatedBonus;
                            _permEmployeeRepo.Update(permEmployee);
                            break;
                        case "4":
                            Console.WriteLine("\nEnter the new number of hours worked\n");
                            var updatedHoursWorked = Convert.ToInt32(Console.ReadLine());
                            permEmployee!.HoursWorked = updatedHoursWorked;
                            _permEmployeeRepo.Update(permEmployee);
                            break;
                    }

                    Console.WriteLine(updated);
                    return;
                }
                else if (permConfirmation == "")
                {
                    Console.WriteLine(nullInputError);
                    return;
                }
            }
            else if (option == "2")
            {
                Console.WriteLine(enterID + "update\n");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(_tempEmployeeRepo.GetEmployee(id) + confirmation);
                var tempConfirmation = Console.ReadLine()!.ToUpper()!;

                if (tempConfirmation == "Y" || tempConfirmation == "YES")
                {
                    Console.WriteLine(selectField + "\n1: Name    2: Day Rate    3: Weeks Worked\n");
                    var input = Console.ReadLine();
                    var tempEmployee = _tempEmployeeRepo.GetEmployee(id);

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("\nEnter the new name\n");
                            var updatedName = Console.ReadLine();
                            tempEmployee!.Name = updatedName!;
                            _tempEmployeeRepo.Update(tempEmployee);
                            break;
                        case "2":
                            Console.WriteLine("\nEnter the new day rate\n");
                            bool dayRateIsNotNumerical = !decimal.TryParse(Console.ReadLine(), out decimal updatedDayRate);
                            
                            if (dayRateIsNotNumerical)
                            {
                                Console.WriteLine(nonNumericalError);
                                return;
                            }

                            tempEmployee!.DayRate = updatedDayRate;
                            _tempEmployeeRepo.Update(tempEmployee);
                            break;
                        case "3":
                            Console.WriteLine("\nEnter the new number of weeks worked\n");
                            var updatedWeeksWorked = Convert.ToInt32(Console.ReadLine());
                            tempEmployee!.WeeksWorked = updatedWeeksWorked;
                            _tempEmployeeRepo.Update(tempEmployee);
                            break;
                    }
                    
                    Console.WriteLine(updated);
                    return;
                }
                else if (tempConfirmation == "")
                {
                    Console.WriteLine(nullInputError);
                    return;
                }
            }
        }

        static void GetAllInfo()
        {
            Console.Clear();
            Console.WriteLine("-=-=-=-=- Permanent Employees -=-=-=-=-\n" + string.Concat(_permEmployeeRepo.GetAll()));
            Console.WriteLine("-=-=-=-=- Temporary Employees -=-=-=-=-\n" + string.Concat(_tempEmployeeRepo.GetAll()));
        }

        static void GetEmployee()
        {
            Console.Clear();
            Console.WriteLine("\nWoukd you like to view [1] a permanent employee or [2] a temporary employee?\n");
            var option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine(enterID + "view\n");
                bool idIsInvalid = !int.TryParse(Console.ReadLine(), out int validID);
                var permanentEmployee = _permEmployeeRepo.GetEmployee(validID);
                
                if (idIsInvalid)
                {
                    Console.WriteLine(nonNumericalError);
                    return;
                }

                Console.WriteLine(permanentEmployee + $"\nWould you like to view {permanentEmployee!.Name}'s total annual salary [1] or hourly rate [2]?\n");
                PermanentPayCalculator permEmployeePayCalculator = new();

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
                        Console.WriteLine("\nInvalid input!\n");
                        break;
                }
            }
            else if (option == "2")
            {
                Console.WriteLine(enterID + "view\n");
                bool idIsInvalid = !int.TryParse(Console.ReadLine(), out int validID);
                var temporaryEmployee = _tempEmployeeRepo.GetEmployee(validID);

                if (idIsInvalid)
                {
                    Console.WriteLine(nonNumericalError);
                    return;
                }

                Console.WriteLine(temporaryEmployee + $"\nWould you like to view {temporaryEmployee!.Name}'s total annual salary [1] or hourly rate [2]?\n");
                TemporaryPayCalculator tempEmployeePayCalculator = new();

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