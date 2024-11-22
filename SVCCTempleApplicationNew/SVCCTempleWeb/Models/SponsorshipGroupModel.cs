using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorshipGroupModel
    {
        public long SponsorshipGroupId { set; get; }
        public string SponsorshipGroupDesc { set; get; }
        public float DisplaySeqNum { set; get; }
        public string BegEffDate { set; get; }
        public string EndEffDate { set; get; }
        public List<SponsorshipListModel> SponsorshipListModels { set; get; }
    }
}
