using PayCalculator.Services;

namespace PayCalculatorTest
{
    [TestFixture]
    public class PermEmployeeCalculatorTest
    {
        [Test]
        public void PermTotalPayIsCalculated()
        {
            // Arrange
            var calculator = new PermanentPayCalculator();
            // Act
            var total = calculator.TotalAnnualPay(10000, 1000);
            // Assert
            Assert.That(total, Is.EqualTo(11000)); 
        }

        [Test]
        public void PermHourlyRateIsCalculated()
        {
            // Arrange
            var calculator = new PermanentPayCalculator(); 
            // Act
            var hourlyRate = calculator.HourlyRate(10000, 200); 
            // Assert
            Assert.That(hourlyRate, Is.EqualTo(50)); 
        }
    }
}