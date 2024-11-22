using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class TempleScheduleMasterModel : AuditInfoModel
    {
        public long? TempleScheduleMasterId { set; get; }
        public string LocationNameDesc { set; get; }
        public long ScheduleMasterId { set; get; }
        public float ScheduleMasterSeqNum { set; get; }
        public long? CalendarCodeId { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public float SeqNum { set; get; }
        public string FrequencyTypeNameDesc { set; get; }
        public string FrequencyCategoryNameDesc { set; get; }
        public string EventDesc0 { set; get; }
        public string EventDesc1 { set; get; }
        public string EventDesc2 { set; get; }
        public string EventDesc3 { set; get; }
        public string EventDesc4 { set; get; }
        public string EventDesc5 { set; get; }
        public string EventDesc6 { set; get; }
        public string EventDesc7 { set; get; }
        public string EventDesc8 { set; get; }
        public string EventDesc9 { set; get; }
        public bool? InfoOnly { set; get; }
        public bool? EventAtTemple { set; get; }
        public bool? MoreInfo { set; get; }
        public long? SponsorshipGroupId { set; get; }
        public long? KioskGroupId { set; get; }
        public string FinishDate { set; get; }
        public string FinishTime { set; get; }
        public string ImageFileName1 { set; get; }
        public string ImageFileName2 { set; get; }
        public string ImageFileName3 { set; get; }
        public string FileName1 { set; get; }
        public string FileName2 { set; get; }
        public string FileName3 { set; get; }
        public string HtmlFileName1 { set; get; }
        public string HtmlFileName2 { set; get; }
        public string HtmlFileName3 { set; get; }
        public string CategoryNameDesc { set; get; }
        public string SampleDateStart { set; get; }
        public string SampleDateFinish { set; get; }
        public string StatusNameDesc { set; get; }
        public CodeDataModel CodeDataModel { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
