using Microsoft.AspNetCore.Mvc;
using PayCalculatorLibrary.Constants;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using log4net;

namespace PayCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryEmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<TemporaryEmployee> _tempEmployeeRepo;
        private readonly ITemporaryPayCalculator _tempPayCalculator;
        private readonly ITemporaryEmployeeMapper _mapper;
        private readonly ILog _log4net;

        public TemporaryEmployeeController(IEmployeeRepository<TemporaryEmployee> tempEmployeeRepo, ITemporaryPayCalculator tempPayCalculator, ITemporaryEmployeeMapper mapper)
        {
            _tempEmployeeRepo = tempEmployeeRepo;
            _tempPayCalculator = tempPayCalculator;
            _mapper = mapper;
            _log4net = LogManager.GetLogger(typeof(TemporaryEmployeeController));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employeeList = _tempEmployeeRepo.GetAll();
            var employeeCount = employeeList.Count();

            if (employeeCount == 0)
            {
                _log4net.Info(LogMessages.EmptyEmployeeList.Insert(26, "temporary "));
                return NotFound(ErrorMessages.EmptyEmployeeList);
            }

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            }

            _log4net.Info("Temporary " + LogMessages.ReturnedAllEmployees.Insert(24, Convert.ToString(employeeCount)));
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _tempEmployeeRepo.GetEmployee(id);
            var stringID = Convert.ToString(id);

            if (employee == null)
            {
                _log4net.Error("Temporary " + LogMessages.NullEmployee.Insert(17, stringID) + " Unable to read");
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(employee.DayRate, employee.WeeksWorked);
            _log4net.Info("Temporary " + LogMessages.EmployeeRead.Insert(17, stringID));
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Create(CreateOrUpdateTemporaryEmployee createModel)
        {
            var mappedEmployee = _mapper.Map(createModel);
            var employee = _tempEmployeeRepo.Create(mappedEmployee);
            employee.TotalAnnualPay = _tempPayCalculator.TotalAnnualPay(mappedEmployee.DayRate, mappedEmployee.WeeksWorked);
            _log4net.Info("Temporary " + LogMessages.EmployeeCreated + $"{employee.Id}");
            return Created($"/temporaryemployee/{employee.Id}", employee);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] CreateOrUpdateTemporaryEmployee updateModel)
        {
            var employee = _mapper.Map(updateModel);
            var stringID = Convert.ToString(id);

            if (_tempEmployeeRepo.GetEmployee(id) == null)
            {
                _log4net.Error("Temporary " + LogMessages.NullEmployee.Insert(17, stringID) + " Unable to update");
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.Id = id;
            _tempEmployeeRepo.Update(employee);
            _log4net.Info("Temporary " + LogMessages.EmployeeUpdated.Insert(17, stringID));
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employeeName = _tempEmployeeRepo.GetEmployee(id).Name;
            var delete = _tempEmployeeRepo.Delete(id);
            var stringID = Convert.ToString(id);
            var deleteMessage = "Temporary " + LogMessages.EmployeeDeleted.Insert(17, stringID);

            if (delete == false)
            {
                _log4net.Error("Temporary " + LogMessages.NullEmployee.Insert(17, stringID) + " Unable to delete");
                return NotFound(ErrorMessages.IdNotFound);
            }

            _log4net.Info(deleteMessage.Insert(29, $"({employeeName})"));
            return Ok(delete);
        }
    }
}