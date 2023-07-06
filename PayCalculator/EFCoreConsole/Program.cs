using PayCalculatorData;
using PayCalculatorLibrary.Models;

EmployeeContext _context = new();

GetPermanentEmployees();
//AddPermanentEmployee();
//GetPermanentEmployees();
//QueryFilters();
//SortEmployees();
//DeleteEmployee();
UpdateEmployee();
GetPermanentEmployees();

void UpdateEmployee()
{
    var employee = _context.PermanentEmployees.FirstOrDefault(x => x.Id == 1);

    if (employee != null)
    {
        employee.Name = "William W";
        _context.SaveChanges();
    }
}

void DeleteEmployee()
{
    var employee = _context.PermanentEmployees.Find(3);

    if (employee != null)
    {
        _context.PermanentEmployees.Remove(employee);
        _context.SaveChanges();
    }
}

void SortEmployees()
{
    var employeesByName = _context.PermanentEmployees.OrderBy(x => x.Name).ToList();
    employeesByName.ForEach(x => Console.WriteLine(x.Name));
}

void QueryFilters()
{
    var name = "Tom W";
    var employees = _context.PermanentEmployees.Where(x => x.Name == name).ToList();
    Console.WriteLine(employees);
}

void AddPermanentEmployee()
{
    var employee = new PermanentEmployee()
    {
        Name = "Harry A",
        Salary = 25000,
        Bonus = 2000,
        StartDate = new DateTime(2022, 8, 15)
    };

    _context.PermanentEmployees.Add(employee);
    _context.SaveChanges();
}

void GetPermanentEmployees()
{
    var employees = _context.PermanentEmployees.ToList();

    foreach (var employee in employees)
    {
        Console.WriteLine(employee.Name);
    }
}