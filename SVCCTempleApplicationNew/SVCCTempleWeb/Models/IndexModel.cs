using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class IndexModel
    {
        public int TabIndex { set; get; }
        public DateTime StartDate { set; get; }
        public List<DailyInfoModel> DailyInfoModels { set; get; }
        public List<TempleEventModel> Festivals { set; get; }
        public List<TempleEventModel> ImportantDates { set; get; }
        public SankalpamInfoModel SankalpamInfoModel { set; get; }
        public List<CalendarData> SankalpamInfos { set; get; }
        public Dictionary<string, List<ImportantDatesModelNew>> MonthImportantDatesModels { set; get; }
        public List<TempleEventImportantDatesModel> TempleEventImportantDatesModels { set; get; }
        public List<UpcomingFestivalsModel> UpcomingFestivalsModels { set; get; }
        public TempleInfoDatesModel TempleInfoDatesModel { set; get; }
        public List<ImportantDatesModel> ImportantDatesModels { set; get; }
    }
}
