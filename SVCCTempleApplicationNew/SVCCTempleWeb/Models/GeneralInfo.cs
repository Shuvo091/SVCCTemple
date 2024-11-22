using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class GeneralInfo
    {
        public string InfoType { set; get; }
        public string InfoDesc { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public string FinishDate { set; get; }
        public string FinishTime { set; get; }
    }
}
