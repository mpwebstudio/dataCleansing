namespace data_validation.net.Web.Models.ViewModels
{
    public class EmailValidationViewModel
    {
        public string Email { get; set; }

        public string IsValid { get; set; }

        public string MxRecord { get; set; }

        public string Message { get; set; }
    }
}