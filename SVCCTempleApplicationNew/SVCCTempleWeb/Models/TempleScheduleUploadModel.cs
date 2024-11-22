using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class TempleScheduleUploadModel : AuditInfoModel
    {
        public long? TempleScheduleUploadId { set; get; }
        public string LocationNameDesc { set; get; }
        public long ScheduleMasterId { set; get; }
        public string StartDate { set; get; }
        public string FinishDate { set; get; }
        public string EventText1 { set; get; }
        public string EventText2 { set; get; }
        public long? CalendarId { set; get; }
        public CalendarDateDataModel CalendarDateDataModel { set; get; }
        public TempleScheduleMasterModel TempleScheduleMasterModel { set; get; }
    }
}
