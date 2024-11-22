using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class ImportantDatesModelNew
    {
        public long ImportantDatesId { set; get; }
        public string LocationNameDesc { set; get; }
        public float SeqNum  { set; get; }
        public string EventName1 { set; get; }
        public string EventName2 { set; get; }
        public string EventDesc1 { set; get; }
        public string EventDesc2 { set; get; }
        public string FileName1 { set; get; }
        public string Url1 { set; get; }
        public string ImageName1 { set; get; }
        public string ImageName2 { set; get; }
        public string StartTime { set; get; }
        public string FinishTime { set; get; }
        public string EventName3 { set; get; }
        public string EventName4 { set; get; }
        public string EventDesc3 { set; get; }
        public string EventDesc4 { set; get; }
        public string ImageName3 { set; get; }
        public string ImageName4 { set; get; }
        public string HtmlFileName3 { set; get; }
        public string HtmlFileName4 { set; get; }
        public string EventTypeNameDesc { set; get; }
        public long SponsorshipGroupId { set; get; }
        public List<ImportantDatesDateModel> ImportantDatesDateModels { set; get; }
    }
}
