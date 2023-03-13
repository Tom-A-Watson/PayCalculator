using Microsoft.AspNetCore.Mvc;

namespace PayCalculatorMVC.Controllers
{
    public class PermanentEmployeeController : Controller
    {
        private readonly ILogger<PermanentEmployeeController> _logger;

        public PermanentEmployeeController(ILogger<PermanentEmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetAll()
        //{

        //}

        //public IActionResult Create()
        //{

        //}

        //public IActionResult GetEmployee()
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
