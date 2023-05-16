using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using PayCalculatorMVC.Models;
using System.Diagnostics;

namespace PayCalculatorMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository<PermanentEmployee> _permEmployeeRepo;
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;
        private readonly IPermanentPayCalculator _permPayCalculator;
        private readonly ITemporaryPayCalculator _tempPayCalculator;
        private readonly ITimeCalculator _timeCalculator;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository<PermanentEmployee> permEmployeeRepo, 
            IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo, IPermanentPayCalculator permPayCalculator, ITemporaryPayCalculator tempPayCalculator, ITimeCalculator timeCalculator)
        {
            _logger = logger;
            _permEmployeeRepo = permEmployeeRepo;
            _permPayCalculator = permPayCalculator;
            _tempEmployeeRepo = tempEmployeeRepo;
            _tempPayCalculator = tempPayCalculator;
            _timeCalculator = timeCalculator;
        }

        public IActionResult Index()
        {
            var permEmployeeList = _permEmployeeRepo.GetAll();
            var tempEmployeeList = _tempEmployeeRepo.GetAll();

            foreach (var permEmployee in permEmployeeList) 
            {
                permEmployee.HoursWorked = _timeCalculator.HoursWorked(permEmployee.StartDate, DateTime.Now);
                permEmployee.TotalAnnualPay = Math.Round(_permPayCalculator.TotalAnnualPay(permEmployee.Salary.Value, permEmployee.Bonus.Value), 2);
                permEmployee.HourlyRate = Math.Round(_permPayCalculator.HourlyRate(permEmployee.Salary.Value, permEmployee.HoursWorked.Value), 2);
            }

            foreach (var tempEmployee in tempEmployeeList)
            {
                tempEmployee.TotalAnnualPay = Math.Round(_tempPayCalculator.TotalAnnualPay(tempEmployee.DayRate, tempEmployee.WeeksWorked), 2);
                tempEmployee.HourlyRate = Math.Round(_tempPayCalculator.HourlyRate(tempEmployee.DayRate), 2);
            }

            return View(new HomePageViewModel {
                PermEmployeeList = permEmployeeList,
                TempEmployeeList = tempEmployeeList
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}