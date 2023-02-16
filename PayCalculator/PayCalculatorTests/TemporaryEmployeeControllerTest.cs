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

        private const int Ok_200 = 200;
        private const int Accepted_202 = 202;
        private const int NotFound_404 = 404;
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
            var result = (OkObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(Ok_200));
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
                Assert.That(result?.StatusCode, Is.EqualTo(NotFound_404));
            });
        }

        [Test]
        [TestCase(2)]
        public void TestGetEmployeeReturnsOk(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[1]);

            // Act
            var response = controller.GetEmployee(id);
            var result = (OkObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(Ok_200));
            });
        }

        [Test]
        [TestCase(3)]
        public void TestGetEmployeeReturnsNotFound(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((TemporaryEmployee?) null);

            // Act
            var response = controller.GetEmployee(id);
            var result = (NotFoundObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(NotFound_404));
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
        [TestCase(1)]
        public void TestUpdateReturnsAccepted(int id)
        {
            // Arrange
            _mockMapper.Setup(x => x.Map(_createOrUpdateEmployeeModel)).Returns(_employees[0]);
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns(_employees[0]);

            // Act
            var response = controller.Update(id, _createOrUpdateEmployeeModel);
            var result = (AcceptedResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(Accepted_202));
            });
        }

        [Test]
        [TestCase(5)]
        public void TestUpdateReturnsNotFound(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((TemporaryEmployee?) null);

            // Act
            var response = controller.Update(id, _createOrUpdateEmployeeModel);
            var result = (NotFoundObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(NotFound_404));
            });
        }

        [Test]
        [TestCase(2)]
        public void TestDeleteReturnsAccepted(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.Delete(id)).Returns(true);

            // Act
            var response = controller.Delete(id);
            var result = (AcceptedResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(Accepted_202));
            });
        }

        [Test]
        [TestCase(10)]
        public void TestDeleteReturnsNotFound(int id)
        {
            // Arrange
            _mockRepository.Setup(x => x.Delete(id)).Returns(false);

            // Act
            var response = controller.Delete(id);
            var result = (NotFoundObjectResult) response;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result?.StatusCode, Is.EqualTo(NotFound_404));
            });
        }
    }
}