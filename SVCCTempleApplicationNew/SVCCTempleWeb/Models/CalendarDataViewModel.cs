using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class CalendarDataViewModel
    {
        public long CalendarYear { set; get; }
        public string FinishDate { set; get; }
        public List<CalendarDateDataModel> NakshatraDataModels { set; get; }
        public List<CalendarDateDataModel> RasiDataModels { set; get; }
        public List<RiseSetModel> RiseSetModels { set; get; }
        public string StartDate { set; get; }
        public TempleScheduleMasterList TempleScheduleMasterList { set; get; }
        public TempleScheduleUploadList TempleScheduleUploadList { set; get; }
        public TempleScheduleUploadList TempleFestivalUploadList { set; get; }
        public List<CalendarDateDataModel> ThithiDataModels { set; get; }
        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
