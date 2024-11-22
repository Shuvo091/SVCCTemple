using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class PaymentModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter email address")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string EmailAddress { set; get; }
        public string PrimaryTelephoneNum { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        [Required(ErrorMessage = "Please sponsor to continue")]
        public float? SponsorshipAmount { set; get; }
        [Display(Name = "Name on Credit Card")]
        [Required(ErrorMessage = "Please enter name as on the card")]
        public string NameOnCreditCard { set; get; }
        [Display(Name = "Credit Card#")]
        [Required(ErrorMessage = "Please enter valid credit card number")]
        public string CreditCardNumber { set; get; }
        [Required(ErrorMessage = "Select Year")]
        public string CreditCardExpiryYYYY { set; get; }
        [Required(ErrorMessage = "Select MM")]
        public string CreditCardExpiryMM { set; get; }
        [Display(Name = "Credit Card Sec Code")]
        [Required(ErrorMessage = "Please enter CVV")]
        public string CreditCardSecCode { set; get; }
        public string ReferenceNumber { set; get; }
        public string Comments { set; get; }
        public string UniqeRefId { set; get; }
        public string CreditCardProcessMessage { set; get; }
        public bool CreditCardProcessStatus { set; get; }
        public string RegiterEmailAddress { set; get; }
    }
}
