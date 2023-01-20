using Microsoft.AspNetCore.Mvc;
using PayCalculator.Models;
using PayCalculator.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermanentEmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<PermanentEmployee> _permEmployeeRepo;
        private readonly IPermanentPayCalculator _permPayCalculator;

        public PermanentEmployeeController(IEmployeeRepository<PermanentEmployee> permEmployeeRepo, IPermanentPayCalculator permPayCalculator)
        {
            _permEmployeeRepo = permEmployeeRepo;
            _permPayCalculator = permPayCalculator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PermanentEmployee>>> Get()
        {
            return Ok(_permEmployeeRepo.GetAll());
        }
    }
}
