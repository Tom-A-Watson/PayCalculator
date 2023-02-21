using PayCalculatorLibrary.Services;

namespace PayCalculatorTest.Services
{
    [TestFixture]
    public class TempEmployeeCalculatorTest
    {
        [Test]
        public void TempTotalPayIsCalculated()
        {
            // Arrange
            var calculator = new TemporaryPayCalculator();
            // Act
            var total = calculator.TotalAnnualPay(500, 40);
            // Assert
            Assert.That(total, Is.EqualTo(100000));
        }

        [Test]
        public void TempHourlyRateIsCalculated()
        {
            // Arrange
            var calculator = new TemporaryPayCalculator();
            // Act
            var hourlyRate = calculator.HourlyRate(350);
            // Assert
            Assert.That(hourlyRate, Is.EqualTo(50));
        }
    }
}
