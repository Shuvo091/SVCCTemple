using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APIDailyInfoDataModel
    {
        public string GregorianDate { set; get; }
        public string EventDesc1 { set; get; }
        public string EventDetail1 { set; get; }
        public long? SponsorshipGroupId { set; get; }
        public long? KioskGroupId { set; get; }
    }
}
