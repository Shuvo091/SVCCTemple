using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APISankalpamInfoDataModel
    {
        public DateTime SankalpamDate { set; get; }
        public List<CalendarData> CalendarDatas { set; get; }
    }
}
