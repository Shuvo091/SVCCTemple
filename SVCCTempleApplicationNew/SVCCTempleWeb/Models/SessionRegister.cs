using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SessionRegister
    {
        public long SessionId { set; get; }
        public string SessionDesc { set; get; }
        public long MaxCount { set; get; }
        public long RegisteredCount { set; get; }
    }
}
