using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APICheckoutModel
    {
        public APIPaymentModel APIPaymentModel { set; get; }
        public List<APIOrderItemModel> APIOrderItemModels { set; get; }
    }
}
