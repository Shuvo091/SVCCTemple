using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorListModel
    {
        public string SponsorshipGroupListId { set; get; }
        public List<SponsorshipGroupModel> SponsorshipGroupModels { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
