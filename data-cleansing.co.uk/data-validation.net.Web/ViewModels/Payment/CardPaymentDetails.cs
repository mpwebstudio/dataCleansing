namespace data_validation.net.Web.ViewModels.Payment
{
    public class CardPaymentDetails
    {
        public string CVV2 { get; set; }

        public int ExpireMonth { get; set; }

        public int ExpireYear { get; set; }

        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }

        public string Title { get; set; }
    }
}