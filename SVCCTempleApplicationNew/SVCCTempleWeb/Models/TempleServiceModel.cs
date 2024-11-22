using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class TempleServiceModel : AuditInfoModel
    {
        public long? TempleServiceId { set; get; }
        public string LocationNameDesc { set; get; }
        public string TempleOrHome { set; get; }
        public float? SeqNum { set; get; }
        public string TempleServiceDesc { set; get; }
        public short? Donation { set; get; }
        public short? DonationPriest { set; get; }
        public short? TimeForService { set; get; }
        public string StatusNameDesc { set; get; }
    }
}
