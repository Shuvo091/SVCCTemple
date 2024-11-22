using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SVCCTempleWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SVCCTempleWeb.ClassCode
{
    public partial class SVCCTempleBL
    {
        public IndexModel BuildIndexModel(string locationNameDesc, string locationNameDesc1, int tabIndex, string serverDirectoryName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            DateTime todaysDate = DateTime.Now;
            DateTime finishDate = DateTime.Parse(todaysDate.AddMonths(2).ToString("yyyy-MM-01")).AddDays(-1);
            finishDate = DateTime.Parse(DateTime.Now.AddMonths(13).ToString("yyyy-MM-01")).AddDays(-1);
            //string importantIdsList = "1, 2, 3, 4, 5, 6, 7, 10, 11";

            IndexModel indexModel = new IndexModel
            {
                StartDate = todaysDate,
                TabIndex = tabIndex,
            };
            switch (tabIndex)
            {
                case 0: //Upcoming Festivals
                    indexModel.UpcomingFestivalsModels = GenerateUpcomingFestivalsModels(locationNameDesc, locationNameDesc1, serverDirectoryName, todaysDate, execUniqueId);
                    indexModel.TempleEventImportantDatesModels = GenerateImportantDates(locationNameDesc, DateTime.Now, "51, 52, 53, 54, 55, 56, 60, 63, 64, 65, 66", execUniqueId);
                    break;
                case 1: //Sankalpam
                    indexModel.SankalpamInfoModel = new SankalpamInfoModel
                    {
                        StartDate = todaysDate,
                        FinishDate = todaysDate.AddDays(17),
                        SankalpamInfoDetailModels = new List<SankalpamInfoDetailModel>(),
                    };
                    for (int i = 0; i < 18; i++)
                    {
                        SankalpamInfoDetailModel sankalpamInfoDetailModel = new SankalpamInfoDetailModel
                        {
                            SankalpamDate = todaysDate.AddDays(i),
                        };
                        sankalpamInfoDetailModel.CalendarDatas = GenerateCalendarData(locationNameDesc, sankalpamInfoDetailModel.SankalpamDate, sankalpamInfoDetailModel.SankalpamDate, execUniqueId);
                        indexModel.SankalpamInfoModel.SankalpamInfoDetailModels.Add(sankalpamInfoDetailModel);
                    }
                    break;
                case 2: //Daily Info
                    indexModel.DailyInfoModels = new List<DailyInfoModel>();
                    DailyInfoModel dailyInfoModel;
                    DateTime dailyInfoDate;
                    for (int i = 0; i < 18; i++)
                    {
                        dailyInfoDate = todaysDate.AddDays(i);
                        dailyInfoModel = new DailyInfoModel
                        {
                            DailyInfoDate = dailyInfoDate,
                            TempleEventModels = GenerateDailyInfos(locationNameDesc, dailyInfoDate, execUniqueId),
                        };
                        indexModel.DailyInfoModels.Add(dailyInfoModel);
                    }
                    break;
                case 3: //Daily Weekly
                    break;
                case 4: //Important Dates
                    //indexModel.ImportantDates = GenerateImportantDates(locationNameDesc, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")), finishDate, execUniqueId);
                    //indexModel.MonthImportantDatesModels = GetImportantDates2A(locationNameDesc, 6, importantIdsList, "41, 51, 52, 54, 55, 56, 57, 61, 62, 65", execUniqueId);//5001, 5002, 5005, 5006, 5007, 5008, 5009
                    indexModel.TempleEventImportantDatesModels = GenerateImportantDates(locationNameDesc, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")), finishDate, "50, 51, 52, 53, 54, 55, 56, 60, 61, 63, 64, 65, 66", execUniqueId);
                    break;
                case 5: //Festivals
                    indexModel.Festivals = GenerateFestivals(locationNameDesc, todaysDate, execUniqueId);
                    break;
                default:
                    break;
            }
            return indexModel;
        }
        public IndexModel BuildIndexModel(string locationNameDesc, string locationNameDesc1, string serverDirectoryName, int tabIndex, string execUniqueId)
        {
            DateTime todaysDate = DateTime.Now;
            DateTime finishDate = DateTime.Parse(todaysDate.AddMonths(2).ToString("yyyy-MM-01")).AddDays(-1);
            finishDate = DateTime.Parse(DateTime.Now.AddMonths(12).ToString("yyyy-MM-01")).AddDays(-1);
            //string importantIdsList = "1, 2, 3, 4, 5, 6, 7, 10, 11";

            IndexModel indexModel = new IndexModel
            {
                StartDate = todaysDate,
                TabIndex = tabIndex,
            };
            switch (tabIndex)
            {
                case 0: //Upcoming Events
                    indexModel.UpcomingFestivalsModels = GenerateUpcomingFestivalsModels(locationNameDesc, locationNameDesc1, serverDirectoryName, todaysDate, execUniqueId);
                    indexModel.TempleEventImportantDatesModels = GenerateImportantDates(locationNameDesc, DateTime.Now, "51, 52, 53, 54, 55, 56, 60, 63, 64, 65, 66", execUniqueId);
                    break;
                case 1: //Sankalpam
                    indexModel.SankalpamInfoModel = new SankalpamInfoModel
                    {
                        StartDate = todaysDate,
                        FinishDate = todaysDate.AddDays(17),
                        SankalpamInfoDetailModels = new List<SankalpamInfoDetailModel>(),
                    };
                    for (int i = 0; i < 18; i++)
                    {
                        SankalpamInfoDetailModel sankalpamInfoDetailModel = new SankalpamInfoDetailModel
                        {
                            SankalpamDate = todaysDate.AddDays(i),
                        };
                        sankalpamInfoDetailModel.CalendarDatas = GenerateCalendarData(locationNameDesc, sankalpamInfoDetailModel.SankalpamDate, sankalpamInfoDetailModel.SankalpamDate, execUniqueId);
                        indexModel.SankalpamInfoModel.SankalpamInfoDetailModels.Add(sankalpamInfoDetailModel);
                    }
                    break;
                case 2: //Daily Info
                    indexModel.DailyInfoModels = new List<DailyInfoModel>();
                    DailyInfoModel dailyInfoModel;
                    DateTime dailyInfoDate;
                    for (int i = 0; i < 18; i++)
                    {
                        dailyInfoDate = todaysDate.AddDays(i);
                        dailyInfoModel = new DailyInfoModel
                        {
                            DailyInfoDate = dailyInfoDate,
                            TempleEventModels = GenerateDailyInfos(locationNameDesc, dailyInfoDate, execUniqueId),
                        };
                        indexModel.DailyInfoModels.Add(dailyInfoModel);
                    }
                    break;
                case 3: //Daily Weekly
                    break;
                case 4: //Important Dates
                    //indexModel.ImportantDates = GenerateImportantDates(locationNameDesc, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")), finishDate, execUniqueId);
                    //indexModel.MonthImportantDatesModels = GetImportantDates2A(locationNameDesc, 6, importantIdsList, "41, 51, 52, 54, 55, 56, 57, 61, 62, 65", execUniqueId);//5001, 5002, 5005, 5006, 5007, 5008, 5009
                    indexModel.TempleEventImportantDatesModels = GenerateImportantDates(locationNameDesc, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")), finishDate, "50, 51, 52, 53, 54, 55, 56, 60, 61, 63, 64, 65, 66", execUniqueId);
                    break;
                case 5: //Festivals
                    indexModel.Festivals = GenerateFestivals(locationNameDesc, todaysDate, execUniqueId);
                    break;
                default:
                    break;
            }
            return indexModel;
        }
        public IndexModel BuildIndexModel(string locationNameDesc, string locationNameDesc1, string importantIdsList, int monthCount, string startDate, string finishDate, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            IndexModel indexModel = new IndexModel
            {
                ImportantDatesModels = GetImportantDates2(locationNameDesc, monthCount, importantIdsList, sqlConnection, execUniqueId),
                TempleInfoDatesModel = new TempleInfoDatesModel
                {
                    StartDate = startDate,
                    FinishDate = finishDate,
                    TempleInfoDateModels = new List<TempleInfoDateModel>(),
                }
            };
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM TempleInfo WHERE StartDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}' ORDER BY StartDate, StartTime, SeqNum", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            TempleInfoDateModel templeInfoDateModel;
            bool sqlDataReaderRead = sqlDataReader.Read();
            while (sqlDataReaderRead)
            {
                indexModel.TempleInfoDatesModel.TempleInfoDateModels.Add
                (
                    templeInfoDateModel = new TempleInfoDateModel
                    {
                        TempleInfoDate = sqlDataReader["StartDate"].ToString(),
                        TempleInfoModels = new List<TempleInfoModel>(),
                    }
                );
                while (sqlDataReaderRead && templeInfoDateModel.TempleInfoDate == sqlDataReader["StartDate"].ToString())
                {
                    templeInfoDateModel.TempleInfoModels.Add
                    (
                        new TempleInfoModel
                        {
                            TempleInfoId = long.Parse(sqlDataReader["TempleInfoId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            ImportantDatesId = long.Parse(sqlDataReader["ImportantDatesId"].ToString()),
                            InfoType = sqlDataReader["InfoType"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishDate = sqlDataReader["FinishDate"].ToString(),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            InfoText1 = sqlDataReader["InfoText1"].ToString(),
                            InfoText2 = sqlDataReader["InfoText2"].ToString(),
                            InfoText3 = sqlDataReader["InfoText3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            ImageName1 = sqlDataReader["ImageName1"].ToString(),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlCommand = new SqlCommand($"SELECT * FROM RiseSet WHERE GregorianDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}' ORDER BY GregorianDate", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoDateModel = indexModel.TempleInfoDatesModel.TempleInfoDateModels.First(x => x.TempleInfoDate == sqlDataReader["GregorianDate"].ToString());
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99000,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99000,
                        StartDate = null,
                        StartTime = sqlDataReader["SunRise"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["SunSet"].ToString(),
                        InfoText1 = "Sunrise / Set",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99001,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99001,
                        StartDate = null,
                        StartTime = sqlDataReader["RKStart"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["RKFinish"].ToString(),
                        InfoText1 = "Rahu",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99002,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99002,
                        StartDate = null,
                        StartTime = sqlDataReader["YGStart"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["YGFinish"].ToString(),
                        InfoText1 = "Yama",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return indexModel;
        }
        public TempleInfoDatesModel GetTempleInfoDates(string locationNameDesc, string locationNameDesc1, string startDate, string finishDate, string execUniqueId)
        {
            TempleInfoDatesModel templeInfoDatesModel = new TempleInfoDatesModel
            {
                StartDate = startDate,
                FinishDate = finishDate,
                TempleInfoDateModels = new List<TempleInfoDateModel>(),
            };
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            TempleInfoDateModel templeInfoDateModel;
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM TempleInfo WHERE StartDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}' ORDER BY StartDate, StartTime, SeqNum", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            while (sqlDataReaderRead)
            {
                templeInfoDatesModel.TempleInfoDateModels.Add
                (
                    templeInfoDateModel = new TempleInfoDateModel
                    {
                        TempleInfoDate = sqlDataReader["StartDate"].ToString(),
                        TempleInfoModels = new List<TempleInfoModel>(),
                    }
                );
                while (sqlDataReaderRead && templeInfoDateModel.TempleInfoDate == sqlDataReader["StartDate"].ToString())
                {
                    templeInfoDateModel.TempleInfoModels.Add
                    (
                        new TempleInfoModel
                        {

                            TempleInfoId = long.Parse(sqlDataReader["TempleInfoId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            ImportantDatesId = long.Parse(sqlDataReader["ImportantDatesId"].ToString()),
                            InfoType = sqlDataReader["InfoType"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishDate = sqlDataReader["FinishDate"].ToString(),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            InfoText1 = sqlDataReader["InfoText1"].ToString(),
                            InfoText2 = sqlDataReader["InfoText2"].ToString(),
                            InfoText3 = sqlDataReader["InfoText3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            ImageName1 = sqlDataReader["ImageName1"].ToString(),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlCommand = new SqlCommand($"SELECT * FROM RiseSet WHERE GregorianDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}' ORDER BY GregorianDate", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoDateModel = templeInfoDatesModel.TempleInfoDateModels.First(x => x.TempleInfoDate == sqlDataReader["GregorianDate"].ToString());
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99000,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99000,
                        StartDate = null,
                        StartTime = sqlDataReader["SunRise"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["SunSet"].ToString(),
                        InfoText1 = "Sunrise / Set",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99001,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99001,
                        StartDate = null,
                        StartTime = sqlDataReader["RKStart"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["RKFinish"].ToString(),
                        InfoText1 = "Rahu",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99002,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99002,
                        StartDate = null,
                        StartTime = sqlDataReader["YGStart"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["YGFinish"].ToString(),
                        InfoText1 = "Yama",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return templeInfoDatesModel;
        }
        public List<UpcomingFestivalsModel> GenerateUpcomingFestivalsModels(string locationNameDesc, string locationNameDesc1, string serverDirectoryName, DateTime processDate, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            string sqlStmt = "";
            sqlStmt = "SELECT * FROM UpcomingFestivals WHERE StatusNameDesc = 'ACTIVE' AND LocationNameDesc = '" + locationNameDesc + "' AND '" + processDate.ToString("yyyy-MM-dd") + "' BETWEEN StartDate AND FinishDate ORDER BY SeqNum";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            UpcomingFestivalsModel upcomingFestivalsModel;
            List<UpcomingFestivalsModel> upcomingFestivalsModels = new List<UpcomingFestivalsModel>();
            int indexNum = 0;
            string fullFileName, fileContents, hrefColor1 = "", hrefColor2 = "", sponsorshipHref;
            StreamReader streamReader;
            while (sqlDataReader.Read())
            {
                upcomingFestivalsModels.Add
                (
                    upcomingFestivalsModel = new UpcomingFestivalsModel
                    {
                        UpcomingFestivalsId = long.Parse(sqlDataReader["UpcomingFestivalsId"].ToString()),
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        UpcomingFestivalsDesc = sqlDataReader["UpcomingFestivalsDesc"].ToString(),
                        SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        LinkTitle = sqlDataReader["LinkTitle"].ToString(),
                        ContentDivId = sqlDataReader["ContentDivId"].ToString(),
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        FinishDate = sqlDataReader["FinishDate"].ToString(),
                        NavigationHtmlFileName1 = sqlDataReader["NavigationHtmlFileName1"].ToString(),
                        NavigationHtmlFileName2 = sqlDataReader["NavigationHtmlFileName2"].ToString(),
                        NavigationHtmlFileName3 = sqlDataReader["NavigationHtmlFileName3"].ToString(),
                        ContentHtmlFileName1 = sqlDataReader["ContentHtmlFileName1"].ToString(),
                        ContentHtmlFileName2 = sqlDataReader["ContentHtmlFileName2"].ToString(),
                        ContentHtmlFileName3 = sqlDataReader["ContentHtmlFileName3"].ToString(),
                        SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                        KioskOrderGroupId = sqlDataReader["KioskOrderGroupId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["KioskOrderGroupId"].ToString()),
                        StatusNameDesc = sqlDataReader["StatusNameDesc"].ToString(),
                    }
                );
                indexNum++;
                try
                {
                    fullFileName = serverDirectoryName + upcomingFestivalsModel.NavigationHtmlFileName1;
                    streamReader = new StreamReader(fullFileName);
                    fileContents = streamReader.ReadToEnd();
                    if (hrefColor1 == "#ff0000")
                    {
                        hrefColor1 = "#0000ff";
                    }
                    else
                    {
                        hrefColor1 = "#ff0000";
                    }
                    fileContents = fileContents.Replace("##@@IndexNum@@##", indexNum.ToString());
                    fileContents = fileContents.Replace("##@@LinkTitle@@##", upcomingFestivalsModel.LinkTitle);
                    fileContents = fileContents.Replace("##@@HrefColor@@##", hrefColor1);
                    streamReader.Close();
                    upcomingFestivalsModel.NavigationHtml = fileContents;
                }
                catch
                {
                    ;
                }
                try
                {
                    fullFileName = serverDirectoryName + upcomingFestivalsModel.ContentHtmlFileName1;
                    streamReader = new StreamReader(fullFileName);
                    fileContents = streamReader.ReadToEnd();
                    if (hrefColor2 == "#ff0000")
                    {
                        hrefColor2 = "#0000ff";
                    }
                    else
                    {
                        hrefColor2 = "#ff0000";
                    }
                    fileContents = fileContents.Replace("##@@LinkTitle@@##", upcomingFestivalsModel.LinkTitle);
                    if (upcomingFestivalsModel.KioskOrderGroupId != null)
                    {
                        sponsorshipHref = "/SVCCTemple" + locationNameDesc1 + "/KioskOrder?id=" + upcomingFestivalsModel.KioskOrderGroupId;
                    }
                    else
                    {
                        if (upcomingFestivalsModel.SponsorshipGroupId != null)
                        {
                            sponsorshipHref = "/SVCCTemple" + locationNameDesc1 + "/Sponsorship/" + upcomingFestivalsModel.SponsorshipGroupId;
                        }
                        else
                        {
                            sponsorshipHref = "";
                        }
                    }
                    fileContents = fileContents.Replace("##@@SponsorshipHref@@##", sponsorshipHref);
                    streamReader.Close();
                    upcomingFestivalsModel.ContentHtml = fileContents;
                }
                catch
                {
                    ;
                }
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return upcomingFestivalsModels;
        }
        public CalendarModel GenerateCalendarData(string locationNameDesc, string yearMonth, string importantDatesIds, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CalendarModel calendarModel = new CalendarModel();
            if (string.IsNullOrWhiteSpace(yearMonth))
            {
                yearMonth = DateTime.Today.ToString("yyyy-MM");
                calendarModel.CalendarFromDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-01"));
            }
            else
            {
                calendarModel.CalendarFromDate = DateTime.Parse(yearMonth + "-01");
            }
            if (calendarModel.CalendarFromDate.DayOfWeek == DayOfWeek.Sunday)
            {
                calendarModel.CalendarStartDate = calendarModel.CalendarFromDate.AddDays(-7);
            }
            else
            {
                calendarModel.CalendarStartDate = calendarModel.CalendarFromDate.AddDays(0 - calendarModel.CalendarFromDate.DayOfWeek);
            }
            calendarModel.CalendarFinishDate = calendarModel.CalendarStartDate.AddDays(42);
            calendarModel.CalendarToDate = calendarModel.CalendarFromDate.AddMonths(1).AddDays(-1);
            calendarModel.CalendarMonthName = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            calendarModel.CalendarYear = new int[] { 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032, 2033, 2034, 2035 };
            calendarModel.CalendarYearSelected = calendarModel.CalendarFromDate.Year;
            calendarModel.CalendarMonthSelected = calendarModel.CalendarFromDate.Month;
            calendarModel.CalendarDataListList = new SortedList<string, List<CalendarData>>();
            calendarModel.CalendarDateListList = new SortedList<DateTime, List<CalendarData>>();
            calendarModel.CalendarEventListList = new SortedList<DateTime, List<CalendarEvent>>();
            GetCalendarData(locationNameDesc, yearMonth, calendarModel.CalendarStartDate, calendarModel.CalendarFinishDate, calendarModel.CalendarFromDate, calendarModel.CalendarToDate, calendarModel.CalendarDataListList, calendarModel.CalendarDateListList, calendarModel.CalendarEventListList, execUniqueId);
            GetImportantDates3(locationNameDesc, calendarModel, 1, importantDatesIds, execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return calendarModel;
        }
        public void GetCalendarData(string locationNameDesc, string yearMonth, DateTime calendarStartDate, DateTime calendarFinishDate, DateTime calendarFromDate, DateTime calendarToDate, SortedList<string, List<CalendarData>> calendarDataListList, SortedList<DateTime, List<CalendarData>> calendarDateListList, SortedList<DateTime, List<CalendarEvent>> calendarEventListList, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            DateTime fromDate, toDate;
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(BuildSqlStmtCalendarData(execUniqueId), sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters.Add("@FinishDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@StartDate"].Value = calendarStartDate.ToString("yyyy-MM-dd");
            sqlCommand.Parameters["@FinishDate"].Value = calendarFinishDate.ToString("yyyy-MM-dd");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            string codeTypeNameDesc;
            List<CalendarData> calendarDataList;
            List<CalendarData> calendarDateList;
            CalendarData calendarDataInstance;
            bool addCalendarData;
            int yearMonthNum, yearMonthStartDate, yearMonthFinishDate;
            while (sqlDataReaderRead)
            {
                codeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString();
                calendarDataList = new List<CalendarData>();
                calendarDataListList.Add(codeTypeNameDesc, calendarDataList);
                while (sqlDataReaderRead && sqlDataReader["CodeTypeNameDesc"].ToString() == codeTypeNameDesc)
                {
                    if (sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_YEAR" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_YEAR_PART" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_SEASON" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_MONTH" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_RAASI")
                    {
                        yearMonthNum = int.Parse(yearMonth.Replace("-", ""));
                        yearMonthStartDate = int.Parse(DateTime.Parse(sqlDataReader["StartDate"].ToString()).ToString("yyyyMM"));
                        yearMonthFinishDate = int.Parse(DateTime.Parse(sqlDataReader["FinishDate"].ToString()).ToString("yyyyMM"));
                        if (yearMonthNum >= yearMonthStartDate && yearMonthNum <= yearMonthFinishDate)
                        {
                            addCalendarData = true;
                        }
                        else
                        {
                            addCalendarData = false;
                        }
                    }
                    else
                    {
                        addCalendarData = true;
                    }
                    if (addCalendarData)
                    {
                        calendarDataInstance = new CalendarData
                        {
                            StartDate = DateTime.Parse(sqlDataReader["StartDate"].ToString()),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishDate = DateTime.Parse(sqlDataReader["FinishDate"].ToString()),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            CodeTypeNameId = long.Parse(sqlDataReader["CodeTypeNameId"].ToString()),
                            CodeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString(),
                            CalendarCodeId = long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                            CodeDataNameId = long.Parse(sqlDataReader["CodeDataNameId"].ToString()),
                            CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                            CodeDataDesc9 = sqlDataReader["CodeDataDesc9"].ToString(),
                        };
                        calendarDataList.Add(calendarDataInstance);
                        if (calendarDataInstance.CodeTypeNameDesc == "PANCHANGAM_THITHI" || calendarDataInstance.CodeTypeNameDesc == "PANCHANGAM_NAKSHATRA" || calendarDataInstance.CodeTypeNameDesc == "RISE_SET")
                        {
                            fromDate = calendarDataInstance.StartDate;
                            toDate = calendarDataInstance.FinishDate;
                            while (fromDate <= toDate)
                            {
                                if (fromDate < calendarStartDate)
                                {

                                }
                                else
                                {
                                    if (!calendarDateListList.TryGetValue(fromDate, out calendarDateList))
                                    {
                                        calendarDateList = new List<CalendarData>();
                                        calendarDateListList.Add(fromDate, calendarDateList);
                                    }
                                    if (fromDate < calendarFromDate || toDate > calendarToDate)
                                    {
                                        calendarDataInstance.Color = "#aaaaaa";
                                    }
                                    else
                                    {
                                        switch (calendarDataInstance.CodeTypeNameDesc)
                                        {
                                            case "PANCHANGAM_NAKSHATRA":
                                                calendarDataInstance.Color = "#0000ff";
                                                break;
                                            case "RISE_SET":
                                                calendarDataInstance.Color = "#a54000";
                                                break;
                                            default:
                                                calendarDataInstance.Color = "#000000";
                                                break;
                                        }
                                    }
                                    calendarDateList.Add(calendarDataInstance);
                                }
                                fromDate = fromDate.AddDays(1);
                            }
                        }
                    }
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlCommand = new SqlCommand(BuildSqlStmtCalendarEventData(execUniqueId), sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters.Add("@FinishDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@StartDate"].Value = calendarStartDate.ToString("yyyy-MM-dd");
            sqlCommand.Parameters["@FinishDate"].Value = calendarFinishDate.ToString("yyyy-MM-dd");
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReaderRead = sqlDataReader.Read();
            string eventDate;
            List<CalendarEvent> calendarEventList;
            while (sqlDataReaderRead)
            {
                eventDate = sqlDataReader["EventDate"].ToString();
                calendarEventList = new List<CalendarEvent>();
                calendarEventListList.Add(DateTime.Parse(eventDate), calendarEventList);
                while (sqlDataReaderRead && sqlDataReader["EventDate"].ToString() == eventDate)
                {
                    calendarEventList.Add
                    (
                        new CalendarEvent
                        {
                            CalendarEventId = long.Parse(sqlDataReader["CalendarEventId"].ToString()),
                            LocationNameDesc = locationNameDesc,
                            EventDate = sqlDataReader["EventDate"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            EventText = sqlDataReader["EventText"].ToString(),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return;
        }
        public List<CalendarData> GenerateCalendarData(string locationNameDesc, DateTime startDate, DateTime finishDate, string execUniqueId)
        {
            List<CalendarData> calendarDatas = new List<CalendarData>();
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(BuildSqlStmtCalendarData(execUniqueId), sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters.Add("@FinishDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
            sqlCommand.Parameters["@FinishDate"].Value = finishDate.ToString("yyyy-MM-dd");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                calendarDatas.Add
                (
                    new CalendarData
                    {
                        StartDate = DateTime.Parse(sqlDataReader["StartDate"].ToString()),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        FinishDate = DateTime.Parse(sqlDataReader["FinishDate"].ToString()),
                        FinishTime = sqlDataReader["FinishTime"].ToString(),
                        CodeTypeNameId = long.Parse(sqlDataReader["CodeTypeNameId"].ToString()),
                        CodeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString(),
                        CodeTypeDesc = sqlDataReader["CodeTypeDesc"].ToString(),
                        CodeTypeDesc1 = sqlDataReader["CodeTypeDesc1"].ToString(),
                        CalendarCodeId = long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                        CodeDataNameId = long.Parse(sqlDataReader["CodeDataNameId"].ToString()),
                        CodeDataNameDesc = sqlDataReader["CodeDataNameDesc"].ToString(),
                        CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                        CodeDataDesc9 = sqlDataReader["CodeDataDesc9"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return calendarDatas;
        }
        public List<TempleEventModel> GenerateDailyInfos(string locationNameDesc, DateTime todaysDate, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            List<TempleEventModel> templeEventModels = new List<TempleEventModel>();
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.SponsorshipGroupId, -1) WHEN -1 THEN TempleEventMaster.SponsorshipGroupId ELSE TempleEvent.SponsorshipGroupId END AS SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.KioskGroupId, -1) WHEN -1 THEN TempleEventMaster.KioskGroupId ELSE TempleEvent.KioskGroupId END AS KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "          FROM TempleEvent" + Environment.NewLine;
            sqlStmt += "    INNER JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "            ON TempleEvent.TempleEventMasterId = TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "         WHERE TempleEvent.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND (TempleEvent.StartDate = '" + todaysDate.ToString("yyyy-MM-dd") + "' OR '" + todaysDate.ToString("yyyy-MM-dd") + "' BETWEEN TempleEvent.StartDate AND TempleEvent.FinishDate)" + Environment.NewLine;
            sqlStmt += "           AND TempleEventMaster.TempleEventMasterId NOT IN(99998)" + Environment.NewLine;
            sqlStmt += "      ORDER BY TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,StartTime" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeEventModels.Add
                (
                    new TempleEventModel
                    {
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventText1 = sqlDataReader["EventText1"].ToString(),
                        EventText2 = sqlDataReader["EventText2"].ToString(),
                        EventText3 = sqlDataReader["EventText3"].ToString(),
                        TempleEventMasterModel = new TempleEventMasterModel
                        {
                            TempleEventMasterId = long.Parse(sqlDataReader["TempleEventMasterId"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            KioskGroupId = sqlDataReader["KioskGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                            FileName1 = sqlDataReader["FileName1"].ToString(),
                            FileName2 = sqlDataReader["FileName2"].ToString(),
                            FileName3 = sqlDataReader["FileName3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            HtmlFileName2 = sqlDataReader["HtmlFileName2"].ToString(),
                            HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                        },
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return templeEventModels;
        }
        public List<TempleEventModel> GenerateFestivals(string locationNameDesc, DateTime startDate, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            List<TempleEventModel> templeEventModels = new List<TempleEventModel>();
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               1 AS QueryNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName2, '') WHEN '' THEN TempleEventMaster.ImageFileName2 ELSE TempleEvent.ImageFileName2 END AS ImageFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName3, '') WHEN '' THEN TempleEventMaster.ImageFileName3 ELSE TempleEvent.ImageFileName1 END AS ImageFileName3" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FinishTime, '') WHEN '' THEN TempleEventMaster.FinishTime ELSE TempleEvent.FinishTime END AS FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.SponsorshipGroupId, -1) WHEN -1 THEN TempleEventMaster.SponsorshipGroupId ELSE TempleEvent.SponsorshipGroupId END AS SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.KioskGroupId, -1) WHEN -1 THEN TempleEventMaster.KioskGroupId ELSE TempleEvent.KioskGroupId END AS KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.SeqNum" + Environment.NewLine;
            sqlStmt += "          FROM TempleEvent" + Environment.NewLine;
            sqlStmt += "    INNER JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "            ON TempleEvent.TempleEventMasterId = TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "           AND TempleEventMaster.TempleEventMasterId IN(99999)" + Environment.NewLine;
            sqlStmt += "         WHERE TempleEvent.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               TempleEvent.StartDate >= '" + startDate.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "            OR '" + startDate.ToString("yyyy-MM-dd") + "' BETWEEN TempleEvent.StartDate AND TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "UNION" + Environment.NewLine;
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               1 AS QueryNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName2, '') WHEN '' THEN TempleEventMaster.ImageFileName2 ELSE TempleEvent.ImageFileName2 END AS ImageFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName3, '') WHEN '' THEN TempleEventMaster.ImageFileName3 ELSE TempleEvent.ImageFileName1 END AS ImageFileName3" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FinishTime, '') WHEN '' THEN TempleEventMaster.FinishTime ELSE TempleEvent.FinishTime END AS FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.SponsorshipGroupId, -1) WHEN -1 THEN TempleEventMaster.SponsorshipGroupId ELSE TempleEvent.SponsorshipGroupId END AS SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.KioskGroupId, -1) WHEN -1 THEN TempleEventMaster.KioskGroupId ELSE TempleEvent.KioskGroupId END AS KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.SeqNum" + Environment.NewLine;
            sqlStmt += "          FROM TempleEvent" + Environment.NewLine;
            sqlStmt += "    INNER JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "            ON TempleEvent.TempleEventMasterId = TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "           AND TempleEventMaster.TempleEventMasterId IN(99999)" + Environment.NewLine;
            sqlStmt += "         WHERE TempleEvent.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               TempleEvent.StartDate >= '" + startDate.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "            OR '" + startDate + "' BETWEEN TempleEvent.StartDate AND TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "      ORDER BY QueryNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.SeqNum" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeEventModels.Add
                (
                    new TempleEventModel
                    {
                        QueryNum = byte.Parse(sqlDataReader["QueryNum"].ToString()),
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventText1 = sqlDataReader["EventText1"].ToString(),
                        EventText2 = sqlDataReader["EventText2"].ToString(),
                        EventText3 = sqlDataReader["EventText3"].ToString(),
                        FinishDate = sqlDataReader["FinishDate"].ToString(),
                        FinishTime = sqlDataReader["FinishTime"].ToString(),
                        TempleEventMasterModel = new TempleEventMasterModel
                        {
                            TempleEventMasterId = long.Parse(sqlDataReader["TempleEventMasterId"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            KioskGroupId = sqlDataReader["KioskGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                            FileName1 = sqlDataReader["FileName1"].ToString(),
                            FileName2 = sqlDataReader["FileName2"].ToString(),
                            FileName3 = sqlDataReader["FileName3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            HtmlFileName2 = sqlDataReader["HtmlFileName2"].ToString(),
                            HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                            ImageFileName1 = sqlDataReader["ImageFileName1"].ToString(),
                            ImageFileName2 = sqlDataReader["ImageFileName2"].ToString(),
                            ImageFileName3 = sqlDataReader["ImageFileName3"].ToString(),
                        },
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return templeEventModels;
        }
        public List<TempleEventModel> GenerateTempleFestivals(string locationNameDesc, string startDate, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            List<TempleEventModel> templeEventModels = new List<TempleEventModel>();
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               1 AS QueryNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName2, '') WHEN '' THEN TempleEventMaster.ImageFileName2 ELSE TempleEvent.ImageFileName2 END AS ImageFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName3, '') WHEN '' THEN TempleEventMaster.ImageFileName3 ELSE TempleEvent.ImageFileName1 END AS ImageFileName3" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FinishTime, '') WHEN '' THEN TempleEventMaster.FinishTime ELSE TempleEvent.FinishTime END AS FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.SponsorshipGroupId, -1) WHEN -1 THEN TempleEventMaster.SponsorshipGroupId ELSE TempleEvent.SponsorshipGroupId END AS SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.KioskGroupId, -1) WHEN -1 THEN TempleEventMaster.KioskGroupId ELSE TempleEvent.KioskGroupId END AS KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.SeqNum" + Environment.NewLine;
            sqlStmt += "          FROM TempleEvent" + Environment.NewLine;
            sqlStmt += "    INNER JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "            ON TempleEvent.TempleEventMasterId = TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "           AND TempleEventMaster.TempleEventMasterId IN(50, 51, 52, 53, 54, 55, 56, 60, 61, 63, 64, 65, 66, 99998, 99999)" + Environment.NewLine;
            sqlStmt += "         WHERE TempleEvent.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND (TempleEvent.StartDate >= '" + startDate + "') OR '" + startDate + "' BETWEEN TempleEvent.StartDate AND TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "UNION" + Environment.NewLine;
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               2 AS QueryNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName2, '') WHEN '' THEN TempleEventMaster.ImageFileName2 ELSE TempleEvent.ImageFileName2 END AS ImageFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName3, '') WHEN '' THEN TempleEventMaster.ImageFileName3 ELSE TempleEvent.ImageFileName1 END AS ImageFileName3" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FinishTime, '') WHEN '' THEN TempleEventMaster.FinishTime ELSE TempleEvent.FinishTime END AS FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.SponsorshipGroupId, -1) WHEN -1 THEN TempleEventMaster.SponsorshipGroupId ELSE TempleEvent.SponsorshipGroupId END AS SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.KioskGroupId, -1) WHEN -1 THEN TempleEventMaster.KioskGroupId ELSE TempleEvent.KioskGroupId END AS KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.SeqNum" + Environment.NewLine;
            sqlStmt += "          FROM TempleEvent" + Environment.NewLine;
            sqlStmt += "    INNER JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "            ON TempleEvent.TempleEventMasterId = TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "           AND TempleEventMaster.TempleEventMasterId IN(50, 51, 52, 53, 54, 55, 56, 60, 61, 63, 64, 65, 66, 99998, 99999)" + Environment.NewLine;
            sqlStmt += "         WHERE TempleEvent.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND TempleEvent.StartDate BETWEEN '" + DateTime.Now.ToString("yyyy-01-01") + "' AND '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY QueryNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.SeqNum" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeEventModels.Add
                (
                    new TempleEventModel
                    {
                        QueryNum = byte.Parse(sqlDataReader["QueryNum"].ToString()),
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventText1 = sqlDataReader["EventText1"].ToString(),
                        EventText2 = sqlDataReader["EventText2"].ToString(),
                        EventText3 = sqlDataReader["EventText3"].ToString(),
                        FinishDate = sqlDataReader["FinishDate"].ToString(),
                        FinishTime = sqlDataReader["FinishTime"].ToString(),
                        TempleEventMasterModel = new TempleEventMasterModel
                        {
                            TempleEventMasterId = long.Parse(sqlDataReader["TempleEventMasterId"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            KioskGroupId = sqlDataReader["KioskGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                            FileName1 = sqlDataReader["FileName1"].ToString(),
                            FileName2 = sqlDataReader["FileName2"].ToString(),
                            FileName3 = sqlDataReader["FileName3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            HtmlFileName2 = sqlDataReader["HtmlFileName2"].ToString(),
                            HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                            ImageFileName1 = sqlDataReader["ImageFileName1"].ToString(),
                            ImageFileName2 = sqlDataReader["ImageFileName2"].ToString(),
                            ImageFileName3 = sqlDataReader["ImageFileName3"].ToString(),
                        },
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return templeEventModels;
        }
        public List<TempleEventModel> GenerateImportantDates(string locationNameDesc, DateTime startDate, DateTime finishDate, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            List<TempleEventModel> importantDates = new List<TempleEventModel>();
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               MonthList_TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN MonthList_TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN MonthList_TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN MonthList_TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN MonthList_TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN MonthList_TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN MonthList_TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN MonthList_TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN MonthList_TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.EndDate" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "                 SELECT" + Environment.NewLine;
            sqlStmt += "                        *" + Environment.NewLine;
            sqlStmt += "                   FROM MonthList" + Environment.NewLine;
            sqlStmt += "             CROSS JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "                  WHERE MonthList.BeginDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + finishDate.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "                    AND TempleEventMaster.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "                    AND TempleEventMaster.TempleEventMasterId IN(50, 51, 52, 53, 54, 55, 56, 60, 61, 63, 64, 65, 66)" + Environment.NewLine;
            sqlStmt += "              ) AS MonthList_TempleEventMaster" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN TempleEvent" + Environment.NewLine;
            sqlStmt += "            ON MonthList_TempleEventMaster.TempleEventMasterId = TempleEvent.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "            AND TempleEvent.StartDate BETWEEN MonthList_TempleEventMaster.BeginDate AND MonthList_TempleEventMaster.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_TempleEventMaster.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY " + Environment.NewLine;
            sqlStmt += "               MonthList_TempleEventMaster.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                importantDates.Add
                (
                    new TempleEventModel
                    {
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventText1 = sqlDataReader["EventText1"].ToString(),
                        EventText2 = sqlDataReader["EventText2"].ToString(),
                        EventText3 = sqlDataReader["EventText3"].ToString(),
                        BeginDate = sqlDataReader["BeginDate"].ToString(),
                        TempleEventMasterModel = new TempleEventMasterModel
                        {
                            TempleEventMasterId = long.Parse(sqlDataReader["TempleEventMasterId"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? -1 : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            FileName1 = sqlDataReader["FileName1"].ToString(),
                            ImageFileName1 = sqlDataReader["ImageFileName1"].ToString(),
                        },
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return importantDates;
        }
        public List<TempleEventImportantDatesModel> GenerateImportantDates(string locationNameDesc, DateTime startDate, DateTime finishDate, string importDatesIds, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            #region
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               MonthList_TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.EndDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN MonthList_TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN MonthList_TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN MonthList_TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN MonthList_TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN MonthList_TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN MonthList_TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN MonthList_TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN MonthList_TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN MonthList_TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName2, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName2 ELSE TempleEvent.ImageFileName2 END AS ImageFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName3, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName3 ELSE TempleEvent.ImageFileName3 END AS ImageFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN MonthList_TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN MonthList_TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN MonthList_TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN MonthList_TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "                 SELECT" + Environment.NewLine;
            sqlStmt += "                        *" + Environment.NewLine;
            sqlStmt += "                   FROM MonthList" + Environment.NewLine;
            sqlStmt += "             CROSS JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "                  WHERE MonthList.BeginDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + finishDate.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "                    AND TempleEventMaster.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "                    AND TempleEventMaster.TempleEventMasterId IN(50, 51, 52, 53, 54, 55, 56, 60, 61, 63, 64, 65, 66)" + Environment.NewLine;
            sqlStmt += "              ) AS MonthList_TempleEventMaster" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN TempleEvent" + Environment.NewLine;
            sqlStmt += "            ON MonthList_TempleEventMaster.TempleEventMasterId = TempleEvent.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "            AND TempleEvent.StartDate BETWEEN MonthList_TempleEventMaster.BeginDate AND MonthList_TempleEventMaster.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_TempleEventMaster.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY " + Environment.NewLine;
            sqlStmt += "               MonthList_TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            #endregion
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            List<TempleEventImportantDatesModel> templeEventImportantDatesModels = new List<TempleEventImportantDatesModel>();
            TempleEventImportantDatesModel templeEventImportantDatesModel;
            while (sqlDataReaderRead)
            {
                templeEventImportantDatesModels.Add
                (
                    templeEventImportantDatesModel = new TempleEventImportantDatesModel
                    {
                        TempleEventMasterId = sqlDataReader["TempleEventMasterId"].ToString(),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventText1 = sqlDataReader["EventText1"].ToString(),
                        EventText2 = sqlDataReader["EventText2"].ToString(),
                        EventText3 = sqlDataReader["EventText3"].ToString(),
                        FileName1 = sqlDataReader["FileName1"].ToString(),
                        FileName2 = sqlDataReader["FileName2"].ToString(),
                        FileName3 = sqlDataReader["FileName3"].ToString(),
                        ImageFileName1 = sqlDataReader["ImageFileName1"].ToString(),
                        ImageFileName2 = sqlDataReader["ImageFileName2"].ToString(),
                        ImageFileName3 = sqlDataReader["ImageFileName3"].ToString(),
                        HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                        HtmlFileName2 = sqlDataReader["HtmlFileName2"].ToString(),
                        HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                        SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                        KioskGroupId = sqlDataReader["KioskGroupId"].ToString(),
                        SeqNum = sqlDataReader["SeqNum"].ToString(),
                        BeginDates = new List<string>(),
                        EndDates = new List<string>(),
                        StartDates = new List<string>(),
                        StartTimes = new List<string>(),
                        EventName1s = new List<string>(),
                        EventName2s = new List<string>(),
                        EventName3s = new List<string>(),
                    }
                );
                while (sqlDataReaderRead && sqlDataReader["TempleEventMasterId"].ToString() == templeEventImportantDatesModel.TempleEventMasterId)
                {
                    templeEventImportantDatesModel.BeginDates.Add(sqlDataReader["BeginDate"].ToString());
                    templeEventImportantDatesModel.EndDates.Add(sqlDataReader["EndDate"].ToString());
                    templeEventImportantDatesModel.StartDates.Add(sqlDataReader["StartDate"].ToString());
                    templeEventImportantDatesModel.StartTimes.Add(sqlDataReader["StartTime"].ToString());
                    templeEventImportantDatesModel.EventName1s.Add(sqlDataReader["EventName1"].ToString());
                    templeEventImportantDatesModel.EventName2s.Add(sqlDataReader["EventName2"].ToString());
                    templeEventImportantDatesModel.EventName3s.Add(sqlDataReader["EventName3"].ToString());
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return templeEventImportantDatesModels;
        }
        public List<TempleEventImportantDatesModel> GenerateImportantDates(string locationNameDesc, DateTime todaysDate, string importDatesIds, string execUniqueId)
        {
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            DateTime startDate, finishDate;
            startDate = DateTime.Parse(todaysDate.ToString("yyyy-MM-01"));
            finishDate = DateTime.Parse(startDate.AddMonths(13).ToString("yyyy-MM-01")).AddDays(-1);
            #region
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               MonthList_TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.EndDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName1, '') WHEN '' THEN MonthList_TempleEventMaster.EventName1 ELSE TempleEvent.EventName1 END AS EventName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName2, '') WHEN '' THEN MonthList_TempleEventMaster.EventName2 ELSE TempleEvent.EventName2 END AS EventName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventName3, '') WHEN '' THEN MonthList_TempleEventMaster.EventName3 ELSE TempleEvent.EventName3 END AS EventName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc1, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc1 ELSE TempleEvent.EventDesc1 END AS EventDesc1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc2, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc2 ELSE TempleEvent.EventDesc2 END AS EventDesc2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventDesc3, '') WHEN '' THEN MonthList_TempleEventMaster.EventDesc3 ELSE TempleEvent.EventDesc3 END AS EventDesc3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText1, '') WHEN '' THEN MonthList_TempleEventMaster.EventText1 ELSE TempleEvent.EventText1 END AS EventText1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText2, '') WHEN '' THEN MonthList_TempleEventMaster.EventText2 ELSE TempleEvent.EventText2 END AS EventText2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.EventText3, '') WHEN '' THEN MonthList_TempleEventMaster.EventText3 ELSE TempleEvent.EventText3 END AS EventText3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName1, '') WHEN '' THEN MonthList_TempleEventMaster.FileName1 ELSE TempleEvent.FileName1 END AS FileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName2, '') WHEN '' THEN MonthList_TempleEventMaster.FileName2 ELSE TempleEvent.FileName2 END AS FileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.FileName3, '') WHEN '' THEN MonthList_TempleEventMaster.FileName3 ELSE TempleEvent.FileName3 END AS FileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName1, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName1 ELSE TempleEvent.ImageFileName1 END AS ImageFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName2, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName2 ELSE TempleEvent.ImageFileName2 END AS ImageFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.ImageFileName3, '') WHEN '' THEN MonthList_TempleEventMaster.ImageFileName3 ELSE TempleEvent.ImageFileName3 END AS ImageFileName3" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName1, '') WHEN '' THEN MonthList_TempleEventMaster.HtmlFileName1 ELSE TempleEvent.HtmlFileName1 END AS HtmlFileName1" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName2, '') WHEN '' THEN MonthList_TempleEventMaster.HtmlFileName2 ELSE TempleEvent.HtmlFileName2 END AS HtmlFileName2" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.HtmlFileName3, '') WHEN '' THEN MonthList_TempleEventMaster.HtmlFileName3 ELSE TempleEvent.HtmlFileName3 END AS HtmlFileName3" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,CASE ISNULL(TempleEvent.StartTime, '') WHEN '' THEN MonthList_TempleEventMaster.StartTime ELSE TempleEvent.StartTime END AS StartTime" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "                 SELECT" + Environment.NewLine;
            sqlStmt += "                        *" + Environment.NewLine;
            sqlStmt += "                   FROM MonthList" + Environment.NewLine;
            sqlStmt += "             CROSS JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "                  WHERE MonthList.BeginDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + finishDate.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "                    AND TempleEventMaster.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "                    AND TempleEventMaster.TempleEventMasterId IN(" + importDatesIds + ")" + Environment.NewLine;
            sqlStmt += "              ) AS MonthList_TempleEventMaster" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN TempleEvent" + Environment.NewLine;
            sqlStmt += "            ON MonthList_TempleEventMaster.TempleEventMasterId = TempleEvent.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "            AND TempleEvent.StartDate BETWEEN MonthList_TempleEventMaster.BeginDate AND MonthList_TempleEventMaster.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_TempleEventMaster.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND TempleEvent.StartDate >= '" + todaysDate.ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY " + Environment.NewLine;
            sqlStmt += "               TempleEvent.StartDate" + Environment.NewLine;
            sqlStmt += "              ,TempleEvent.StartTime" + Environment.NewLine;
            sqlStmt += "              ,MonthList_TempleEventMaster.SeqNum" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            #endregion
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            List<TempleEventImportantDatesModel> templeEventImportantDatesModels = new List<TempleEventImportantDatesModel>();
            TempleEventImportantDatesModel templeEventImportantDatesModel;
            while (sqlDataReaderRead)
            {
                templeEventImportantDatesModels.Add
                (
                    templeEventImportantDatesModel = new TempleEventImportantDatesModel
                    {
                        TempleEventMasterId = sqlDataReader["TempleEventMasterId"].ToString(),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventText1 = sqlDataReader["EventText1"].ToString(),
                        EventText2 = sqlDataReader["EventText2"].ToString(),
                        EventText3 = sqlDataReader["EventText3"].ToString(),
                        FileName1 = sqlDataReader["FileName1"].ToString(),
                        FileName2 = sqlDataReader["FileName2"].ToString(),
                        FileName3 = sqlDataReader["FileName3"].ToString(),
                        ImageFileName1 = sqlDataReader["ImageFileName1"].ToString(),
                        ImageFileName2 = sqlDataReader["ImageFileName2"].ToString(),
                        ImageFileName3 = sqlDataReader["ImageFileName3"].ToString(),
                        HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                        HtmlFileName2 = sqlDataReader["HtmlFileName2"].ToString(),
                        HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                        SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                        KioskGroupId = sqlDataReader["KioskGroupId"].ToString(),
                        SeqNum = sqlDataReader["SeqNum"].ToString(),
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        BeginDates = new List<string>(),
                        EndDates = new List<string>(),
                        StartDates = new List<string>(),
                        StartTimes = new List<string>(),
                        EventName1s = new List<string>(),
                        EventName2s = new List<string>(),
                        EventName3s = new List<string>(),
                    }
                );
                while (sqlDataReaderRead && sqlDataReader["TempleEventMasterId"].ToString() == templeEventImportantDatesModel.TempleEventMasterId)
                {
                    templeEventImportantDatesModel.BeginDates.Add(sqlDataReader["BeginDate"].ToString());
                    templeEventImportantDatesModel.EndDates.Add(sqlDataReader["EndDate"].ToString());
                    templeEventImportantDatesModel.StartDates.Add(sqlDataReader["StartDate"].ToString());
                    templeEventImportantDatesModel.StartTimes.Add(sqlDataReader["StartTime"].ToString());
                    templeEventImportantDatesModel.EventName1s.Add(sqlDataReader["EventName1"].ToString());
                    templeEventImportantDatesModel.EventName2s.Add(sqlDataReader["EventName2"].ToString());
                    templeEventImportantDatesModel.EventName3s.Add(sqlDataReader["EventName3"].ToString());
                    sqlDataReaderRead = sqlDataReader.Read();
                }
                if (templeEventImportantDatesModels.Count >= 6)
                {
                    break;
                }
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return templeEventImportantDatesModels;
        }
        public List<ImportantDatesModel> GetImportantDates(string locationNameDesc, string execUniqueId)
        {
            List<ImportantDatesModel> importantDatesModels = new List<ImportantDatesModel>();
            string fromDate, toDate, sqlStmt;
            fromDate = DateTime.Now.ToString("yyyy-MM-01");
            toDate = DateTime.Parse(fromDate).AddMonths(9).AddDays(-1).ToString("yyyy-MM-dd");
            #region
            sqlStmt = "";
            sqlStmt += "        SELECT MonthList_ImportantDates.*" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventDate" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventTime" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText1" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText2" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText3" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               *" + Environment.NewLine;
            sqlStmt += "          FROM MonthList" + Environment.NewLine;
            sqlStmt += "    CROSS JOIN ImportantDates" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList.BeginDate BETWEEN '" + fromDate + "' AND '" + toDate + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "              ) MonthList_ImportantDates" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN ImportantDatesDate" + Environment.NewLine;
            sqlStmt += "            ON MonthList_ImportantDates.ImportantDatesId = ImportantDatesDate.ImportantDatesId" + Environment.NewLine;
            sqlStmt += "           AND ImportantDatesDate.EventDate BETWEEN MonthList_ImportantDates.BeginDate AND MonthList_ImportantDates.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY MonthList_ImportantDates.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,MonthList_ImportantDates.BeginDate" + Environment.NewLine;
            SqlConnection sqlConnection = new SqlConnection(System.Web.HttpContext.Current.Application["DatabaseConnectionString"].ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            #endregion
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ImportantDatesModel importantDatesModel;
            importantDatesModel = new ImportantDatesModel
            {
                ImportantDatesId = "",
                BeginDate = fromDate,
                EndDate = toDate,
            };
            importantDatesModels.Add(importantDatesModel);
            while (sqlDataReader.Read())
            {
                importantDatesModel = new ImportantDatesModel
                {
                    MonthListId = sqlDataReader["MonthListId"].ToString(),
                    BeginDate = sqlDataReader["BeginDate"].ToString(),
                    EndDate = sqlDataReader["EndDate"].ToString(),
                    ImportantDatesId = sqlDataReader["ImportantDatesId"].ToString(),
                    LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                    SeqNum = sqlDataReader["SeqNum"].ToString(),
                    EventName1 = sqlDataReader["EventName1"].ToString(),
                    EventName2 = sqlDataReader["EventName2"].ToString(),
                    EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                    EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                    StartTime = sqlDataReader["StartTime"].ToString(),
                    FinishTime = sqlDataReader["FinishTime"].ToString(),
                    ImageName1 = sqlDataReader["ImageName1"].ToString(),
                    ImageName2 = sqlDataReader["ImageName2"].ToString(),
                    EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
                    EventDate = sqlDataReader["EventDate"].ToString(),
                    EventTime = sqlDataReader["EventTime"].ToString(),
                    EventText1 = sqlDataReader["EventText1"].ToString(),
                    EventText2 = sqlDataReader["EventText2"].ToString(),
                    EventText3 = sqlDataReader["EventText3"].ToString(),
                };
                importantDatesModels.Add(importantDatesModel);
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return importantDatesModels;
        }
        public List<ImportantDatesModel> GetImportantDates2(string locationNameDesc, int monthCount, string importantIdsList, SqlConnection sqlConnection, string execUniqueId)
        {
            string fromDate, toDate, sqlStmt;
            fromDate = DateTime.Now.ToString("yyyy-MM-01");
            toDate = DateTime.Parse(fromDate).AddMonths(monthCount).AddDays(-1).ToString("yyyy-MM-dd");
            sqlStmt = "";
            sqlStmt += "        SELECT MonthList_ImportantDates.*" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventDate" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventTime" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText1" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText2" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText3" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               *" + Environment.NewLine;
            sqlStmt += "          FROM MonthList" + Environment.NewLine;
            sqlStmt += "    CROSS JOIN ImportantDates" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList.BeginDate BETWEEN '" + fromDate + "' AND '" + toDate + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.ImportantDatesId IN(" + importantIdsList + ")" + Environment.NewLine;
            sqlStmt += "              ) MonthList_ImportantDates" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN ImportantDatesDate" + Environment.NewLine;
            sqlStmt += "            ON MonthList_ImportantDates.ImportantDatesId = ImportantDatesDate.ImportantDatesId" + Environment.NewLine;
            sqlStmt += "           AND ImportantDatesDate.EventDate BETWEEN MonthList_ImportantDates.BeginDate AND MonthList_ImportantDates.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY MonthList_ImportantDates.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_ImportantDates.SeqNum" + Environment.NewLine;
            bool sqlConnectionClose = false;
            if (sqlConnection == null)
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                sqlConnectionClose = true;
            }
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ImportantDatesModel importantDatesModels;
            List<ImportantDatesModel> importantDatesModelsList = new List<ImportantDatesModel>();
            importantDatesModels = new ImportantDatesModel
            {
                ImportantDatesId = "",
                BeginDate = fromDate,
                EndDate = toDate,
            };
            importantDatesModelsList.Add(importantDatesModels);
            while (sqlDataReader.Read())
            {
                importantDatesModels = new ImportantDatesModel
                {
                    MonthListId = sqlDataReader["MonthListId"].ToString(),
                    BeginDate = sqlDataReader["BeginDate"].ToString(),
                    EndDate = sqlDataReader["EndDate"].ToString(),
                    ImportantDatesId = sqlDataReader["ImportantDatesId"].ToString(),
                    LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                    SeqNum = sqlDataReader["SeqNum"].ToString(),
                    EventName1 = sqlDataReader["EventName1"].ToString(),
                    EventName2 = sqlDataReader["EventName2"].ToString(),
                    EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                    EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                    StartTime = sqlDataReader["StartTime"].ToString(),
                    FinishTime = sqlDataReader["FinishTime"].ToString(),
                    ImageName1 = sqlDataReader["ImageName1"].ToString(),
                    ImageName2 = sqlDataReader["ImageName2"].ToString(),
                    EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
                    EventDate = sqlDataReader["EventDate"].ToString(),
                    EventTime = sqlDataReader["EventTime"].ToString().Trim() == "" ? "" : DateTime.Parse("1900-01-01 " + sqlDataReader["EventTime"].ToString()).ToString("hh:mm tt"),
                    EventText1 = sqlDataReader["EventText1"].ToString(),
                    EventText2 = sqlDataReader["EventText2"].ToString(),
                    EventText3 = sqlDataReader["EventText3"].ToString(),
                    SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                };
                importantDatesModelsList.Add(importantDatesModels);
            }
            sqlDataReader.Close();
            if (sqlConnectionClose)
            {
                sqlConnection.Close();
            }
            return importantDatesModelsList;
        }
        public Dictionary<string, List<ImportantDatesModelNew>> GetImportantDates2A(string locationNameDesc, int monthCount, string importantIdsList, string importantIdsList2, string execUniqueId)
        {
            string fromDate, toDate, sqlStmt;
            fromDate = DateTime.Now.ToString("yyyy-MM-01");
            toDate = DateTime.Parse(fromDate).AddMonths(monthCount).AddDays(-1).ToString("yyyy-MM-dd");
            //fromDate = "2021-10-01";
            //toDate = "2022-01-31";
            Dictionary<string, List<ImportantDatesModelNew>> monthImportantDatesModels = new Dictionary<string, List<ImportantDatesModelNew>>();
            List<ImportantDatesModelNew> importantDatesModels;
            ImportantDatesModelNew importantDatesModel;
            for (int i = 0; i < monthCount; i++)
            {
                importantDatesModels = new List<ImportantDatesModelNew>();
                monthImportantDatesModels[DateTime.Parse(fromDate).AddMonths(i).ToString("yyyy-MM-01")] = importantDatesModels;
            }
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            sqlStmt = "";
            sqlStmt += "        SELECT *" + Environment.NewLine;
            sqlStmt += "          FROM ImportantDates" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN ImportantDatesDate" + Environment.NewLine;
            sqlStmt += "            ON ImportantDates.ImportantDatesId = ImportantDatesDate.ImportantDatesId" + Environment.NewLine;
            sqlStmt += "         WHERE ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.ImportantDatesId IN(" + importantIdsList + ")" + Environment.NewLine;
            sqlStmt += "           AND ImportantDatesDate.EventDate BETWEEN '" + fromDate + "' AND '" + toDate + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY ImportantDates.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventDate" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            string importantDatesId;
            while (sqlDataReaderRead)
            {
                importantDatesId = sqlDataReader["ImportantDatesId"].ToString();
                foreach (var monthImportantDatesModel in monthImportantDatesModels)
                {
                    importantDatesModels = monthImportantDatesModel.Value;
                    importantDatesModels.Add
                    (
                        new ImportantDatesModelNew
                        {
                            ImportantDatesId = long.Parse(sqlDataReader["ImportantDatesId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            EventName1 = sqlDataReader["EventName1"].ToString(),
                            EventName2 = sqlDataReader["EventName2"].ToString(),
                            EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                            EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                            FileName1 = sqlDataReader["FileName1"].ToString(),
                            Url1 = sqlDataReader["Url1"].ToString(),
                            ImageName1 = sqlDataReader["ImageName1"].ToString(),
                            ImageName2 = sqlDataReader["ImageName2"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            EventName3 = sqlDataReader["EventName3"].ToString(),
                            EventName4 = sqlDataReader["EventName4"].ToString(),
                            EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                            EventDesc4 = sqlDataReader["EventDesc4"].ToString(),
                            ImageName3 = sqlDataReader["ImageName3"].ToString(),
                            ImageName4 = sqlDataReader["ImageName4"].ToString(),
                            HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                            HtmlFileName4 = sqlDataReader["HtmlFileName4"].ToString(),
                            EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
                            SponsorshipGroupId = long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            ImportantDatesDateModels = new List<ImportantDatesDateModel>(),
                        }
                    );
                }
                while (sqlDataReaderRead && sqlDataReader["ImportantDatesId"].ToString() == importantDatesId)
                {
                    //Get the date
                    importantDatesModels = monthImportantDatesModels[sqlDataReader["EventDate"].ToString().Substring(0, 8) + "01"];
                    importantDatesModel = importantDatesModels.First(x => x.ImportantDatesId == long.Parse(sqlDataReader["ImportantDatesId"].ToString()));
                    importantDatesModel.ImportantDatesDateModels.Add
                    (
                        new ImportantDatesDateModel
                        {
                            ImportantDatesId = long.Parse(sqlDataReader["ImportantDatesId"].ToString()),
                            EventDate = sqlDataReader["EventDate"].ToString(),
                            EventTime = sqlDataReader["EventTime"].ToString(),
                            EventText1 = sqlDataReader["EventText1"].ToString(),
                            EventText2 = sqlDataReader["EventText2"].ToString(),
                            EventText3 = sqlDataReader["EventText3"].ToString(),
                            ImportantDatesDateId = long.Parse(sqlDataReader["ImportantDatesDateId"].ToString()),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            importantDatesModels = new List<ImportantDatesModelNew>();
            monthImportantDatesModels["9999-12-01"] = importantDatesModels;
            sqlStmt = "";
            sqlStmt += "        SELECT *" + Environment.NewLine;
            sqlStmt += "          FROM ImportantDates" + Environment.NewLine;
            sqlStmt += "         WHERE ImportantDates.ImportantDatesId IN(" + importantIdsList2 + ")" + Environment.NewLine;
            sqlStmt += "      ORDER BY ImportantDates.SeqNum" + Environment.NewLine;
            sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                importantDatesModels.Add
                (
                    new ImportantDatesModelNew
                    {
                        ImportantDatesId = long.Parse(sqlDataReader["ImportantDatesId"].ToString()),
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        EventName1 = sqlDataReader["EventName1"].ToString(),
                        EventName2 = sqlDataReader["EventName2"].ToString(),
                        EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                        EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                        FileName1 = sqlDataReader["FileName1"].ToString(),
                        Url1 = sqlDataReader["Url1"].ToString(),
                        ImageName1 = sqlDataReader["ImageName1"].ToString(),
                        ImageName2 = sqlDataReader["ImageName2"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        FinishTime = sqlDataReader["FinishTime"].ToString(),
                        EventName3 = sqlDataReader["EventName3"].ToString(),
                        EventName4 = sqlDataReader["EventName4"].ToString(),
                        EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                        EventDesc4 = sqlDataReader["EventDesc4"].ToString(),
                        ImageName3 = sqlDataReader["ImageName3"].ToString(),
                        ImageName4 = sqlDataReader["ImageName4"].ToString(),
                        HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                        HtmlFileName4 = sqlDataReader["HtmlFileName4"].ToString(),
                        EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
                        SponsorshipGroupId = long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                        ImportantDatesDateModels = new List<ImportantDatesDateModel>(),
                    }
                );
            }
            sqlDataReader.Close();
            for (int i = 0; i < importantDatesModels.Count % 5; i++)
            {
                importantDatesModels.Add(new ImportantDatesModelNew());
            }
            sqlConnection.Close();
            return monthImportantDatesModels;
        }
        public void GetImportantDates3(string locationNameDesc, CalendarModel templeEventsModels, int monthCount, string importantIdsList, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //string sqlStmt;
            //sqlStmt = "";
            //sqlStmt += "        SELECT ImportantDates.*" + Environment.NewLine;
            //sqlStmt += "          FROM ImportantDates" + Environment.NewLine;
            //sqlStmt += "         WHERE ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            //sqlStmt += "           AND ImportantDates.ImportantDatesId IN(" + importantIdsList + ")" + Environment.NewLine;
            //sqlStmt += "      ORDER BY ImportantDates.SeqNum" + Environment.NewLine;
            //string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            //SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            //sqlConnection.Open();
            //SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            templeEventsModels.ImportantDatesModelsList = new List<ImportantDatesModel>();
            //while (sqlDataReader.Read())
            //{
            //    templeEventsModels.ImportantDatesModelsList.Add
            //    (
            //        new ImportantDatesModel
            //        {
            //            ImportantDatesId = sqlDataReader["ImportantDatesId"].ToString(),
            //            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
            //            SeqNum = sqlDataReader["SeqNum"].ToString(),
            //            EventName1 = sqlDataReader["EventName1"].ToString(),
            //            EventName2 = sqlDataReader["EventName2"].ToString(),
            //            EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
            //            EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
            //            StartTime = sqlDataReader["StartTime"].ToString(),
            //            FinishTime = sqlDataReader["FinishTime"].ToString(),
            //            ImageName1 = sqlDataReader["ImageName1"].ToString(),
            //            ImageName2 = sqlDataReader["ImageName2"].ToString(),
            //            EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
            //        }
            //    );
            //}
            //sqlDataReader.Close();
            //sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
        public Dictionary<string, string> GetTodaysInfo(string locationNameDesc, string gregorianDate, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Dictionary<string, string> todaysInfos;
            todaysInfos = new Dictionary<string, string>();
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RiseSet WHERE LocationNameDesc = '" + locationNameDesc + "' AND GregorianDate = '" + gregorianDate + "'", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            todaysInfos["SunRise"] = sqlDataReader["SunRise"].ToString();
            todaysInfos["SunSet"] = sqlDataReader["SunSet"].ToString();
            todaysInfos["RKStart"] = sqlDataReader["RKStart"].ToString();
            todaysInfos["RKFinish"] = sqlDataReader["RKFinish"].ToString();
            todaysInfos["YGStart"] = sqlDataReader["YGStart"].ToString();
            todaysInfos["YGFinish"] = sqlDataReader["YGFinish"].ToString();
            sqlDataReader.Close();
            sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return todaysInfos;
        }
        public SnippetsModel Snippets(string locationNameDesc, string locationNameDesc1, string directoryName, long snippetsId)
        {
            StreamReader streamReader = new StreamReader(directoryName + "Snippets" + snippetsId + ".html");
            SnippetsModel snippetsModel = new SnippetsModel
            {
                SnippetsHtmlData = streamReader.ReadToEnd(),
            };
            streamReader.Close();
            return snippetsModel;
        }
        public void DeleteExceptionLog(string execUniqueId)
        {
            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("TRUNCATE TABLE ArchLib.ExceptionLog", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private string BuildSqlStmtCalendarData(string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string sqlStmt = "";

            sqlStmt += "        SELECT Calendar.StartDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartTime" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,Calendar.CalendarCodeId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM Calendar" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
            sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeType" + Environment.NewLine;
            sqlStmt += "            ON CodeData.CodeTypeId = CodeType.CodeTypeId" + Environment.NewLine;
            sqlStmt += "         WHERE Calendar.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND CodeType.CodeTypeNameDesc NOT IN('PANCHANGAM_KARANA')" + Environment.NewLine;
            sqlStmt += "           AND" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               @StartDate BETWEEN Calendar.StartDate AND Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "            OR @FinishDate BETWEEN Calendar.StartDate AND Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT Calendar.StartDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartTime" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,Calendar.CalendarCodeId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM Calendar" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
            sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeType" + Environment.NewLine;
            sqlStmt += "            ON CodeData.CodeTypeId = CodeType.CodeTypeId" + Environment.NewLine;
            sqlStmt += "         WHERE Calendar.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND CodeType.CodeTypeNameDesc NOT IN('PANCHANGAM_KARANA')" + Environment.NewLine;
            sqlStmt += "           AND" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               Calendar.StartDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "            OR Calendar.FinishDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.SunRise" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.SunSet" + Environment.NewLine;
            sqlStmt += "              ,9100 CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,'RISE_SET' AS CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RiseSetId" + Environment.NewLine;
            sqlStmt += "              ,9100 AS CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'Rise/Set' AS CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND GregorianDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RKStart" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RKFinish" + Environment.NewLine;
            sqlStmt += "              ,9200 CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,'RISE_SET' AS CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RiseSetId" + Environment.NewLine;
            sqlStmt += "              ,9200 AS CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'Rahu' AS CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND GregorianDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.YGStart" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.YGFinish" + Environment.NewLine;
            sqlStmt += "              ,9300 CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,'RISE_SET' AS CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RiseSetId" + Environment.NewLine;
            sqlStmt += "              ,9300 AS CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'Yama' AS CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND GregorianDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "      ORDER BY CodeType.CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartDate" + Environment.NewLine;

            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return sqlStmt;
        }
        private string BuildSqlStmtCalendarEventData(string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string sqlStmt = "";

            sqlStmt += "        SELECT CalendarEvent.CalendarEventId";
            sqlStmt += "              ,CalendarEvent.EventDate";
            sqlStmt += "              ,CalendarEvent.SeqNum";
            sqlStmt += "              ,CalendarEvent.EventText";
            sqlStmt += "          FROM CalendarEvent";
            sqlStmt += "         WHERE CalendarEvent.LocationNameDesc = @LocationNameDesc";
            sqlStmt += "           AND CalendarEvent.EventDate BETWEEN @StartDate AND @FinishDate";
            sqlStmt += "      ORDER BY CalendarEvent.EventDate";
            sqlStmt += "              ,CalendarEvent.SeqNum";

            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return sqlStmt;
        }
    }
}
