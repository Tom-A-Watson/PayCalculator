using PayCalculatorAPI.Services;
using PayCalculatorLibrary.Models;

namespace PayCalculatorTest
{
    [TestFixture]
    public class TemporaryEmployeeMapperTest
    {
#nullable disable
        private TemporaryEmployeeMapper _mapper;
        private CreateOrUpdateTemporaryEmployee _model;
        private const string ModelName = "jonathan";
        private const decimal ModelDayRate = 600;
        private const int ModelWeeksWorked = 40;
#nullable enable

        [Test]
        public void TestTemporaryEmployeeMapping()
        {
            // Arrange
            _mapper = new();
            _model = new()
            {
                Name = ModelName,
                DayRate = ModelDayRate,
                WeeksWorked = ModelWeeksWorked
            };

            // Act
            var _employee = _mapper.Map(_model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_employee.GetType, Is.EqualTo(typeof(TemporaryEmployee)));
                Assert.That(_employee.Name, Is.EqualTo(ModelName));
                Assert.That(_employee.DayRate, Is.EqualTo(ModelDayRate));
                Assert.That(_employee.WeeksWorked, Is.EqualTo(ModelWeeksWorked));
            });
        }
    }
}