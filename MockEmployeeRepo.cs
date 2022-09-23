namespace PayCalculator
{
    public class MockEmployeeRepo
    {
        public static List<PermEmployee> _permEmps = new List<PermEmployee>()
        {
            new PermEmployee(2500)
            {
                Name = "Joe Bloggs",
                Id = 001,
                Salary = 25000
            },

            new PermEmployee(1000)
            {
                Name = "John Smith",
                Id = 002,
                Salary = 35000
            }
        };
    }
}
