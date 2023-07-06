using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorTest.Repositories
{
    [TestFixture]
    public class PersistentTempEmployeeRepoTest
    {
#nullable disable
        private List<TemporaryEmployee> employees;
        private TemporaryEmployee _updateModel;
        private PersistentTemporaryEmployeeRepo repository;

        private const string EmployeeName = "Jake";
        private const decimal EmployeeDayRate = 250;
        private DateTime EmployeeStartDate = new(2023, 5, 1);

        private DbContextOptionsBuilder builder = new DbContextOptionsBuilder<EmployeeContext>();
        private EmployeeContext context;
        private DbContextOptions options;
        private TemporaryPayCalculator payCalculator;
        private TimeCalculator timeCalculator;
        private TemporaryEmployee testEmployee;
        private TemporaryEmployee employee;
        private bool deleted;
#nullable enable

        [SetUp]
        public void SetUp()
        {
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options = builder.Options;
            context = new EmployeeContext(options);
            context.Database.EnsureCreated();

            employees = new()
            {
                new TemporaryEmployee { Id = 1, Name = "Tom" },
                new TemporaryEmployee { Id = 2, Name = "Harry" },
                new TemporaryEmployee{ Id = 3, Name = "Kyle"},
                new TemporaryEmployee { Id = 4, Name = "Alex"}
            };

            context.TemporaryEmployees.AddRange(employees);
            context.SaveChanges();

            testEmployee = new()
            {
                Name = "Lucy",
                DayRate = 300,
                StartDate = new DateTime(2023, 5, 1)
            };

            _updateModel = new()
            {
                Id = 1,
                Name = EmployeeName,
                DayRate = EmployeeDayRate,
                StartDate = EmployeeStartDate
            };

            payCalculator = new();
            timeCalculator = new();
            repository = new(context, payCalculator, timeCalculator);
        }

        [Test]
        public void CanInsertEmployeeIntoDatabase()
        {
            // Act
            context.TemporaryEmployees.Add(testEmployee);
            context.SaveChanges();

            // Assert
            Assert.That(testEmployee.Id, Is.Not.EqualTo(0));
        }


        [Test]
        public void TestGetEmployeeWorks()
        {
            // Act
            employee = repository.GetEmployee(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                Assert.That(employee.Name, Is.EqualTo("Tom"));
            });
        }

        [Test]
        public void TestGetEmployeeFails()
        {
            // Act
            employee = repository.GetEmployee(40);

            // Assert
            Assert.That(employee, Is.Null);
        }

        [Test]
        public void TestCreateEmployeeWorks()
        {
            // Act
            employee = repository.Create(testEmployee);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                Assert.That(employee.Id, Is.Not.EqualTo(0));
                Assert.That(employee.Name, Is.EqualTo("Lucy"));
                Assert.That(Math.Round(employee.HourlyRate, 2), Is.EqualTo(42.86));
                Assert.That(repository.GetAll(), Has.Exactly(5).Items);
            });
        }

        [Test]
        public void TestUpdateEmployeeWorks()
        {
            // Act
            employee = repository.GetEmployee(1);
            employee = repository.Update(_updateModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                Assert.That(employee.Name, Is.EqualTo("Jake"));
                Assert.That(employee.StartDate, Is.EqualTo(new DateTime(2023, 5, 1)));
                Assert.That(repository.GetAll(), Has.Exactly(4).Items);
            });
        }

        [Test]
        public void TestUpdateEmployeeFails()
        {
            // Act
            testEmployee.Id = 10;
            employee = repository.Update(testEmployee);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Null);
                Assert.That(repository.GetAll(), Has.Exactly(4).Items);
            });
        }

        [Test]
        public void TestDeleteEmployeeWorks()
        {
            // Act
            deleted = repository.Delete(3);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(deleted);
                Assert.That(repository.GetAll(), Has.Exactly(3).Items);
                Assert.That(repository.GetEmployee(3), Is.Null);
            });
        }

        [Test]
        public void TestDeleteEmployeeFails()
        {
            // Act
            deleted = repository.Delete(20);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(deleted);
                Assert.That(repository.GetAll(), Has.Exactly(4).Items);
            });
        }
    }
}