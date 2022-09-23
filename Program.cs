namespace PayCalculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter 1 to get all employee information.\nEnter 2 to get a single employee's information");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    GetAllInfo(); break;
                case "2":
                    GetEmployee(); break;
            }
        }

        static void GetAllInfo()
        {
            for (int i = 0; i < MockEmployeeRepo._permEmps.Count; i++)
            {
                Employee permEmp = MockEmployeeRepo._permEmps[i];
                {
                    string info = $"\nStaff ID: {permEmp.Id} \nStaff Name: {permEmp.Name} \nStaff Salary: {permEmp.Salary}";
                    Console.WriteLine(info);
                }
            }
        }

        static void GetEmployee()
        {
            Console.WriteLine("Enter the ID of the employee you would like to view?");
            string? idInput = Console.ReadLine();
            Employee emp = MockEmployeeRepo._permEmps[Int32.Parse(idInput) - 1];
            Console.WriteLine($"\nStaff ID: {emp.Id} \nStaff Name: {emp.Name} \nStaff Salary: {emp.Salary}");
        }
    }
}