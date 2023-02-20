using PayCalculatorAPI.Services;
using PayCalculatorLibrary.Models;

namespace PayCalculatorTest
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
            var _employee = _mapper.Map(_model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_employee.GetType, Is.EqualTo(typeof(PermanentEmployee)));
                Assert.That(_employee.Name, Is.EqualTo(ModelName));
                Assert.That(_employee.Salary, Is.EqualTo(ModelSalary));
                Assert.That(_employee.Bonus, Is.EqualTo(ModelBonus));
                Assert.That(_employee.HoursWorked, Is.EqualTo(ModelHoursWorked));
            });
        }
    }
}