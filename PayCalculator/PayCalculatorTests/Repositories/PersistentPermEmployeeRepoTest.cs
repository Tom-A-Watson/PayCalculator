using Microsoft.EntityFrameworkCore;
using PayCalculatorData;
using PayCalculatorLibrary.Models;

namespace PayCalculatorTest.Repositories
{
    [TestFixture]
    public class PersistentPermEmployeeRepoTest
    {
#nullable disable
        private PermanentEmployee testEmployee;
#nullable enable
        [SetUp]
        public void SetUp() 
        {
            testEmployee = new()
            {
                Name = "Lucy",
                Salary = 1000m,
                Bonus = 1000,
                StartDate = new DateTime(2023, 5, 1)
            };
        }

        [Test]
        public void CanInsertEmployeeIntoDatabase()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<EmployeeContext>();
            var context = new EmployeeContext(builder.Options);

            // Act
            builder.UseInMemoryDatabase("CanInsertEmployeeIntoDatabase");
            context.PermanentEmployees.Add(testEmployee);
            context.SaveChanges();

            // Assert
            Assert.That(testEmployee.Id, Is.Not.EqualTo(0));
        }
    }
}
