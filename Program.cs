namespace PayCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter 1 to get all employee information \nEnter 2 to get a single employee's information \nEnter 3 to exit");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    GetAllInfo(); 
                    break;
                case "2":
                    GetEmployee(); 
                    break;
                case "3": return;
                default:
                    if (option != "1" || option != "2" || option != "3") { Console.WriteLine("\nInvalid input!"); } 
                    break;
            }
        }

        static void GetAllInfo()
        {
            for (int i = 0; i < MockEmployeeRepo._permEmps.Count; i++)
            {
                PermEmployeeCalculations permEmp = (PermEmployeeCalculations) MockEmployeeRepo._permEmps[i];
                Console.WriteLine($"\nStaff ID: {permEmp.Id} \nStaff Name: {permEmp.Name} \nStaff Contract Type: {permEmp.ContractType}" +
                    $" \nStaff Salary: {permEmp.Salary} \nStaff Bonus: {permEmp.Bonus} \nHours Worked: {permEmp.HoursWorked}");
            }
        }

        static void GetEmployee()
        {
            Console.WriteLine("\nEnter the ID of the employee you would like to view");
            
            string? idInput = Console.ReadLine();
            PermEmployeeCalculations permEmp = (PermEmployeeCalculations) MockEmployeeRepo._permEmps[Int32.Parse(idInput) - 1];
            
            Console.WriteLine($"\nStaff ID: {permEmp.Id} \nStaff Name: {permEmp.Name} \nStaff Contract Type: {permEmp.ContractType}" +
                $" \nStaff Salary: {permEmp.Salary} \nStaff Bonus: {permEmp.Bonus} \nHours Worked: {permEmp.HoursWorked}");
        }
    }
}