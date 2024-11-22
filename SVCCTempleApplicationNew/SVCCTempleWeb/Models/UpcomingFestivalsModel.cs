using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class UpcomingFestivalsModel
    {
        public long UpcomingFestivalsId { set; get; }
        public string LocationNameDesc { set; get; }
        public string UpcomingFestivalsDesc { set; get; }
        public float SeqNum { set; get; }
        public string LinkTitle { set; get; }
        public string ContentDivId { set; get; }
        public string StartDate { set; get; }
        public string FinishDate { set; get; }
        public string NavigationHtmlFileName1 { set; get; }
        public string NavigationHtmlFileName2 { set; get; }
        public string NavigationHtmlFileName3 { set; get; }
        public string ContentHtmlFileName1 { set; get; }
        public string ContentHtmlFileName2 { set; get; }
        public string ContentHtmlFileName3 { set; get; }
        public long? SponsorshipGroupId { set; get; }
        public long? KioskOrderGroupId { set; get; }
        public string StatusNameDesc { set; get; }
        public string NavigationHtml { set; get; }
        public string ContentHtml { set; get; }
    }
}
