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
        private readonly string idNotFoundMessage = "Sorry, this ID could not be found";

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

        [HttpGet("{id}")]
        public async Task<ActionResult<PermanentEmployee>> GetEmployee(int id)
        {
            if (_permEmployeeRepo.GetEmployee(id) == null)
            {
                return NotFound(idNotFoundMessage);
            }

            return Ok(_permEmployeeRepo.GetEmployee(id));
        }

        [HttpPut("{employee}")]
        public async Task<ActionResult<PermanentEmployee>> Create(PermanentEmployee employee)
        {
            return Ok(_permEmployeeRepo.Create(employee));
        }

        [HttpPatch("{employee}")]
        public async Task<ActionResult<PermanentEmployee>> Update(PermanentEmployee employee)
        {
            if (employee == null)
            {
                return NotFound("This employee does not exist!");
            }

            return Ok(_permEmployeeRepo.Update(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PermanentEmployee>> Delete(int id)
        {
            if (_permEmployeeRepo.GetEmployee(id) == null)
            {
                return NotFound(idNotFoundMessage);
            }

            return Ok(_permEmployeeRepo.Delete(id));
        }
    }
}
