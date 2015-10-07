namespace data_validation.net.Web.ViewModels.Payment
{
    public class PayPalDetails
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string ItemName { get; set; }

        public string ItemPrice { get; set; }

        public string InvoiceNo { get; set; }
    }
}