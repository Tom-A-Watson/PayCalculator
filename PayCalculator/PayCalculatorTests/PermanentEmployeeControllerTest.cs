using Microsoft.AspNetCore.Mvc;
using PayCalculatorAPI.Controllers;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;
using Moq;
using PayCalculatorAPI.Services;

namespace PayCalculatorTest
{
    [TestFixture]
    public class PermanentEmployeeControllerTest
    {
#nullable disable
        private List<PermanentEmployee> _employees;
        private CreateOrUpdatePermanentEmployee _createOrUpdateEmployeeModel;
        private PermanentEmployeeController controller;

        private Mock<IPermanentEmployeeMapper> _mockMapper;
        private Mock<IEmployeeRepository<PermanentEmployee>> _mockRepository;
        private Mock<PermanentPayCalculator> _mockCalculator;
        
        private const string EmployeeName = "Tom Watson";
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

            _mockRepository = new();
            _mockCalculator = new();
            _mockMapper = new();
            controller = new PermanentEmployeeController(_mockRepository.Object, _mockCalculator.Object, _mockMapper.Object);
        }
        
        [Test]
        public void TestGetAllReturnsOk()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_employees);
            
            // Act
            var response = controller.Get();
            var result = (OkObjectResult) response;

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
            var result = (NotFoundObjectResult) response;

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
            var id = 2;
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[0]);

            // Act
            var response = controller.GetEmployee(id);
            var result = (OkObjectResult) response;

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
            var id = 3;
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((PermanentEmployee?) null);

            // Act
            var response = controller.GetEmployee(id);
            var result = (NotFoundObjectResult) response;

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
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.Create(_employees[0])).Returns(_employees[0]);

            // Act
            var response = controller.Create(_createOrUpdateEmployeeModel);
            var result = (CreatedResult) response;

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
            var id = 1;
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[0]);
            
            // Act
            var response = controller.Update(id, _createOrUpdateEmployeeModel);
            var result = (AcceptedResult) response;

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
            var id = 5;
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((PermanentEmployee?) null);

            // Act
            var response = controller.Update(id, _createOrUpdateEmployeeModel);
            var result = (NotFoundObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(404));
            });
        }

        [Test]
        public void TestDeleteReturnsAccepted()
        {
            // Arrange
            var id = 2;
            _mockRepository.Setup(x => x.Delete(id)).Returns(true);

            // Act
            var response = controller.Delete(id);
            var result = (AcceptedResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(202));
            });
        }

        [Test]
        public void TestDeleteReturnsNotFound()
        {
            // Arrange
            var id = 10;
            _mockRepository.Setup(x => x.Delete(id)).Returns(false);

            // Act
            var response = controller.Delete(id);
            var result = (NotFoundObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(404));
            });
        }
    }
}
