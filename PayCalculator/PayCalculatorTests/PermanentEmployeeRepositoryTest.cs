using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;

namespace PayCalculatorTest
{
    [TestFixture]
    public class PermanentEmployeeRepositoryTest
    {
#nullable disable
        private PermanentEmployeeRepository repository;
        private PermanentEmployee testEmployee;
        private PermanentEmployee employee;
        private bool deleted;
#nullable enable

        [SetUp]
        public void SetupTests()
        {
            // Arrange
            repository = new();
            
            testEmployee = new()
            {
                Id = 3,
                Name = "mark",
                Salary = 20000,
                Bonus = 5000,
                Contract = ContractType.Permanent,
                HoursWorked = 1820
            };
        }

        [Test]
        public void TestGetEmployeeWorks()
        { 
            // Act
            employee = repository.GetEmployee(2);
            // Assert
            Assert.That(employee.Name, Is.EqualTo("John Smith"));
        }

        [Test]
        public void TestGetEmployeeFails()
        {
            // Act
            employee = repository.GetEmployee(3);
            // Assert
            Assert.IsNull(employee);
        }

        [Test]
        public void TestCreateEmployeeWorks()
        {   
            // Act
            employee = repository.Create(testEmployee);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(repository.GetAll().Count(), Is.EqualTo(3));
                Assert.That(employee.Name, Is.EqualTo("mark"));
            });
        }

        [Test]
        public void TestDeleteEmployeeWorks()
        {
            // Act
            deleted = repository.Delete(1);

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
            deleted = repository.Delete(4);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(repository.GetAll().Count(), Is.EqualTo(2));
                Assert.IsFalse(deleted);
            });
        }
    }
}
