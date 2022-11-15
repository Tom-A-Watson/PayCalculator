using PayCalculator;

namespace PayCalculatorTest
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void PermTotalIsCalculated()
        {
            var calculator = new PermPayCalc();
            var total = calculator.TotalAnnualPay(10000, 1000);

            Assert.That(total, Is.EqualTo(11000));
        }
    }
}