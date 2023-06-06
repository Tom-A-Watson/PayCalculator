using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using PayCalculatorMVC.Models;

namespace PayCalculatorMVC.Controllers
{
    public class TemporaryEmployeeController : Controller
    {
        private readonly ILogger<TemporaryEmployeeController> _logger;
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;
        private readonly ITemporaryEmployeeMapper _mapper;
        private readonly ITemporaryPayCalculator _payCalculator;
        private readonly ITimeCalculator _timeCalculator;

        public TemporaryEmployeeController(ILogger<TemporaryEmployeeController> logger, IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo,
            ITemporaryEmployeeMapper mapper, ITemporaryPayCalculator calculator, ITimeCalculator timeCalculator)
        {
            _logger = logger;
            _tempEmployeeRepo = tempEmployeeRepo;
            _mapper = mapper;
            _payCalculator = calculator;
            _timeCalculator = timeCalculator;
        }

        public IActionResult Index(TempEmployeeAlertsViewModel viewModel)
        {
            var employeeList = _tempEmployeeRepo.GetAll();
            viewModel.Employees = employeeList;

            if (!viewModel.Alerts.HasValue)
            {
                viewModel.Alerts = Enums.Alerts.None;
            }

            foreach (var employee in employeeList)
            {
                employee.HoursWorked = _timeCalculator.HoursWorked(employee.StartDate, DateTime.Now);
                employee.WeeksWorked = _timeCalculator.WeeksWorked(employee.StartDate, DateTime.Now);
                employee.TotalAnnualPay = Math.Round(_payCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked), 2);
                employee.HourlyRate = Math.Round(_payCalculator.HourlyRate(employee.DayRate), 2);
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrUpdateTemporaryEmployee createModel)
        {
            var mappedEmployee = _mapper.Map(createModel);
            var viewModel = new TempEmployeeAlertsViewModel();

            if (ModelState.IsValid)
            {
                _tempEmployeeRepo.Create(mappedEmployee);
                viewModel.Alerts = Enums.Alerts.CreateSuccess;
                return RedirectToAction("Index", viewModel);
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
            var viewModel = new TempEmployeeAlertsViewModel();

            if (ModelState.IsValid)
            {
                _tempEmployeeRepo.Update(existingEmployee);
                viewModel.Alerts = Enums.Alerts.UpdateSuccess;
                return RedirectToAction("Index", viewModel);
            }

            return RedirectToAction("Update");
        }

        public IActionResult Delete(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Deletion(int id)
        {
            var delete = _tempEmployeeRepo.Delete(id);
            var viewModel = new TempEmployeeAlertsViewModel();

            if (!delete)
            {
                viewModel.Alerts = Enums.Alerts.DeleteFailure;
                return RedirectToAction("Index", viewModel);
            }

            _tempEmployeeRepo.Delete(id);
            viewModel.Alerts = Enums.Alerts.DeleteSuccess;
            return RedirectToAction("Index", viewModel);
        }
    }
}