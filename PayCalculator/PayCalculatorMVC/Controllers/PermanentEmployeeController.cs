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
        private readonly IPermanentPayCalculator _payCalculator;
        private readonly ITimeCalculator _timeCalculator;

        public PermanentEmployeeController(ILogger<PermanentEmployeeController> logger, IEmployeeRepository<PermanentEmployee> permEmployeeRepo, 
            IPermanentEmployeeMapper mapper, IPermanentPayCalculator calculator, ITimeCalculator timeCalculator)
        {
            _logger = logger;
            _permEmployeeRepo = permEmployeeRepo;
            _mapper = mapper;
            _payCalculator = calculator;
            _timeCalculator = timeCalculator;
        }

        public IActionResult Index()
        {
            var employeeList = _permEmployeeRepo.GetAll();

            foreach (var employee in employeeList) 
            {
                employee.HoursWorked = _timeCalculator.HoursWorked(employee.StartDate, DateTime.Now);
                employee.TotalAnnualPay = Math.Round(_payCalculator.TotalAnnualPay(employee.Salary.Value, employee.Bonus.Value), 2);
                employee.HourlyRate = Math.Round(_payCalculator.HourlyRate(employee.Salary.Value, employee.HoursWorked.Value), 2);
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
                _permEmployeeRepo.Create(mappedEmployee);
                return RedirectToAction("Index");
            }
            
            return View(mappedEmployee);
        }

        public IActionResult Update(int id)
        {
            var employee = _permEmployeeRepo.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(PermanentEmployee existingEmployee)
        {
            if (ModelState.IsValid)
            {
                _permEmployeeRepo.Update(existingEmployee);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Update");
        }

        public IActionResult Delete(int id)
        {
            var employee = _permEmployeeRepo.GetEmployee(id);
            return View(employee);
        }


        [HttpPost]
        public IActionResult Deletion(int id)
        {
            if (!_permEmployeeRepo.Delete(id))
            {
                return RedirectToAction("Index");
            }

            _permEmployeeRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}