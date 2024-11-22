using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class TempleEventImportantDatesModel
    {
        public string TempleEventMasterId { set; get; }
        public string EventName1 { set; get; }
        public string EventName2 { set; get; }
        public string EventName3 { set; get; }
        public string EventDesc1 { set; get; }
        public string EventDesc2 { set; get; }
        public string EventDesc3 { set; get; }
        public string EventText1 { set; get; }
        public string EventText2 { set; get; }
        public string EventText3 { set; get; }
        public string FileName1 { set; get; }
        public string FileName2 { set; get; }
        public string FileName3 { set; get; }
        public string ImageFileName1 { set; get; }
        public string ImageFileName2 { set; get; }
        public string ImageFileName3 { set; get; }
        public string HtmlFileName1 { set; get; }
        public string HtmlFileName2 { set; get; }
        public string HtmlFileName3 { set; get; }
        public string SponsorshipGroupId { set; get; }
        public string KioskGroupId { set; get; }
        public string SeqNum { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public List<string> BeginDates  { set; get; }
        public List<string> EndDates  { set; get; }
        public List<string> StartDates { set; get; }
        public List<string> StartTimes { set; get; }
        public List<string> EventName1s { set; get; }
        public List<string> EventName2s { set; get; }
        public List<string> EventName3s { set; get; }
    }
}
