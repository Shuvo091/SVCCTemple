using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class BookingModel : AuditInfoModel
    {
        public long? BookingId { set; get; }

        public string LocationNameDesc { set; get; }

        [Compare("EmailAddress", ErrorMessage = "Email address & confirm do not match")]
        [Display(Name = "Confirm email address")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please enter valid confirm email addres")]
        [Required(ErrorMessage = "Please enter confirm email address")]
        public string ConfirmEmailAddress { get; set; }

        [Display(Name = "Email address")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please enter valid email addres")]
        [Required(ErrorMessage = "Please enter email address")]
        public string EmailAddress { set; get; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter max 100 characters first name")]
        public string FirstName { set; get; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter max 100 characters last name")]
        public string LastName { set; get; }

        [Display(Name = "Telephone#")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter valid 10 digit phone#")]
        [Required(ErrorMessage = "Please enter phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter valid 10 digit phone#")]
        public string TelephoneNumber { set; get; }

        [Display(Name = "Service")]
        public long? ServiceId { set; get; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please enter start date")]
        public string StartDate { set; get; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please enter start time")]
        public string StartTime { set; get; }

        public string FinishDate { set; get; }

        public string FinishTime { set; get; }

        [Display(Name = "Patron Comments")]
        public string PatronComments { set; get; }

        [Display(Name = "Priest Comments")]
        public string PriestComments { set; get; }

        public string StatusNameDesc { set; get; }

        [Display(Name = "Select Service")]
        public List<TempleServiceModel> TempleServiceModels { set; get; }

        public List<BookingAssignModel> BookingAssignModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
