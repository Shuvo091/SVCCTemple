using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class KioskItemModel
    {
        public long? KioskItemId { set; get; }
        public string LocationNameDesc { set; get; }
        public long? KioskGroupId { set; get; }
        public string ItemDesc { set; get; }
        public string ItemImageName { set; get; }
        public string ItemImageHeight { set; get; }
        public string ItemImageWidth { set; get; }
        public float? ItemRate { set; get; }
        public float? SeqNum { set; get; }
        public long? SponsorshipListId { set; get; }
        public long? OrderQty { set; get; }
        public float? OrderAmount { set; get; }
        public KioskGroupModel KioskGroupModel { set; get; }
    }
}
