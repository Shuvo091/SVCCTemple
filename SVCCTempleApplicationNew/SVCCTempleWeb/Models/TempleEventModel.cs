using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class TempleEventModel
    {
        public byte QueryNum { set; get; }
        public long TempleEventId { set; get; }
        public string LocationNameDesc { set; get; }
        public long TempleEventMasterId { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public float SeqNum { set; get; }
        public string EventName1 { set; get; }
        public string EventName2 { set; get; }
        public string EventName3 { set; get; }
        public string EventDesc1 { set; get; }
        public string EventDesc2 { set; get; }
        public string EventDesc3 { set; get; }
        public string EventText1 { set; get; }
        public string EventText2 { set; get; }
        public string EventText3 { set; get; }
        public long SponsorshipGroupId { set; get; }
        public long KioskGroupId { set; get; }
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
        public string StatusNameDesc { set; get; }
        public string AddUserId { set; get; }
        public string AddUserName { set; get; }
        public string AddDateTime { set; get; }
        public string UpdUserId { set; get; }
        public string UpdUserName { set; get; }
        public string UpdDateTime { set; get; }
        public TempleEventMasterModel TempleEventMasterModel { set; get; }
        public string BeginDate { set; get; }
    }
}
