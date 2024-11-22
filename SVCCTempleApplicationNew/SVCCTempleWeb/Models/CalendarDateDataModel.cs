using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class CalendarDateDataModel : AuditInfoModel
    {
        public long? CalendarId { set; get; }
        public string LocationNameDesc { set; get; }
        public long DemogInfoCityId { set; get; }
        public long CalendarCodeId { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public string FinishDate { set; get; }
        public string FinishTime { set; get; }
        public CodeDataModel CodeDataModel { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
