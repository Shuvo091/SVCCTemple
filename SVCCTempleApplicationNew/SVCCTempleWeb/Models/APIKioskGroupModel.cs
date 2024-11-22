using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APIKioskGroupModel
    {
        public long? KioskGroupId { set; get; }
        public string LocationNameDesc { set; get; }
        public string KioskGroupDesc { set; get; }
        public string KioskGroupType { set; get; }
        public string ItemImageName { set; get; }
        public string ItemImageHeight { set; get; }
        public string ItemImageUrl { set; get; }
        public string ItemImageWidth { set; get; }
        public float? SeqNum { set; get; }
        public long? SponsorshipGroupId { set; get; }
        public List<APIKioskItemModel> APIKioskItemModels { set; get; }
    }
}
