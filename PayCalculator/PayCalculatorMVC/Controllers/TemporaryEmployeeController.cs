using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;

namespace PayCalculatorMVC.Controllers
{
    public class TemporaryEmployeeController : Controller
    {
        private readonly ILogger<TemporaryEmployeeController> _logger;
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;

        public TemporaryEmployeeController(ILogger<TemporaryEmployeeController> logger, IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo)
        {
            _logger = logger;
            _tempEmployeeRepo = tempEmployeeRepo;
        }

        public IActionResult Index()
        {
            var employeeList = _tempEmployeeRepo.GetAll();
            return View(employeeList);
        }


        //public IActionResult Create()
        //{

        //}

        //public IActionResult Update()
        //{

        //}

        //public IActionResult Delete()
        //{

        //}
    }
}