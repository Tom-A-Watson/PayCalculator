using Microsoft.EntityFrameworkCore;
using PayCalculatorLibrary.Models;
using PayCalculatorLibrary.Repositories;
using PayCalculatorLibrary.Services;

namespace PayCalculatorTest.Repositories
{
    [TestFixture]
    public class PersistentPermEmployeeRepoTest
    {
#nullable disable
        private List<PermanentEmployee> employees;
        private PermanentEmployee _updateModel;
        private PersistentPermanentEmployeeRepo repository;

        private const string EmployeeName = "Jake";
        private const decimal EmployeeSalary = 20000;
        private const decimal EmployeeBonus = 5000;
        private DateTime EmployeeStartDate = new(2023, 5, 1);

        private DbContextOptionsBuilder builder = new DbContextOptionsBuilder<EmployeeContext>();
        private EmployeeContext context;
        private DbContextOptions options;
        private PermanentPayCalculator payCalculator;
        private TimeCalculator timeCalculator;
        private PermanentEmployee testEmployee;
        private PermanentEmployee employee;
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
                new PermanentEmployee { Id = 1, Name = "Tom" },
                new PermanentEmployee { Id = 2, Name = "Harry" },
                new PermanentEmployee { Id = 3, Name = "Kyle"},
                new PermanentEmployee { Id = 4, Name = "Alex"}
            };

            context.PermanentEmployees.AddRange(employees);
            context.SaveChanges();

            testEmployee = new PermanentEmployee()
            {
                Name = "Lucy",
                Salary = 1000,
                Bonus = 1000,
                StartDate = new DateTime(2023, 5, 1)
            };

            _updateModel = new PermanentEmployee()
            {
                Id = 1,
                Name = EmployeeName,
                Salary = EmployeeSalary,
                Bonus = EmployeeBonus,
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
            context.PermanentEmployees.Add(testEmployee);
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
                Assert.That(employee.TotalAnnualPay, Is.EqualTo(2000));
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