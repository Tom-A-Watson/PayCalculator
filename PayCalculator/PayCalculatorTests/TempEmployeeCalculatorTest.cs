using PayCalculator.Services;

namespace PayCalculatorTests
{
    [TestFixture]
    public class TempEmployeeCalculatorTest
    {
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
