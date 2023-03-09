using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Services;
using PayCalculatorLibrary.Constants;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using log4net;

namespace PayCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermanentEmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<PermanentEmployee> _permEmployeeRepo;
        private readonly IPermanentPayCalculator _permPayCalculator;
        private readonly IPermanentEmployeeMapper _mapper;
        private readonly ILog _log4net;

        public PermanentEmployeeController(IEmployeeRepository<PermanentEmployee> permEmployeeRepo, IPermanentPayCalculator permPayCalculator, IPermanentEmployeeMapper mapper)
        {
            _permEmployeeRepo = permEmployeeRepo;
            _permPayCalculator = permPayCalculator;
            _mapper = mapper;
            _log4net = LogManager.GetLogger(typeof(PermanentEmployeeController));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employeeList = _permEmployeeRepo.GetAll();

            if (employeeList.Count() == 0)
            {
                _log4net.Info("Returned an empty list of permanent employees");
                return NotFound(ErrorMessages.EmptyEmployeeList);
            }

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            }

            _log4net.Info($"Returned all permanent employees, {employeeList.Count()} were found");
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _permEmployeeRepo.GetEmployee(id);

            if (employee == null)
            {
                _log4net.Error($"Permanent employee with ID {id} does not exist!");
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            _log4net.Info($"Permanent employee with ID {id} read sucessfully");
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Create(CreateOrUpdatePermanentEmployee createModel)
        {
            var employee = _mapper.Map(createModel);
            var newEmployee = _permEmployeeRepo.Create(employee);
            newEmployee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary, employee.Bonus);
            _log4net.Info($"Created a new permanent employee with an ID of {newEmployee.Id}");
            return Created($"/permanentemployee/{newEmployee.Id}", newEmployee);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] CreateOrUpdatePermanentEmployee updateModel)
        {
            if (_permEmployeeRepo.GetEmployee(id) == null)
            {
                _log4net.Error($"Permanent employee with ID {id} does not exist!");
                return NotFound(ErrorMessages.IdNotFound);
            }

            var employee = _mapper.Map(updateModel);
            employee.Id = id;
            _permEmployeeRepo.Update(employee);
            _log4net.Info($"Permanent employee with ID {id} successfully updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _permEmployeeRepo.Delete(id);

            if (delete == false)
            {
                _log4net.Error($"Permanent employee with ID {id} does not exist!");
                return NotFound(ErrorMessages.IdNotFound);
            }

            _log4net.Info($"Permanent employee with ID {id} successfully deleted");
            return Ok(delete);
        }
    }
}