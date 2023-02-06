using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;

namespace PayCalculatorTest
{
    [TestFixture]
    public class TemporaryEmployeeRepositoryTest
    {
#nullable disable
        // Arrange
        private TemporaryEmployeeRepository repository;
        private TemporaryEmployee blankEmployee;
        private TemporaryEmployee employee;
        private bool deleted;
#nullable enable

        [SetUp]
        public void SetupTests()
        {
            // Arrange
            repository = new TemporaryEmployeeRepository();

            blankEmployee = new()
            {
                Id = 1,
                Name = "ben",
                DayRate = 250,
                Contract = ContractType.Temporary,
                WeeksWorked = 46
            };
        }

        [Test]
        public void TestGetEmployeeWorks()
        {
            // Act
            employee = repository.GetEmployee(3);
            // Assert
            Assert.That(employee.Name, Is.EqualTo("Matt Burns"));
        }

        [Test]
        public void TestGetEmployeeFails()
        {
            // Act
            employee = repository.GetEmployee(2);
            // Assert
            Assert.IsNull(employee);
        }

        [Test]
        public void TestCreateEmployeeWorks()
        {
            // Act
            employee = repository.Create(blankEmployee);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(repository.GetAll().Count(), Is.EqualTo(3));
                Assert.That(employee.Name, Is.EqualTo("ben"));
            });
        }

        [Test]
        public void TestDeleteEmployeeWorks()
        {
            // Act
            deleted = repository.Delete(4);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(repository.GetAll().Count(), Is.EqualTo(1));
                Assert.IsTrue(deleted);
            });
        }

        [Test]
        public void TestDeleteEmployeeFails()
        {
            // Act
            deleted = repository.Delete(14);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(repository.GetAll().Count(), Is.EqualTo(2));
                Assert.IsFalse(deleted);
            });
        }
    }
}
