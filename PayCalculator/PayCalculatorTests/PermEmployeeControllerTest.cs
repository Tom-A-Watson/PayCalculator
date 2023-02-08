using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Controllers;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using Moq;

namespace PayCalculatorTest
{
    [TestFixture]
    public class PermEmployeeControllerTest
    {
#nullable disable
        private PermanentEmployee employee;
        private List<PermanentEmployee> _employees;
        private PermanentEmployeeController controller;
        private Mock<IEmployeeRepository<PermanentEmployee>> _mockRepository;
        private Mock<PermanentPayCalculator> _mockCalculator;
#nullable enable

        [SetUp]
        public void Setup()
        {
            // Arrange
            _employees = new List<PermanentEmployee>()
            {
                new PermanentEmployee()
                {
                    Id = 1,
                    Name = "nathan",
                    Salary = 20000,
                    Bonus = 100,
                    HoursWorked = 1820
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

            _mockRepository = new();
            _mockCalculator = new();
            controller = new PermanentEmployeeController(_mockRepository.Object, _mockCalculator.Object);
        }
        
        [Test]
        public void TestGetAllReturnsOk()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_employees);
            
            // Act
            var response = controller.Get();
            var result = response as OkObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public void TestGetAllReturnsNotFound()
        {
            // Arrange
            _employees.Clear();
            _mockRepository.Setup(x => x.GetAll()).Returns(_employees);

            // Act
            var response = controller.Get();
            var result = response as NotFoundObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result); 
                Assert.That(result?.StatusCode, Is.EqualTo(404));
            });
        }

        [Test]
        public void TestGetEmployeeReturnsOk()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(1)).Returns(_employees[0]);

            // Act
            var response = controller.GetEmployee(1);
            var result = response as OkObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(200));
            });
        }

        [Test]
        public void TestGetEmployeeReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(3)).Returns((PermanentEmployee) null!);

            // Act
            var response = controller.GetEmployee(3);
            var result = response as NotFoundObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(404));
            });
        }
    }
}
