using PayCalculatorAPI.Services;
using PayCalculatorLibrary.Models;

namespace PayCalculatorAPITest.Services
{
    [TestFixture]
    public class PermanentEmployeeMapperTest
    {
#nullable disable
        private PermanentEmployeeMapper _mapper;
        private CreateOrUpdatePermanentEmployee _model;
        private const string ModelName = "william";
        private const decimal ModelSalary = 40000;
        private const decimal ModelBonus = 2000;
        private const int ModelHoursWorked = 150;
#nullable enable

        [Test]
        public void TestTemporaryEmployeeMapping()
        {
            // Arrange
            _mapper = new();
            _model = new()
            {
                Name = ModelName,
                Salary = ModelSalary,
                Bonus = ModelBonus,
                HoursWorked = ModelHoursWorked
            };

            // Act
            var employee = _mapper.Map(_model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee.GetType, Is.EqualTo(typeof(PermanentEmployee)));
                Assert.That(employee.Name, Is.EqualTo(ModelName));
                Assert.That(employee.Salary, Is.EqualTo(ModelSalary));
                Assert.That(employee.Bonus, Is.EqualTo(ModelBonus));
                Assert.That(employee.HoursWorked, Is.EqualTo(ModelHoursWorked));
            });
        }
    }
}