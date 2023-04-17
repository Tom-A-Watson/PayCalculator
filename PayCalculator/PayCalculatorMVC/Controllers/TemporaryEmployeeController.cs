using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorMVC.Controllers
{
    public class TemporaryEmployeeController : Controller
    {
        private readonly ILogger<TemporaryEmployeeController> _logger;
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;
        private readonly ITemporaryEmployeeMapper _mapper;
        private readonly ITemporaryPayCalculator _calculator;

        public TemporaryEmployeeController(ILogger<TemporaryEmployeeController> logger, IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo,
            ITemporaryEmployeeMapper mapper, ITemporaryPayCalculator calculator)
        {
            _logger = logger;
            _tempEmployeeRepo = tempEmployeeRepo;
            _mapper = mapper;
            _calculator = calculator;
        }

        public IActionResult Index()
        {
            var employeeList = _tempEmployeeRepo.GetAll();

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = Math.Round(_calculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked), 2);
                employee.HourlyRate = Math.Round(_calculator.HourlyRate(employee.DayRate), 2);
            }

            return View(employeeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrUpdateTemporaryEmployee createModel)
        {
            var mappedEmployee = _mapper.Map(createModel);

            if (ModelState.IsValid)
            {
                _tempEmployeeRepo.Create(mappedEmployee);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        public IActionResult Update(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(TemporaryEmployee existingEmployee)
        {
            if (ModelState.IsValid)
            {
                _tempEmployeeRepo.Update(existingEmployee);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Update");
        }

        public IActionResult Delete(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Deletion(int id)
        {
            _tempEmployeeRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}