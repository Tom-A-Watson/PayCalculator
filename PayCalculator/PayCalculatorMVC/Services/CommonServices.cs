using PayCalculatorMVC.Enums;

namespace PayCalculatorMVC.Services
{
    public class CommonServices
    {
        public static string ShowAlert(Alerts alerts, string message)
        {
            string? alertDiv = null;
            switch (alerts)
            {
                case Alerts.Success:
                    alertDiv = "<div class='alert alert-success alert-dismissable' id='alert'><button type='button' " +
                        "class='close' data-dismiss='alert'>×</button><strong> Success! </strong> " + message + "</a>.</div>";
                    break;
                case Alerts.Danger:
                    alertDiv = "<div class='alert alert-danger alert-dismissible' id='alert'><button type='button' " +
                        "class='close' data-dismiss='alert'>×</button><strong> Error! </strong> " + message + "</a>.</div>";
                    break;
            }

            return alertDiv;
        }
    }
}
