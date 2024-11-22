using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class BookingAssignModel : AuditInfoModel
    {
        public long? BookingAssignId { set; get; }

        public string LocationNameDesc { set; get; }

        public long BookingId { set; get; }

        public float SeqNUm { set; get; }

        public long PriestId { set; get; }

        public string StatusNameDesc { set; get; }
    }
}
