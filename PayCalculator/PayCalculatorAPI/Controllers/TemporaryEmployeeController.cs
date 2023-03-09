using log4net;
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
    public class TemporaryEmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;
        private readonly ITemporaryPayCalculator _tempPayCalculator;
        private ITemporaryEmployeeMapper _mapper;
        private readonly ILog _log4net;

        public TemporaryEmployeeController(IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo, ITemporaryPayCalculator tempPayCalculator, ITemporaryEmployeeMapper mapper)
        {
            _tempEmployeeRepo = tempEmployeeRepo;
            _tempPayCalculator = tempPayCalculator;
            _mapper = mapper;
            _log4net = LogManager.GetLogger(typeof(PermanentEmployeeController));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employeeList = _tempEmployeeRepo.GetAll();

            if (employeeList.Count() == 0)
            {
                _log4net.Info("Returned an empty list of temporary employees");
                return NotFound(ErrorMessages.EmptyEmployeeList);
            }

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            }

            _log4net.Info($"Returned all temporary employees, {employeeList.Count()} were found");
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            
            if (employee == null)
            {
                _log4net.Error($"Temporary employee with ID {id} does not exist!");
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            _log4net.Info($"Temporary employee with ID {id} read successfully");
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Create(CreateOrUpdateTemporaryEmployee createModel)
        {
            var employee = _mapper.Map(createModel);
            var newEmployee = _tempEmployeeRepo.Create(employee);
            newEmployee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            _log4net.Info($"Created a new temporary employee with an ID of {newEmployee.Id}");
            return Created($"/temporaryemployee/{newEmployee.Id}", newEmployee);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] CreateOrUpdateTemporaryEmployee updateModel)
        {
            if (_tempEmployeeRepo.GetEmployee(id) == null)
            {
                _log4net.Error($"Temporary employee with ID {id} does not exist!");
                return NotFound(ErrorMessages.IdNotFound);
            }

            var employee = _mapper.Map(updateModel);
            employee.Id = id;
            _tempEmployeeRepo.Update(employee);
            _log4net.Error($"Temporary employee with ID {id} successfully updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _tempEmployeeRepo.Delete(id);

            if (delete == false)
            {
                _log4net.Error($"Temporary employee with ID {id} does not exist!");
                return NotFound(ErrorMessages.IdNotFound);
            }

            _log4net.Info($"Temporary employee with ID {id} successfully deleted");
            return Ok(delete);
        }
    }
}