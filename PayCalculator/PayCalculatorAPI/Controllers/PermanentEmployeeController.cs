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
            var employeeCount = employeeList.Count();

            if (employeeCount == 0)
            {
                _log4net.Info(LogMessages.EmptyEmployeeList.Insert(26, "permanent "));
                return NotFound(ErrorMessages.EmptyEmployeeList);
            }

            foreach (var employee in employeeList)
            {
                employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary.Value, employee.Bonus.Value);
            }

            _log4net.Info("Permanent " + LogMessages.ReturnedAllEmployees.Insert(24, Convert.ToString(employeeCount)));
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _permEmployeeRepo.GetEmployee(id);
            var stringID = Convert.ToString(id);

            if (employee == null)
            {
                _log4net.Error("Permanent " + LogMessages.NullEmployee.Insert(17, stringID) + " Unable to read");
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(employee.Salary.Value, employee.Bonus.Value);
            _log4net.Info("Permanent " + LogMessages.EmployeeRead.Insert(17, stringID));
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult Create(CreateOrUpdatePermanentEmployee createModel)
        {
            var mappedEmployee = _mapper.Map(createModel);
            var employee = _permEmployeeRepo.Create(mappedEmployee);
            employee.TotalAnnualPay = _permPayCalculator.TotalAnnualPay(mappedEmployee.Salary.Value, mappedEmployee.Bonus.Value);
            _log4net.Info("Permanent " + LogMessages.EmployeeCreated + $"{employee.Id}");
            return Created($"/permanentemployee/{employee.Id}", employee);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] CreateOrUpdatePermanentEmployee updateModel)
        {
            var employee = _mapper.Map(updateModel);
            var stringID = Convert.ToString(id);

            if (_permEmployeeRepo.GetEmployee(id) == null)
            {
                _log4net.Error("Permanent " + LogMessages.NullEmployee.Insert(17, stringID) + " Unable to update");
                return NotFound(ErrorMessages.IdNotFound);
            }

            employee.Id = id;
            _permEmployeeRepo.Update(employee);
            _log4net.Info("Permanent " + LogMessages.EmployeeUpdated.Insert(17, stringID));
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employeeName = _permEmployeeRepo.GetEmployee(id).Name;
            var delete = _permEmployeeRepo.Delete(id);
            var stringID = Convert.ToString(id);
            var deleteMessage = "Permanent " + LogMessages.EmployeeDeleted.Insert(17, stringID);
            
            if (delete == false)
            {
                _log4net.Error("Permanent " + LogMessages.NullEmployee.Insert(17, stringID) + " Unable to delete");
                return NotFound(ErrorMessages.IdNotFound);
            }

            _log4net.Info(deleteMessage.Insert(29, $"({employeeName})"));
            return Ok(delete);
        }
    }
}