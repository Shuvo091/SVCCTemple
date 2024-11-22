using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APIOrderItemModel
    {
        public long KioskItemId { set; get; }
        public long OrderQty { set; get; }
        public float OrderAmount { set; get; }
    }
}
