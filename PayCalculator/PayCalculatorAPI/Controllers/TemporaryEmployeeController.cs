using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Resources;
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

        public TemporaryEmployeeController(IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo, ITemporaryPayCalculator tempPayCalculator)
        {
            _tempEmployeeRepo = tempEmployeeRepo;
            _tempPayCalculator = tempPayCalculator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employeeList = _tempEmployeeRepo.GetAll();

            if (employeeList.Count() == 0)
            {
                return NotFound();
            }

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            }

            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            
            if (employee == null)
            {
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Create(CreateTemporaryEmployee createTemporaryEmployee)
        {
            TemporaryEmployee employee = new();
            employee.Name = createTemporaryEmployee.Name;
            employee.DayRate = createTemporaryEmployee.DayRate;
            employee.WeeksWorked = createTemporaryEmployee.WeeksWorked;
            return Created($"/temporaryemployee{employee.Id}", _tempEmployeeRepo.Create(employee));
        }

        [HttpPatch]
        public IActionResult Update(TemporaryEmployee employee)
        {
            if (employee == null)
            {
                return NotFound(ErrorMessages.EmployeeNotFound);
            }

            _tempEmployeeRepo.Update(employee);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _tempEmployeeRepo.Delete(id);
            return (delete == false) ? NotFound(ErrorMessages.IdNotFound) : Accepted(delete);
        }
    }
}