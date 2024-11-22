using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class CodeTypeModel
    {
        public long? CodeTypeId { set; get; }
        public long? CodeTypeNameId { set; get; }
        public string CodeTypeNameDesc { set; get; }
        public string CodeTypeDesc { set; get; }
        public string CodeTypeDesc1 { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
