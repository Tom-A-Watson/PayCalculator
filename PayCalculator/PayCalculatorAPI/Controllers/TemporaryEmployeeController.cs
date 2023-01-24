using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryEmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;
        private readonly ITemporaryPayCalculator _tempPayCalculator;
        private readonly string idNotFoundMessage = "Sorry, this ID could not be found";

        public TemporaryEmployeeController(IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo, ITemporaryPayCalculator tempPayCalculator)
        {
            _tempEmployeeRepo = tempEmployeeRepo;
            _tempPayCalculator = tempPayCalculator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TemporaryEmployee>>> Get()
        {
            return Ok(_tempEmployeeRepo.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemporaryEmployee>> GetEmployee(int id)
        {
            var result = _tempEmployeeRepo.GetEmployee(id);

            if (result == null)
            {
                return NotFound(idNotFoundMessage);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<TemporaryEmployee>> Create(TemporaryEmployee employee)
        {
            return Created("Temporary Employee Repository", _tempEmployeeRepo.Create(employee));
        }

        [HttpPatch]
        public async Task<ActionResult<TemporaryEmployee>> Update(TemporaryEmployee employee)
        {
            if (employee == null)
            {
                return NotFound("This employee does not exist!");
            }

            return Accepted(_tempEmployeeRepo.Update(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PermanentEmployee>> Delete(int id)
        {
            var result = _tempEmployeeRepo.Delete(id);

            if (result == false)
            {
                return NotFound(idNotFoundMessage);
            }

            return Accepted(result);
        }
    }
}