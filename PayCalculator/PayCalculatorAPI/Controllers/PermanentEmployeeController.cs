using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Resources;
using PayCalculatorAPI.Services;
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
        private FieldExtractor extractor;

        public PermanentEmployeeController(IEmployeeRepository<PermanentEmployee> permEmployeeRepo, IPermanentPayCalculator permPayCalculator)
        {
            _permEmployeeRepo = permEmployeeRepo;
            _permPayCalculator = permPayCalculator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employeeList = _permEmployeeRepo.GetAll();

            if (employeeList.Count() == 0)
            {
                return NotFound(ErrorMessages.EmptyEmployeeList);
            }

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            }

            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _permEmployeeRepo.GetEmployee(id);

            if (employee == null)
            {
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Create(CreateOrUpdatePermanentEmployee createModel)
        {
            extractor = new();
            var employee = extractor.ExtractPermEmployeeDetails(createModel);
            return Created($"/permanentemployee/{employee.Id}", _permEmployeeRepo.Create(employee));
        }

        [HttpPatch]
        public IActionResult Update(PermanentEmployee employee)
        {
            if (employee == null)
            {
                return NotFound(ErrorMessages.EmployeeNotFound);
            }

            _permEmployeeRepo.Update(employee);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _permEmployeeRepo.Delete(id);
            return (delete == false) ? NotFound(ErrorMessages.IdNotFound) : Accepted(delete);
        }
    }
}