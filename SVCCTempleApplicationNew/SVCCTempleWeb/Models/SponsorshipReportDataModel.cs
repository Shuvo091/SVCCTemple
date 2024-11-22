using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SponsorshipReportDataModel
    {
        public string AddDateTime { set; get; }
        public long SponsorshipId { set; get; }
        public string SankalpamDateTime { set; get; }
        public string LocationNameDesc { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string PrimaryTelephoneNum { set; get; }
        public float ShoppingCartAmount { set; get; }
        public string ShoppingCartComments { set; get; }
        public string StatusNameDesc { set; get; }
        public string RequestData { set; get; }
        public string ResponseData { set; get; }
        public string SponsorshipGroupDesc { set; get; }
    }
}
