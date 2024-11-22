using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class CodeDataModel : AuditInfoModel
    {
        public long? CodeDataId { set; get; }
        public long CodeTypeId { set; get; }
        public long CodeDataNameId { set; get; }
        public string CodeDataNameDesc { set; get; }
        public string CodeDataDesc0 { set; get; }
        public string CodeDataDesc1 { set; get; }
        public string CodeDataDesc2 { set; get; }
        public string CodeDataDesc3 { set; get; }
        public string CodeDataDesc4 { set; get; }
        public string CodeDataDesc5 { set; get; }
        public string CodeDataDesc6 { set; get; }
        public string CodeDataDesc7 { set; get; }
        public string CodeDataDesc8 { set; get; }
        public string CodeDataDesc9 { set; get; }
        public CodeTypeModel CodeTypeModel { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
