using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorshipModel
    {
        public long SponsorshipId { get; set; }
        public string DownloadFileName { set; get; }
        public SponsorModel SponsorModel { set; get; }
        public PaymentModel PaymentModel { set; get; }
        public string SponsorshipReceipt { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
