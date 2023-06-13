using PayCalculatorData;
using PayCalculatorLibrary.Models;

using (EmployeeContext context =  new EmployeeContext())
{
    context.Database.EnsureCreated();
}

GetPermanentEmployees();
AddPermanentEmployee();
GetPermanentEmployees();

void AddPermanentEmployee()
{
    var employee = new PermanentEmployee()
    {
        Name = "Harry A",
        Salary = 25000,
        Bonus = 2000,
        StartDate = new DateTime(2022, 8, 15)
    };

    using var context = new EmployeeContext();
    context.PermanentEmployees.Add(employee);
    context.SaveChanges();
}

void GetPermanentEmployees()
{
    using var context = new EmployeeContext();
    var employees = context.PermanentEmployees.ToList();

    foreach (var employee in employees)
    {
        Console.WriteLine(employee.Name);
    }
}