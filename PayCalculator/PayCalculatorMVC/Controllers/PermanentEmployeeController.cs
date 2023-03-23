using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;

namespace PayCalculatorMVC.Controllers
{
    public class PermanentEmployeeController : Controller
    {
        private readonly ILogger<PermanentEmployeeController> _logger;
        private readonly IEmployeeRepository<PermanentEmployee> _permEmployeeRepo;

        public PermanentEmployeeController(ILogger<PermanentEmployeeController> logger, IEmployeeRepository<PermanentEmployee> permEmployeeRepo)
        {
            _logger = logger;
            _permEmployeeRepo = permEmployeeRepo;
        }

        public IActionResult Index()
        {
            var employeeList = _permEmployeeRepo.GetAll();
            return View(employeeList);
        }


        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult Update()
        //{

        //}

        //public IActionResult Delete()
        //{

        //}
    }
}