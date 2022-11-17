using PayCalculator;

namespace PayCalculatorTest
{
    [TestFixture]
    public class PermEmployeeCalculatorTest
    {
        [Test]
        public void PermTotalPayIsCalculated()
        {
            var calculator = new PermPayCalc(); // Arrange
            var total = calculator.TotalAnnualPay(10000, 1000); // Act
            Assert.That(total, Is.EqualTo(11000)); // Assert
        }

        [Test]
        public void PermHourlyRateIsCalculated()
        {
            var calculator = new PermPayCalc(); // Arrange
            var hourlyRate = calculator.HourlyRate(10000, 200); // Act
            Assert.That(hourlyRate, Is.EqualTo(50)); // Assert
        }
    }
}