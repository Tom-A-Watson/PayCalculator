using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using PayCalculatorMVC.Models;

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

        public IActionResult Index(PermEmployeeAlertsViewModel viewModel)
        {
            var employeeList = _permEmployeeRepo.GetAll();
            viewModel.Employees = employeeList;

            if (!viewModel.Alerts.HasValue)
            {
                viewModel.Alerts = Enums.Alerts.None;
            }

            foreach (var employee in employeeList) 
            {
                employee.HoursWorked = _timeCalculator.HoursWorked(employee.StartDate, DateTime.Now);
                employee.TotalAnnualPay = Math.Round(_payCalculator.TotalAnnualPay(employee.Salary.Value, employee.Bonus.Value), 2);
                employee.HourlyRate = Math.Round(_payCalculator.HourlyRate(employee.Salary.Value, employee.HoursWorked), 2);
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrUpdatePermanentEmployee createModel)
        {
            var mappedEmployee = _mapper.Map(createModel);
            var viewModel = new PermEmployeeAlertsViewModel();
            viewModel.Name = createModel.Name;

            if (ModelState.IsValid)
            {
                _permEmployeeRepo.Create(mappedEmployee);
                viewModel.Alerts = Enums.Alerts.CreateSuccess;
                return RedirectToAction("Index", viewModel);
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
            var viewModel = new PermEmployeeAlertsViewModel();
            viewModel.Name = _permEmployeeRepo.Update(existingEmployee).Name;

            if (ModelState.IsValid)
            {
                _permEmployeeRepo.Update(existingEmployee);
                viewModel.Alerts = Enums.Alerts.UpdateSuccess;
                return RedirectToAction("Index", viewModel);
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
            var viewModel = new PermEmployeeAlertsViewModel();
            viewModel.Name = _permEmployeeRepo.GetEmployee(id).Name;
            var delete = _permEmployeeRepo.Delete(id);

            if (!delete)
            {
                viewModel.Alerts = Enums.Alerts.DeleteFailure;
                return RedirectToAction("Index", viewModel);
            }

            _permEmployeeRepo.Delete(id);
            viewModel.Alerts = Enums.Alerts.DeleteSuccess;
            return RedirectToAction("Index", viewModel);
        }
    }
}