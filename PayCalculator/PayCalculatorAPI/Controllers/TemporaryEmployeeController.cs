using Microsoft.AspNetCore.Mvc;
using PayCalculator.Models;
using PayCalculator.Repositories;
using PayCalculator.Services;

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
            if (_tempEmployeeRepo.GetEmployee(id) == null)
            {
                return NotFound(idNotFoundMessage);
            }

            return Ok(_tempEmployeeRepo.GetEmployee(id));
        }

        [HttpPut("{employee}")]
        public async Task<ActionResult<TemporaryEmployee>> Create(TemporaryEmployee employee)
        {
            return Ok(_tempEmployeeRepo.Create(employee));
        }

        [HttpPatch("{employee}")]
        public async Task<ActionResult<TemporaryEmployee>> Update(TemporaryEmployee employee)
        {
            if (employee == null)
            {
                return NotFound("This employee does not exist!");
            }

            return Ok(_tempEmployeeRepo.Update(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TemporaryEmployee>> Delete(int id)
        {
            if (_tempEmployeeRepo.GetEmployee(id) == null)
            {
                return NotFound(idNotFoundMessage);
            }

            return Ok(_tempEmployeeRepo.Delete(id));
        }
    }
}
