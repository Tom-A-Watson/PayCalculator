namespace PayCalculator
{
    public class MockEmployeeRepo
    {
        public static List<Employee> _permEmps = new List<Employee>()
        {
            new PermEmployeeCalculations()
            {
                Name = "Joe Bloggs",
                Id = 001,
                ContractType = "Permanent",
                Salary = 25000,
                Bonus = 2500,
                HoursWorked = 35
            },

            new PermEmployeeCalculations()
            {
                Name = "John Smith",
                Id = 002,
                ContractType = "Permanent",
                Salary = 35000,
                Bonus = 1000,
                HoursWorked = 35
            }
        };
    }
}
