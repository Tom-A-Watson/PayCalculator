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
        private readonly Models.ErrorMessages _errorMessages;

        public PermanentEmployeeController(IEmployeeRepository<PermanentEmployee> permEmployeeRepo, IPermanentPayCalculator permPayCalculator, Models.ErrorMessages errorMessages)
        {
            _permEmployeeRepo = permEmployeeRepo;
            _permPayCalculator = permPayCalculator;
            _errorMessages = errorMessages;
        }

        [HttpGet]
        public async Task<ActionResult<List<PermanentEmployee>>> Get()
        {
            var employeeList = _permEmployeeRepo.GetAll();
            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            }

            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermanentEmployee>> GetEmployee(int id)
        {
            var employee = _permEmployeeRepo.GetEmployee(id);

            if (employee == null)
            {
                return NotFound(_errorMessages.idNotFound);
            }

            employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult<PermanentEmployee>> Create(PermanentEmployee employee)
        {
            return Created("Permanent Employee Repository", _permEmployeeRepo.Create(employee));
        }

        [HttpPatch]
        public async Task<ActionResult<PermanentEmployee>> Update(PermanentEmployee employee)
        {
            return (employee == null) ? NotFound(_errorMessages.employeeNotFound) : Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PermanentEmployee>> Delete(int id)
        {
            bool? deletedEmployee = _permEmployeeRepo.Delete(id);
            return (deletedEmployee == null) ? NotFound(_errorMessages.idNotFound) : Accepted(deletedEmployee);
        }
    }
}