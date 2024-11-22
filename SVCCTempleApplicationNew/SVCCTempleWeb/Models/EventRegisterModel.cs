using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class EventRegisterModel
    {
        public long? EventRegisterId { set; get; }

        [Display(Name = "Comments")]
        [StringLength(1024, MinimumLength = 0, ErrorMessage = "Please enter max 1024 characters first name")]
        public string Comments { set; get; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email addres")]
        [Required(ErrorMessage = "Please enter email address")]
        public string EmailAddress { set; get; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter max 50 characters first name")]
        public string FirstName { set; get; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter max 50 characters last name")]
        public string LastName { set; get; }

        public string SessionDesc { set; get; }

        [Display(Name = "Session")]
        [Required(ErrorMessage = "Please select session")]
        public long? SessionId { set; get; }

        [Display(Name = "Telephone#")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid 10 digit telephone#")]
        [Required(ErrorMessage = "Please enter telephone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit telephone#")]
        public string TelephoneNumber { set; get; }

        public string KioskOrderedItemIds { set; get; }

        public string KioskOrderedOrderQtys { set; get; }

        public List<SessionRegister> SessionRegisters { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
