using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class KioskCheckoutModel
    {
        public string GeneralDonationAmount { set; get; }
        public string GeneralDonationDescription { set; get; }
        public List<KioskItemModel> KioskItemModels { set; get; }
        public string KioskOrderItemIds { set; get; }
        public string KioskOrderedOrderQtys { set; get; }
        public float OrderItemAmount { set; get; }
        public int OrderItemCount { set; get; }
        public PaymentModel PaymentModel { set; get; }
        public long SponsorshipId { set; get; }
        public string SponsorshipReceipt { set; get; }
    }
}
