namespace PayCalculator
{
    public class Contractor
    {
        double dayRate;
        int weeksWorkedAnnually;

        public Contractor(double dayRate, int weeksWorkedAnnually)
        {
            this.dayRate = dayRate;
            this.weeksWorkedAnnually = weeksWorkedAnnually;
        }

        public double TotalAnnualPay()
        {
            return dayRate * weeksWorkedAnnually;
        }

        public double HourlyPay()
        {

        }
    }
}