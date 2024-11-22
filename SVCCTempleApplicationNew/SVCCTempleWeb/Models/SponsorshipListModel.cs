using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorshipListModel
    {
        public long SponsorshipListId { set; get; }
        public string SponsorshipListDesc { set; get; }
        public int SponsorshipListMinQty { set; get; }
        public float SponsorshipListRate { set; get; }
        public long SponsorshipGroupId { set; get; }
        public int? SponsorshipListOrderQty { set; get; }
        public float? SponsorshipListOrderAmount { set; get; }
    }
}
