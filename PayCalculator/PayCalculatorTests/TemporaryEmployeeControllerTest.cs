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
    public class TemporaryEmployeeControllerTest
    {
#nullable disable
        private List<TemporaryEmployee> _employees;
        private CreateOrUpdateTemporaryEmployee _createOrUpdateEmployeeModel;
        private TemporaryEmployeeController controller;

        private Mock<ITemporaryEmployeeMapper> _mockMapper;
        private Mock<IEmployeeRepository<TemporaryEmployee>> _mockRepository;
        private Mock<TemporaryPayCalculator> _mockCalculator;

        private const string EmployeeName = "Tom Watson";
        private const decimal EmployeeDayRate = 1200;
        private const int EmployeeWeeksWorked = 42;
#nullable enable

        [SetUp]
        public void Setup()
        {
            _employees = new List<TemporaryEmployee>()
            {
                new TemporaryEmployee()
                {
                    Id = 1,
                    Name = EmployeeName,
                    DayRate = EmployeeDayRate,
                    WeeksWorked = EmployeeWeeksWorked
                },

                new TemporaryEmployee()
                {
                    Id= 2,
                    Name = "micheal",
                    DayRate = 500,
                    WeeksWorked = 30
                }
            };

            _createOrUpdateEmployeeModel = new()
            {
                Name = EmployeeName,
                DayRate = EmployeeDayRate,
                WeeksWorked = EmployeeWeeksWorked
            };

            _mockRepository = new();
            _mockCalculator = new();
            _mockMapper = new();
            controller = new TemporaryEmployeeController(_mockRepository.Object, _mockCalculator.Object, _mockMapper.Object);
        }

        [Test]
        public void TestGetAllReturnsOk()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_employees);

            // Act
            var response = controller.Get();
            var result = (OkObjectResult)response;

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
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((TemporaryEmployee?) null);

            // Act
            var response = controller.GetEmployee(id);
            var result = (NotFoundObjectResult)response;

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
            var result = (CreatedResult)response;

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
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.Update(_employees[0])).Returns(_employees[0]);

            // Act
            var response = controller.Update(1, _createOrUpdateEmployeeModel);
            var result = (AcceptedResult)response;

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
            _createOrUpdateEmployeeModel = null;
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.Update(_employees[0])).Returns(_employees[0]);

            // Act
            var response = controller.Update(1, _createOrUpdateEmployeeModel);
            var result = (NotFoundObjectResult)response;

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
            var result = (AcceptedResult)response;

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
            var result = (NotFoundObjectResult)response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(404));
            });
        }
    }
}
