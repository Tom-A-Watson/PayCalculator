using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Services;
using PayCalculatorLibrary.Constants;
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
        private readonly IPermanentEmployeeMapper _mapper;

        public PermanentEmployeeController(IEmployeeRepository<PermanentEmployee> permEmployeeRepo, IPermanentPayCalculator permPayCalculator, IPermanentEmployeeMapper mapper)
        {
            _permEmployeeRepo = permEmployeeRepo;
            _permPayCalculator = permPayCalculator;
            _mapper = mapper;
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
            var employee = _mapper.Map(createModel);
            employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            return Created($"/permanentemployee/{employee.Id}", _permEmployeeRepo.Create(employee));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] CreateOrUpdatePermanentEmployee updateModel)
        {
            if (updateModel == null)
            {
                return NotFound(ErrorMessages.EmployeeNotFound);
            }

            var employee = _mapper.Map(updateModel);
            employee.Id = id;
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