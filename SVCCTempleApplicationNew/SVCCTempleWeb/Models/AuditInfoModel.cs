using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class AuditInfoModel
    {
        public virtual string AddDateTime { get; set; }

        public virtual string AddUserId { get; set; }

        public virtual string AddUserName { get; set; }

        public virtual string UpdDateTime { get; set; }

        public virtual string UpdUserId { get; set; }

        public virtual string UpdUserName { get; set; }
    }
}
