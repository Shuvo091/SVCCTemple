using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class ImportantDatesDateModel
    {
        public long ImportantDatesId { set; get; }
        public string EventDate { set; get; }
        public string EventTime { set; get; }
        public string EventText1 { set; get; }
        public string EventText2 { set; get; }
        public string EventText3 { set; get; }
        public long ImportantDatesDateId { set; get; }
    }
}
