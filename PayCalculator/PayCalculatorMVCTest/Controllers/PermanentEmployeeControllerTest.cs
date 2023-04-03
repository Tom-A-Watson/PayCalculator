using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using PayCalculatorMVC.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace PayCalculatorMVCTest.Controllers
{
    [TestFixture]
    public class PermanentEmployeeControllerTest
    {
#nullable disable
        private List<PermanentEmployee> _employees;
        private CreateOrUpdatePermanentEmployee _createOrUpdateEmployeeModel;
        private PermanentEmployeeController controller;

        private Mock<ILogger<PermanentEmployeeController>> _mockLogger;
        private Mock<IPermanentEmployeeMapper> _mockMapper;
        private Mock<IEmployeeRepository<PermanentEmployee>> _mockRepository;
        private Mock<PermanentPayCalculator> _mockCalculator;

        private const string EmployeeName = "Bhavana";
        private const decimal EmployeeSalary = 20000;
        private const decimal EmployeeBonus = 5000;
        private const int EmployeeHoursWorked = 100;
#nullable enable
        [SetUp]
        public void Setup()
        {
            _employees = new List<PermanentEmployee>()
            {
                new PermanentEmployee()
                {
                    Id = 1,
                    Name = EmployeeName,
                    Salary = EmployeeSalary,
                    Bonus = EmployeeBonus,
                    HoursWorked = EmployeeHoursWorked
                },

                new PermanentEmployee()
                {
                    Id= 2,
                    Name = "zachary",
                    Salary = 20000,
                    Bonus = 200,
                    HoursWorked = 1820
                }
            };

            _createOrUpdateEmployeeModel = new()
            {
                Name = EmployeeName,
                Salary = EmployeeSalary,
                Bonus = EmployeeBonus,
                HoursWorked = EmployeeHoursWorked
            };

            _mockLogger = new();
            _mockRepository = new();
            _mockCalculator = new();
            _mockMapper = new();
            controller = new PermanentEmployeeController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockCalculator.Object);
        }

        [Test]
        public void Test_Index_ReturnsAllEmployees()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_employees);

            // Act
            var result = controller.Index();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType, Is.EqualTo(typeof(ViewResult)));
            });
        }

        [Test]
        public void Test_Create_ReturnsNewEmployee()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.Create(_employees[0])).Returns(_employees[0]);

            // Act
            var result = controller.Create(_createOrUpdateEmployeeModel) as RedirectToActionResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType, Is.EqualTo(typeof(RedirectToActionResult)));
                Assert.That(result.ActionName, Is.EqualTo("Index"));
            });
        }
    }
}
