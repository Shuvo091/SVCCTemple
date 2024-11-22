using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorModel
    {
        public string OtherSponsorshipDescription { set; get; }
        public float? OtherSponsorshipAmount { set; get; }
        public long? SponsorshipGroupListId { set; get; }
        public List<SponsorshipGroupModel> SponsorshipGroupModels { set; get; }
    }
}
