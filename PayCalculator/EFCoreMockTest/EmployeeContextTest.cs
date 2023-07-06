using Moq;
using Moq.EntityFrameworkCore;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace EFCoreMockTest
{
    public class EmployeeContextTest
    {
#nullable disable
        private Mock<EmployeeContext> _mockEmployeeContext;

        private PersistentPermanentEmployeeRepo _persistentPermEmployeeRepo;
        private List<PermanentEmployee> _employees;
        private IPermanentPayCalculator _permPayCalculator;
        private ITimeCalculator _timeCalculator;
#nullable enable

        [SetUp]
        public void Setup()
        {
            _employees = new()
            {
                new PermanentEmployee() { Id = 1, Name = "William" },
                new PermanentEmployee() { Id = 2, Name = "Amir" }
            };

            _mockEmployeeContext = new();
            _persistentPermEmployeeRepo = new(_mockEmployeeContext.Object, _permPayCalculator, _timeCalculator);
        }

        [Test]
        public void Test_PermanentEmployee_GetAll()
        {
            // Arrange
            _mockEmployeeContext.Setup(x => x.PermanentEmployees).ReturnsDbSet(_employees);

            // Act
            var employeeList = _persistentPermEmployeeRepo.GetAll();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employeeList, Is.Not.Null);
                Assert.That(employeeList, Has.Exactly(2).Items);
            });
        }

        [TestCase(2)]
        public void Test_PermanentEmployee_GetEmployee(int id)
        {
            // Arrange
            _mockEmployeeContext.Setup(x => x.PermanentEmployees.Find(id)).Returns(_employees[1]);

            // Act
            var employee = _persistentPermEmployeeRepo.GetEmployee(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                Assert.That(employee.Name, Is.EqualTo("Amir"));
            });
        }
    }
}