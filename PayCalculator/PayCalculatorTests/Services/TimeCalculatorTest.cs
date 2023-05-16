using PayCalculatorLibrary.Services;

namespace PayCalculatorTest.Services
{
    [TestFixture]
    public class TimeCalculatorTest
    {
        [TestCase("2023, 5, 2", "2023, 5, 10", 49)]     // 1 week + another working day 
        [TestCase("2023, 5, 1", "2023, 5, 12", 70)]     // 2 full working weeks
        [TestCase("2023, 5, 1", "2023, 5, 2", 14)]      // 2 days
        [TestCase("2023, 5, 10", "2023, 5, 15", 28)]    // 4 working days wrapping around a weekend
        [TestCase("2023, 1, 1", "2023, 5, 16", 679)]    // Start of the year to present day
        public void HoursWorked_Returns_Expected_Value(string stringStartDate, string stringEndDate, int expectedHoursWorked)
        {
            // Arrange
            var calculator = new TimeCalculator();
            var startDate = DateTime.Parse(stringStartDate);
            var endDate = DateTime.Parse(stringEndDate);

            // Act
            var result = calculator.HoursWorked(startDate, endDate);

            // Assert
            Assert.That(result, Is.EqualTo(expectedHoursWorked));
        }
    }
}
