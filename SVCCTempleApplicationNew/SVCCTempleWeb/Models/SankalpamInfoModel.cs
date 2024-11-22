using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class SankalpamInfoModel
    {
        public DateTime StartDate { set; get; }
        public DateTime FinishDate { set; get; }
        public List<SankalpamInfoDetailModel> SankalpamInfoDetailModels { set; get; }
    }
}
