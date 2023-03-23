using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorMVC.Models;
using System.Diagnostics;

namespace PayCalculatorMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository<PermanentEmployee> _permEmployeeRepo;
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;

        public HomeController(ILogger<HomeController> logger,
            IEmployeeRepository<PermanentEmployee> permEmployeeRepo, IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo)
        {
            _logger = logger;
            _permEmployeeRepo = permEmployeeRepo;
            _tempEmployeeRepo = tempEmployeeRepo;
        }

        public IActionResult Index()
        {
            return View(new HomePageViewModel {
                PermEmployeeList = _permEmployeeRepo.GetAll(), 
                TempEmployeeList = _tempEmployeeRepo.GetAll()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}