using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorMVC.Controllers
{
    public class PermanentEmployeeController : Controller
    {
        private readonly ILogger<PermanentEmployeeController> _logger;
        private readonly IEmployeeRepository<PermanentEmployee> _permEmployeeRepo;
        private readonly IPermanentEmployeeMapper _mapper;
        private readonly IPermanentPayCalculator _calculator;

        public PermanentEmployeeController(ILogger<PermanentEmployeeController> logger, IEmployeeRepository<PermanentEmployee> permEmployeeRepo, 
            IPermanentEmployeeMapper mapper, IPermanentPayCalculator calculator)
        {
            _logger = logger;
            _permEmployeeRepo = permEmployeeRepo;
            _mapper = mapper;
            _calculator = calculator;
        }

        public IActionResult Index()
        {
            var employeeList = _permEmployeeRepo.GetAll();

            foreach (var employee in employeeList) 
            {
                employee.TotalAnnualPay = Math.Round(_calculator.TotalAnnualPay(employee.Salary, employee.Bonus), 2);
                employee.HourlyRate = Math.Round(_calculator.HourlyRate(employee.Salary, employee.HoursWorked), 2);
            }

            return View(employeeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrUpdatePermanentEmployee createModel)
        {
            var mappedEmployee = _mapper.Map(createModel);

            if (ModelState.IsValid)
            {
                var employee = _permEmployeeRepo.Create(mappedEmployee);
                employee.TotalAnnualPay = Math.Round(_calculator.TotalAnnualPay(employee.Salary, employee.Bonus), 2);
                employee.HourlyRate = Math.Round(_calculator.HourlyRate(employee.Salary, employee.HoursWorked), 2);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        //public IActionResult Update()
        //{

        //}

        //public IActionResult Delete()
        //{

        //}
    }
}