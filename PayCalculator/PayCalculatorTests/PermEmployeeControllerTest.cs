using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Controllers;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using Moq;
using NuGet.Frameworks;

namespace PayCalculatorTest
{
    [TestFixture]
    public class PermEmployeeControllerTest
    {
#nullable disable
        private int id;
        private PermanentEmployee permanentEmployee;
        private List<PermanentEmployee> _employees;
        private CreateOrUpdatePermanentEmployee employee;
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
            id = 1;
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[0]);

            // Act
            var response = controller.GetEmployee(id);
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
            id = 3;
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((PermanentEmployee?) null);

            // Act
            var response = controller.GetEmployee(id);
            var result = response as NotFoundObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(404));
            });
        }

        [Test]
        public void TestCreateEmployeeReturnsCreated()
        {
            // Arrange
            employee = new();
            _mockRepository.Setup(x => x.Create(permanentEmployee)).Returns(permanentEmployee);

            // Act
            var response = controller.Create(employee);
            var result = response as CreatedAtActionResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(201));
            });
        }

        [Test]
        public void TestUpdateReturnsAccepted()
        {
            // Arrange
            _mockRepository.Setup(x => x.Update(permanentEmployee)).Returns(permanentEmployee);
            
            // Act
            var response = controller.Update(permanentEmployee);
            var result = response as AcceptedAtActionResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(202));
            });
        }

        [Test]
        public void TestUpdateReturnsNotFound()
        {
            // Arrange
            // Act
            // Assert
        }

        [Test]
        public void TestDeleteReturnsAccepted()
        {
            // Arrange
            // Act
            // Assert
        }

        [Test]
        public void TestDeleteReturnsNotFound()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
