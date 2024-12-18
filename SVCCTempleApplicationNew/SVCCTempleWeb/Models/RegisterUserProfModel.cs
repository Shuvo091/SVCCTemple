﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class RegisterUserProfModel
    {
        [Required(ErrorMessage = "Please enter register catcha answer")]
        [Display(Name = "Register captcha answer")]
        public string CaptchaAnswerRegister { set; get; }

        public string CaptchaNumberRegister0 { set; get; }

        public string CaptchaNumberRegister1 { set; get; }

        [Compare(nameof(RegisterEmailAddress), ErrorMessage = "Register email address & confirm do not match")]
        [Display(Name = "Confirm register email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid confirm register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid confirm email addres")]
        [Required(ErrorMessage = "Please enter confirm email address")]
        public string ConfirmRegisterEmailAddress { get; set; }

        [Display(Name = "Register email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid register email addres")]
        [Required(ErrorMessage = "Please enter register email address")]
        public string RegisterEmailAddress { get; set; }

        //[Display(Name = "Telephone#")]
        ////[RegularExpression(@"(.{10})", ErrorMessage = "Enter 10 digit telephone number")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid 10 digit phone#")]
        //[Required(ErrorMessage = "Please enter phone#")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit phone#")]
        //public string TelephoneNumber { set; get; }

        public ResponseObjectModel0 ResponseObjectModel0 { set; get; }

        //[Required(ErrorMessage = "Please enter email address for {0}")]
        //[Display(Name = "Email Address")]
        //[EmailAddress(ErrorMessage = "Please enter valid email address for {0}")]
        //public string RegisterEmailAddress { get; set; }

        //[Required(ErrorMessage = "Please enter email address for {0}")]
        //[Display(Name = "Confirm Email Address")]
        //[EmailAddress(ErrorMessage = "Please enter valid email address for {0}")]
        //[Compare(nameof(RegisterEmailAddress), ErrorMessage = "Email Address and Confirm Email Address do not match")]
        //public string ConfirmRegisterEmailAddress { get; set; }

        public string CaptchaQuestion { set; get; }

        [Display(Name = "Answer to captcha"), Required(ErrorMessage = "Please enter your answer")]
        public string AnswerToCaptcha { set; get; }

        public string Simulate { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
