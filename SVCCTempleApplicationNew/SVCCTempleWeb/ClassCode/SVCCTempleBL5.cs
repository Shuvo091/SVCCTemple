using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SVCCTempleWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SVCCTempleWeb.ClassCode
{
    public partial class SVCCTempleBL
    {
        //APICalendarInfoGet GET
        public APICalendarInfoModel APICalendarInfoGet(string locationNameDesc, string locationNameDesc1, string yearMonth, string importantDatesIds1, string execUniqueId)
        {
            string importantDatesIds = "1, 2, 3, 4, 5, 6, 9, 41, 42, 43, 44, 45, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65";
            CalendarModel calendarModel = GenerateCalendarData(locationNameDesc, yearMonth, importantDatesIds, execUniqueId);
            APICalendarInfoModel aPICalendarInfoModel = new APICalendarInfoModel
            {
                APICalendarDatas = new List<APICalendarData>(),
                CalendarYYYYMM = yearMonth,
                FinishDate = calendarModel.CalendarFinishDate,
                StartDate = calendarModel.CalendarStartDate,
            };
            int seqNum = 0;
            foreach (var x in calendarModel.CalendarDateListList)
            {
                foreach (var y in x.Value)
                {
                    aPICalendarInfoModel.APICalendarDatas.Add
                    (
                        new APICalendarData
                        {
                            CalendarDate = x.Key,
                            SeqNum = ++seqNum,
                            StartDate = y.StartDate,
                            StartTime = y.StartTime,
                            FinishDate = y.FinishDate,
                            FinishTime = y.FinishTime,
                            CalendarCodeId = y.CalendarCodeId,
                            CodeDataDesc0 = y.CodeDataDesc0,
                            CodeDataDesc9 = y.CodeDataDesc9,
                            CodeDataNameDesc = y.CodeDataNameDesc,
                            CodeDataNameId = y.CodeDataNameId,
                            CodeTypeDesc = y.CodeTypeDesc,
                            CodeTypeDesc1 = y.CodeTypeDesc1,
                            CodeTypeNameDesc = y.CodeTypeNameDesc,
                            CodeTypeNameId = y.CodeTypeNameId,
                            Color = y.Color,
                            LocationNameDesc = locationNameDesc,
                        }
                    );
                }
            }
            return aPICalendarInfoModel;
        }
        //APIDailyInfoGet GET
        public APIDailyInfoModel APIDailyInfoGet(string locationNameDesc, string locationNameDesc1, string imagesBaseUrl, string gregorianDate, string execUniqueId)
        {
            string sqlStmt = "";
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.SunRise" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.SunSet" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RKStart" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RKFinish" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.YGStart" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.YGFinish" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "               RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND RiseSet.GregorianDate = '" + gregorianDate + "'" + Environment.NewLine;
            var dailyInfos = GenerateDailyInfos(locationNameDesc, DateTime.Parse(gregorianDate), execUniqueId);
            APIDailyInfoModel aPIDailyInfoModel = new APIDailyInfoModel
            {
                BannerImageUrl = imagesBaseUrl + locationNameDesc1 + "/SmartPhone/SVCCBannerFremont" + locationNameDesc1 + ".jpg",
                DeityImagesUrl = new List<string>
                {
                    imagesBaseUrl + locationNameDesc1 + "/SmartPhone/Ganesha_01.jpg",
                    imagesBaseUrl + locationNameDesc1 + "/SmartPhone/Balaji_01.png",
                    imagesBaseUrl + locationNameDesc1 + "/SmartPhone/Durga_01.png",
                    imagesBaseUrl + locationNameDesc1 + "/SmartPhone/Shiva_01.png",
                },
                APIDailyInfoDataModels = new List<APIDailyInfoDataModel>(),
            };
            APIDailyInfoDataModel aPIDailyInfoDataModel;
            foreach (var dailyInfo in dailyInfos)
            {
                aPIDailyInfoModel.APIDailyInfoDataModels.Add
                (
                    aPIDailyInfoDataModel = new APIDailyInfoDataModel
                    {
                        GregorianDate = gregorianDate,
                        EventDesc1 = dailyInfo.EventName1,
                        SponsorshipGroupId = dailyInfo.TempleEventMasterModel.SponsorshipGroupId,
                        KioskGroupId = dailyInfo.TempleEventMasterModel.KioskGroupId,
                    }
                );
                if (dailyInfo.TempleEventMasterModel.TempleEventMasterId >= 20)
                {
                    //else
                    {
                        if (dailyInfo.EventDesc1 != "")
                        {
                            aPIDailyInfoDataModel.EventDesc1 += Environment.NewLine + dailyInfo.EventDesc1;
                        }
                    }
                    aPIDailyInfoDataModel.EventDetail1 = DateTime.Parse(dailyInfo.StartTime).ToString("h:mm tt");
                }
                else
                {
                    aPIDailyInfoDataModel.EventDetail1 = dailyInfo.EventText1;
                }
            }
            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                aPIDailyInfoModel.GeneralInfos = new List<GeneralInfo>
                {
                    new GeneralInfo
                    {
                        InfoType = "Daily Info",
                        InfoDesc = "Sun Rise / Set",
                        StartDate = sqlDataReader["GregorianDate"].ToString(),
                        StartTime = sqlDataReader["SunRise"].ToString(),
                        FinishDate = sqlDataReader["GregorianDate"].ToString(),
                        FinishTime = sqlDataReader["SunSet"].ToString(),
                    },
                    new GeneralInfo
                    {
                        InfoType = "Daily Info",
                        InfoDesc = "Rahu Kalam",
                        StartDate = sqlDataReader["GregorianDate"].ToString(),
                        StartTime = sqlDataReader["RKStart"].ToString(),
                        FinishDate = sqlDataReader["GregorianDate"].ToString(),
                        FinishTime = sqlDataReader["RKFinish"].ToString(),
                    },
                    new GeneralInfo
                    {
                        InfoType = "Daily Info",
                        InfoDesc = "Yama Gandam",
                        StartDate = sqlDataReader["GregorianDate"].ToString(),
                        StartTime = sqlDataReader["YGStart"].ToString(),
                        FinishDate = sqlDataReader["GregorianDate"].ToString(),
                        FinishTime = sqlDataReader["YGFinish"].ToString(),
                    },
                };
            }
            return aPIDailyInfoModel;
        }
        //APIKioskOrder GET
        public APIKioskOrderModel APIKioskOrderGet(string locationNameDesc, string locationNameDesc1, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            int rowNum = 0;
            try
            {
                List<APIKioskGroupModel> aPIKioskGroupModels = new List<APIKioskGroupModel>();
                APIKioskOrderModel aPIKioskOrderModel = new APIKioskOrderModel
                {
                    APIkioskGroupModels = aPIKioskGroupModels,
                };
                APIKioskGroupModel aPIKioskGroupModel;
                sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
                sqlConnection.Open();
                SqlCommand sqlCommand = BuildSqlCommandKioskOrderSelect(sqlConnection);
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@EffDate"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    aPIKioskGroupModels.Add
                    (
                        aPIKioskGroupModel = new APIKioskGroupModel
                        {
                            KioskGroupId = long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            KioskGroupDesc = sqlDataReader["KioskGroupDesc"].ToString(),
                            KioskGroupType = sqlDataReader["KioskGroupType"].ToString(),
                            ItemImageName = locationNameDesc1 + "/SmartPhone/Kiosk/" + sqlDataReader["KioskGroup_ItemImageName"].ToString(),
                            ItemImageHeight = sqlDataReader["KioskGroup_ItemImageHeight"].ToString(),
                            ItemImageWidth = sqlDataReader["KioskGroup_ItemImageWidth"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            APIKioskItemModels = new List<APIKioskItemModel>(),
                        }
                    );
                    while (sqlDataReaderRead && aPIKioskGroupModel.KioskGroupId == long.Parse(sqlDataReader["KioskGroupId"].ToString()))
                    {
                        rowNum++;
                        aPIKioskGroupModel.APIKioskItemModels.Add
                        (
                            new APIKioskItemModel
                            {
                                KioskItemId = long.Parse(sqlDataReader["KioskItemId"].ToString()),
                                LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                                KioskGroupId = long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                                ItemDesc = sqlDataReader["ItemDesc"].ToString(),
                                //ItemDesc1 = sqlDataReader["ItemDesc1"].ToString(),
                                //ItemDesc2 = sqlDataReader["ItemDesc2"].ToString(),
                                ItemImageName = locationNameDesc1 + "/SmartPhone/Kiosk/" + sqlDataReader["ItemImageName"].ToString(),
                                ItemImageHeight = sqlDataReader["ItemImageHeight"].ToString(),
                                ItemImageWidth = sqlDataReader["ItemImageWidth"].ToString(),
                                ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                                SponsorshipListId = sqlDataReader["SponsorshipListId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SponsorshipListId"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return aPIKioskOrderModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlDataReader.Close();
                }
                catch
                {
                    ;
                }
                try
                {
                    sqlConnection.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        //APISankalpamInfoGet GET
        public APISankalpamInfoModel APISankalpamInfoGet(string locationNameDesc, string locationNameDesc1, DateTime sankalpamDate, string imagesBaseUrl, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                APISankalpamInfoModel apiSankalpamInfoModel = new APISankalpamInfoModel
                {
                    SankalpamDate = sankalpamDate,
                    CalendarDatas = GenerateCalendarData(locationNameDesc, sankalpamDate, sankalpamDate, execUniqueId),
                };
                return apiSankalpamInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
            finally
            {
            }
        }
    }
}
