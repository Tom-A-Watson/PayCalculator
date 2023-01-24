using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
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
            var result = _permEmployeeRepo.GetEmployee(id);

            if (result == null)
            {
                return NotFound(idNotFoundMessage);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<PermanentEmployee>> Create(PermanentEmployee employee)
        {
            return Created("Permanent Employee Repository", _permEmployeeRepo.Create(employee));
        }

        [HttpPatch]
        public async Task<ActionResult<PermanentEmployee>> Update(PermanentEmployee employee)
        {
            if (employee == null)
            {
                return NotFound("This employee does not exist!");
            }

            return Accepted(_permEmployeeRepo.Update(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PermanentEmployee>> Delete(int id)
        {
            var result = _permEmployeeRepo.Delete(id);

            if (result == false)
            {
                return NotFound(idNotFoundMessage);
            }

            return Accepted(result);
        }
    }
}