using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SessionObjectModel
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string LoginUserId { set; get; }
        public string LoginNameId1 { set; get; }
        public string UserTypeNameId { set; get; }
        public long UserStatusId { set; get; }
        public long PersonId { set; get; }
    }
}