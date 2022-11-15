using PayCalculator;

namespace PayCalculatorTest
{
    [TestFixture]
    public class CalculatorTest
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

        [Test]
        public void TempTotalPayIsCalculated()
        {
            var calculator = new TempPayCalc(); // Arrange
            var total = calculator.TotalAnnualPay(500, 40); // Act
            Assert.That(total, Is.EqualTo(100000)); // Assert
        }

        [Test]
        public void TempHourlyRateIsCalculated()
        {
            var calculator = new TempPayCalc(); // Arrange
            var hourlyRate = calculator.HourlyRate(350); // Act
            Assert.That(hourlyRate, Is.EqualTo(50)); // Assert
        }
    }
}