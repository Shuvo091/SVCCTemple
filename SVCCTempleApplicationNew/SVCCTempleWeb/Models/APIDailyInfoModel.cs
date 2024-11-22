using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleWeb.Models
{
    public class APIDailyInfoModel
    {
        public string BannerImageUrl { set; get; }
        public List<string> DeityImagesUrl { set; get; }
        public List<GeneralInfo> GeneralInfos { set; get; }
        public List<APIDailyInfoDataModel> APIDailyInfoDataModels { set; get; }
    }
}
