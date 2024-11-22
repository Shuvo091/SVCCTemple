using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class RegisterUserLoginUserModel
    {
        public LoginUserProfModel LoginUserProfModel { set; get; }
        public RegisterUserProfModel RegisterUserProfModel { set; get; }
        public ResetPasswordModel ResetPasswordModel { set; get; }
    }
}
