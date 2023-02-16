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

        private const int Ok_200 = 200;
        private const int Accepted_202 = 202;
        private const int NotFound_404 = 404;
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
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((PermanentEmployee?) null);

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
            _mockRepository.Setup(x => x.GetEmployee(id)).Returns((PermanentEmployee?) null);

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