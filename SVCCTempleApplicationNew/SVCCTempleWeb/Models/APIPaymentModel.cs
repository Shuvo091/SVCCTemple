using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APIPaymentModel
    {
        public string CreditCardExpiryMM { set; get; }
        public string CreditCardExpiryYYYY { set; get; }
        public string CreditCardNumber { set; get; }
        public string CreditCardSecCode { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string NameOnCreditCard { set; get; }
        public string PrimaryTelephoneNum { set; get; }
        public float SponsorshipAmount { set; get; }
    }
}
