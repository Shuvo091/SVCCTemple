using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APICalendarInfoModel
    {
        public string CalendarYYYYMM { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime FinishDate { set; get; }
        public List<APICalendarData> APICalendarDatas { set; get; }
    }
}
