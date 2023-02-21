using PayCalculatorAPI.Services;
using PayCalculatorLibrary.Models;

namespace PayCalculatorAPITest.Services
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
            var employee = _mapper.Map(_model);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee.GetType, Is.EqualTo(typeof(TemporaryEmployee)));
                Assert.That(employee.Name, Is.EqualTo(ModelName));
                Assert.That(employee.DayRate, Is.EqualTo(ModelDayRate));
                Assert.That(employee.WeeksWorked, Is.EqualTo(ModelWeeksWorked));
            });
        }
    }
}