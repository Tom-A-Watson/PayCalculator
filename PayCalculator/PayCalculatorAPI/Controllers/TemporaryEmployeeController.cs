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
        private readonly Models.ErrorMessages _errorMessages;

        public TemporaryEmployeeController(IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo, ITemporaryPayCalculator tempPayCalculator, Models.ErrorMessages errorMessages)
        {
            _tempEmployeeRepo = tempEmployeeRepo;
            _tempPayCalculator = tempPayCalculator;
            _errorMessages = errorMessages;
        }

        [HttpGet]
        public async Task<ActionResult<List<TemporaryEmployee>>> Get()
        {
            var employeeList = _tempEmployeeRepo.GetAll();
            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            }

            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemporaryEmployee>> GetEmployee(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            
            if (employee == null)
            {
                return NotFound(_errorMessages.idNotFound);
            }

            employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult<TemporaryEmployee>> Create(TemporaryEmployee employee)
        {
            return Created("Temporary Employee Repository", _tempEmployeeRepo.Create(employee));
        }

        [HttpPatch]
        public async Task<ActionResult<TemporaryEmployee>> Update(TemporaryEmployee employee)
        {
            return (employee == null) ? NotFound(_errorMessages.employeeNotFound) : Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PermanentEmployee>> Delete(int id)
        {
            bool? deletedEmployee = _tempEmployeeRepo.Delete(id);
            return (deletedEmployee == null) ? NotFound(_errorMessages.idNotFound) : Accepted(deletedEmployee);
        }
    }
}