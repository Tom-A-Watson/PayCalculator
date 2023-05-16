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
        private int employeeCount;
        private CreateOrUpdatePermanentEmployee _createOrUpdateEmployeeModel;
        private PermanentEmployeeController controller;

        private Mock<ILogger<PermanentEmployeeController>> _mockLogger;
        private Mock<IPermanentEmployeeMapper> _mockMapper;
        private Mock<IEmployeeRepository<PermanentEmployee>> _mockRepository;
        private Mock<IPermanentPayCalculator> _mockPayCalculator;
        private Mock<ITimeCalculator> _mockTimeCalculator;

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
            _mockPayCalculator = new();
            _mockTimeCalculator = new();
            _mockMapper = new();
            controller = new PermanentEmployeeController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object, _mockPayCalculator.Object, _mockTimeCalculator.Object);
        }

        [Test]
        public void Index_ReturnsAllEmployees()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_employees);

            // Act
            var result = (ViewResult) controller.Index();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType, Is.EqualTo(typeof(ViewResult)));
                Assert.That(((List<PermanentEmployee>) result.Model).Count, Is.EqualTo(_employees.Count));
                Assert.That(((List<PermanentEmployee>) result.Model), Is.EquivalentTo(_employees));
                _mockRepository.Verify(x => x.GetAll(), Times.Once());
                _mockPayCalculator.Verify(x => x.TotalAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Exactly(_employees.Count));
                _mockPayCalculator.Verify(x => x.HourlyRate(It.IsAny<decimal>(), It.IsAny<int>()), Times.Exactly(_employees.Count));
            });
        }

        [Test]
        public void Create_ReturnsNewEmployee()
        {
            // Arrange
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.Create(_employees[0])).Returns(_employees[0]);

            // Act
            var result = (RedirectToActionResult) controller.Create(_createOrUpdateEmployeeModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToActionResult)));
                Assert.That(result.ActionName, Is.EqualTo("Index"));
                _mockMapper.Verify(x => x.Map(_createOrUpdateEmployeeModel), Times.Once());
                _mockRepository.Verify(x => x.Create(It.IsAny<PermanentEmployee>()), Times.Once());
            });
        }

        [TestCase(2)]
        public void Update_Sends_Data_Correctly(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[1]);

            // Act
            var result = (ViewResult) controller.Update(id);

            // Assert
            Assert.Multiple(() => 
            { 
                Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
                _mockRepository.Verify(x => x.GetEmployee(id), Times.Once());
            });
        }

        [Test]
        public void ValidModel_Updates_Employee_And_RedirectsToIndex()
        {
            // Arrange
            PermanentEmployee employee = new();

            // Act
            var result = (RedirectToActionResult) controller.Update(employee);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToActionResult)));
                Assert.That(result.ActionName, Is.EqualTo("Index"));
                _mockRepository.Verify(x => x.Update(employee), Times.Once());
            });
        }

        [Test]
        public void InvalidModel_RedirectsToUpdate()
        {
            // Arrange
            PermanentEmployee employee = new();
            controller.ModelState.AddModelError("test key", "test error message");

            // Act
            var result = (RedirectToActionResult) controller.Update(employee);

            // Assert
            Assert.Multiple(() => 
            {
                Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToActionResult)));
                Assert.That(result.ActionName, Is.EqualTo("Update"));
            });
        }

        [TestCase(2)]
        public void Delete_Sends_Data_Correctly(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[1]);

            // Act
            var result = (ViewResult)controller.Delete(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
                _mockRepository.Verify(x => x.GetEmployee(id), Times.Once());
            });
        }

        [TestCase(1)]
        public void Deletion_Removes_Employee(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.Delete(id)).Returns(true);

            // Act
            var result = (RedirectToActionResult) controller.Deletion(id);

            // Asserts
            Assert.Multiple(() =>
            {
                Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToActionResult)));
                Assert.That(result.ActionName, Is.EqualTo("Index"));
                _mockRepository.Verify(x => x.Delete(id), Times.Once());
            });
        }
    }
}
