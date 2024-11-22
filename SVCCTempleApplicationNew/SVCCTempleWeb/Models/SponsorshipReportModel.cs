using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorshipReportModel
    {
        public string EmailAddress { set; get; }
        public string StartDate { set; get; }
        public string FinishDate { set; get; }
        public List<SponsorshipReportDataModel> SponsorshipReportDataModels { set; get; }
    }
}
