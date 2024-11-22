using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class RiseSetModel : AuditInfoModel
    {
        public long? RiseSetId { set; get; }
        public string LocationNameDesc { set; get; }
        public long DemogInfoCityId { set; get; }
        public string GregorianDate { set; get; }
        public string SunRise { set; get; }
        public string SunSet { set; get; }
        public string RKStart { set; get; }
        public string RKFinish { set; get; }
        public string YGStart { set; get; }
        public string YGFinish { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
