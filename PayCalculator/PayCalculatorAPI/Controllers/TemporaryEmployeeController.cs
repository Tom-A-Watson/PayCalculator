using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using PayCalculatorAPI.Resources;
using PayCalculatorAPI.Services;
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
        private FieldExtractor extractor;

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
        public IActionResult Create(CreateOrUpdateTemporaryEmployee createModel)
        {
            extractor = new();
            var employee = extractor.ExtractTempEmployeeDetails(createModel);
            return Created($"/temporaryemployee{employee.Id}", _tempEmployeeRepo.Create(employee));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(CreateOrUpdateTemporaryEmployee updateModel)
        {
            if (updateModel == null)
            {
                return NotFound(ErrorMessages.EmployeeNotFound);
            }

            extractor = new();
            var employee = extractor.ExtractTempEmployeeDetails(updateModel);
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