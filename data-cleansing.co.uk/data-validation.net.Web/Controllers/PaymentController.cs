namespace data_validation.net.Web.Controllers
{
    using data_cleansing.net.Data;
    using data_validation.net.Web.ViewModels;
    using data_validation.net.Web.ViewModels.Payment;
    using PayPal.Api;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class PaymentController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        private static class PaymentInfo
        {
            public static string CVV2 { get; set; }

            public static int expireMonth { get; set; }

            public static int expireYear { get; set; }

            public static string firstName { get; set; }

            public static string surName { get; set; }

            public static string cardNumber { get; set; }

            public static string title { get; set; }

            public static int userAddress { get; set; }

            public static int credits { get; set; }

            public static string totalAmount { get; set; }

            public static string invoice { get; set; }

        }


        public static List<RegionInfo> GetCountriesByIso3166()
        {
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo country = new RegionInfo(culture.LCID);
                if (countries.Where(p => p.Name == country.Name).Count() == 0)
                    countries.Add(country);
            }
            return countries.OrderBy(p => p.EnglishName).ToList();
        }

        //public ActionResult PaymentWithCreditCard()
        //{
        //    var cardStaus = CardType.CheckStatus(PaymentInfo.cardNumber);

        //    if (cardStaus.IsValid == false)
        //    {
        //        return View("FailureView");
        //    }

        //    //create and item for which you are taking payment
        //    //if you need to add more items in the list
        //    //Then you will need to create multiple item objects or use some loop to instantiate object
        //    Item item = new Item();
        //    item.name = "Credits Purchase";
        //    item.currency = "GBP";
        //    item.price = PaymentInfo.totalAmount;
        //    item.quantity = "1";
        //    item.sku = "sku";

        //    //Now make a List of Item and add the above item to it
        //    //you can create as many items as you want and add to this list
        //    List<Item> itms = new List<Item>();
        //    itms.Add(item);
        //    ItemList itemList = new ItemList();
        //    itemList.items = itms;

        //    DataValidationData db = new DataValidationData();

        //    var billingInfo = db.Profiles.Where(x => x.Id == PaymentInfo.userAddress && x.UserName == User.Identity.Name).First();

        //    PaymentInfo.invoice = (db.Invoices.Max(x => x.InvoiceNo) + 1).ToString();

        //    //Address for the payment
        //    Address billingAddress = new Address();
        //    //billingAddress.city = billingInfo.City;
        //    //billingAddress.country_code = billingInfo.Country;
        //    //billingAddress.line1 = billingInfo.Address + billingInfo.Address1;
        //    //billingAddress.postal_code = billingInfo.PostCode;
        //    //billingAddress.state = "NY";

        //    //Now Create an object of credit card and add above details to it
        //    //Please replace your credit card details over here which you got from paypal
        //    CreditCard crdtCard = new CreditCard();
        //    crdtCard.billing_address = billingAddress;
        //    crdtCard.cvv2 = PaymentInfo.CVV2;  //card cvv2 number
        //    crdtCard.expire_month = PaymentInfo.expireMonth; //card expire date
        //    crdtCard.expire_year = PaymentInfo.expireYear; //card expire year
        //    crdtCard.first_name = PaymentInfo.firstName;
        //    crdtCard.last_name = PaymentInfo.surName;
        //    crdtCard.number = PaymentInfo.cardNumber; //enter your credit card number here
        //    crdtCard.type = cardStaus.Type;

        //    //crdtCard.type = "visa"; //credit card type here paypal allows 4 types

        //    // Specify details of your payment amount.
        //    Details details = new Details();
        //    details.shipping = "0";
        //    details.subtotal = PaymentInfo.totalAmount;
        //    details.tax = "0";

        //    // Specify your total payment amount and assign the details object
        //    Amount amnt = new Amount();
        //    amnt.currency = "GBP";
        //    // Total = shipping tax + subtotal.
        //    amnt.total = PaymentInfo.totalAmount;
        //    amnt.details = details;

        //    // Now make a transaction object and assign the Amount object
        //    Transaction tran = new Transaction();
        //    tran.amount = amnt;
        //    tran.description = "Description about the payment amount.";
        //    tran.item_list = itemList;
        //    tran.invoice_number = PaymentInfo.invoice;

        //    // Now, we have to make a list of transaction and add the transactions object
        //    // to this list. You can create one or more object as per your requirements

        //    List<Transaction> transactions = new List<Transaction>();
        //    transactions.Add(tran);

        //    // Now we need to specify the FundingInstrument of the Payer
        //    // for credit card payments, set the CreditCard which we made above

        //    FundingInstrument fundInstrument = new FundingInstrument();
        //    fundInstrument.credit_card = crdtCard;

        //    // The Payment creation API requires a list of FundingIntrument

        //    List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
        //    fundingInstrumentList.Add(fundInstrument);

        //    // Now create Payer object and assign the fundinginstrument list to the object
        //    Payer payr = new Payer();
        //    payr.funding_instruments = fundingInstrumentList;
        //    payr.payment_method = "credit_card";

        //    // finally create the payment object and assign the payer object & transaction list to it
        //    Payment pymnt = new Payment();
        //    pymnt.intent = "sale";
        //    pymnt.payer = payr;
        //    pymnt.transactions = transactions;

        //    try
        //    {
        //        //getting context from the paypal
        //        //basically we are sending the clientID and clientSecret key in this function
        //        //to the get the context from the paypal API to make the payment
        //        //for which we have created the object above.

        //        //Basically, apiContext object has a accesstoken which is sent by the paypal
        //        //to authenticate the payment to facilitator account.
        //        //An access token could be an alphanumeric string

        //        APIContext apiContext = Configuration.GetAPIContext();

        //        //Create is a Payment class function which actually sends the payment details
        //        //to the paypal API for the payment. The function is passed with the ApiContext
        //        //which we received above.

        //        Payment createdPayment = pymnt.Create(apiContext);

        //        //if the createdPayment.state is "approved" it means the payment was successful else not

        //        if (createdPayment.state.ToLower() != "approved")
        //        {
        //            return View("FailureView");
        //        }
        //    }
        //    catch (PayPal.PayPalException ex)
        //    {
        //        Logger.Log("Error: " + ex.Message);
        //        return View("FailureView");
        //    }

        //    db.Invoices.Add(new Invoice
        //    {
        //        InvoiceNo = Int32.Parse(PaymentInfo.invoice),
        //        Amount = Int32.Parse(PaymentInfo.totalAmount),
        //        PaymentType = "CreditCard",
        //        Date = DateTime.Now,
        //        CreditsPurchase = PaymentInfo.credits,
        //        UserName = User.Identity.Name,
        //        ProfileId = PaymentInfo.userAddress
        //    });

        //    try
        //    {
        //        var userCredits = db.Credits.Single(x => x.UserName == User.Identity.Name);
        //        userCredits.Credits = userCredits.Credits + PaymentInfo.credits;
        //        userCredits.DatePurchase = DateTime.Now;

        //        db.Entry(userCredits).State = System.Data.Entity.EntityState.Modified;
        //    }
        //    catch
        //    {
        //        db.Credits.Add(new Credit()
        //        {
        //            UserName = User.Identity.Name,
        //            DatePurchase = DateTime.Now,
        //            Credits = PaymentInfo.credits
        //        });
        //    }

        //    db.SaveChanges();

        //    return View("SuccessView");
        //}

        public List<UserPaymentDetails> paymentDetails = new List<UserPaymentDetails>();

        public ActionResult PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {

                    //this section will be executed first because PayerID doesn't exist

                    //it is returned by the create function call of the payment class

                    // Creating a payment

                    // baseURL is the url on which paypal sendsback the data.

                    // So we have provided URL of this controller only

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session

                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url

                    //on which payer is redirected for paypal acccount payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    if (paymentDetails.Count == 0)
                    {
                        string inv = string.Empty;
                        string pay = string.Empty;
                        foreach (var item in createdPayment.transactions)
                        {
                            inv = item.invoice_number;
                            pay = item.amount.total;
                        }

                        paymentDetails.Add(new UserPaymentDetails() { Amount = pay, InvoiceNo = Int32.Parse(inv) });
                    }

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error" + ex.Message);
                return View("FailureView");
            }

            db.Invoice.Add(new data_cleansing.net.Models.Invoice()
            {
                InvoiceNo = Int32.Parse(PaymentInfo.invoice),
                Amount = Int32.Parse(PaymentInfo.totalAmount),
                PaymentType = "PayPal",
                Date = DateTime.Now,
                CreditsPurchase = PaymentInfo.credits,
                UserName = User.Identity.Name,
                ProfileId = PaymentInfo.userAddress
            });

            var userCredits = db.Credits.Single(x => x.UserName == User.Identity.Name);

            userCredits.Credits = userCredits.Credits + PaymentInfo.credits;

            userCredits.DateExpire = DateTime.Now.AddYears(1);

            userCredits.DatePurchase = DateTime.Now;

            db.Entry(userCredits).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            //LINQ do not accept parse into query!!!
            var invTemp = Int32.Parse(PaymentInfo.invoice);

            var invoiceNumber = db.Invoice.Single(x => x.InvoiceNo == invTemp);

            ViewBag.Credits = invoiceNumber.CreditsPurchase.ToString();
            ViewBag.Invoice = invoiceNumber.Id.ToString();
            ViewBag.Amount = invoiceNumber.Amount.ToString();

            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Add on credits",
                currency = "GBP",
                price = PaymentInfo.totalAmount,
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = PaymentInfo.totalAmount
            };

            // similar as we did for credit card, do here and create amount object

            var amount = new Amount()
            {
                currency = "GBP",
                total = PaymentInfo.totalAmount, // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            PaymentInfo.invoice = (db.Invoice.Max(x => x.InvoiceNo) + 1).ToString();

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Add on credits",
                invoice_number = PaymentInfo.invoice,
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        public void BillingAddress(data_cleansing.net.Models.Profile billinAddress)
        {
            PaymentInfo.userAddress = db.Profile.Where(x => x.UserName == User.Identity.Name).Max(x => x.Id);

            var pesho = new List<ResultMessage>();

            pesho.Add(new ResultMessage { Message = "success" });
        }

        public void PayPalPrePayment(string id)
        {
            var pesho = new List<ResultMessage>();

            switch (id)
            {
                case "25": PaymentInfo.credits = 3000; PaymentInfo.totalAmount = "25"; break;
                case "50": PaymentInfo.credits = 7200; PaymentInfo.totalAmount = "50"; break;
                case "100": PaymentInfo.credits = 15600; PaymentInfo.totalAmount = "100"; break;
                case "150": PaymentInfo.credits = 25800; PaymentInfo.totalAmount = "150"; break;
                case "200": PaymentInfo.credits = 41400; PaymentInfo.totalAmount = "200"; break;
                case "500": PaymentInfo.credits = 126000; PaymentInfo.totalAmount = "500"; break;

                default: pesho.Add(new ResultMessage { Message = "false" }); break;
            }

            if (pesho.Count == 0)
            {
                pesho.Add(new ResultMessage { Message = "success" });
            }
        }

        public JsonResult UserAddress(int id)
        {
            PaymentInfo.userAddress = id;

            var cmd = new List<ResultMessage>();

            cmd.Add(new ResultMessage { Message = "success" });

            return Json(cmd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PreCardPayment(CardPaymentDetails details)
        {
            details.CardHolderName = details.CardHolderName.TrimEnd().TrimStart();

            PaymentInfo.firstName = details.CardHolderName.Substring(0, details.CardHolderName.IndexOf(" "));
            PaymentInfo.surName = details.CardHolderName.Substring(details.CardHolderName.IndexOf(" ") + 1);
            PaymentInfo.cardNumber = details.CardNumber;
            PaymentInfo.expireMonth = details.ExpireMonth;
            PaymentInfo.expireYear = details.ExpireYear;
            PaymentInfo.CVV2 = details.CVV2;

            var pesho = new List<ResultMessage>();
            pesho.Add(new ResultMessage { Message = "success" });
            return Json(pesho, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FailureView()
        {
            return View();
        }
    }
}