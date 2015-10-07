namespace data_validation.net.WebApi.Controllers
{
    using data_validation.net.Web.ViewModels;
    
    public class IsFraudController
    {
        public bool Check(string userName,string ip, string host)
        {
            var fraud = new data_validation.net.Web.Controllers.Helpers.CreditsDeduct();

            var isFraud = fraud.CheckForFraud(userName, ip, host);

            return isFraud;
        }
    }
}