using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SVCCTempleWeb.ClassCode
{
    public partial class SVCCTempleBL
    {
        public void GenerateTempleEventData(string locationNameDesc, string startDate, string finishDate, string execUniqueId)
        {
            #region
            string sqlStmt = "";
            sqlStmt += "        SELECT TempleEventMaster.*" + Environment.NewLine;
            sqlStmt += "          FROM TempleEventMaster" + Environment.NewLine;
            sqlStmt += "         WHERE LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            #endregion
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnectionUpload = new SqlConnection(databaseConnectionString);
            sqlConnectionUpload.Open();
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlCommand = new SqlCommand("TRUNCATE TABLE TempleEventUpload", sqlConnectionUpload);
            sqlCommand.ExecuteNonQuery();
            sqlCommand = BuildSqlCommandTempleEventUpload(sqlConnectionUpload, execUniqueId);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            while (sqlDataReader.Read())
            {
                switch (long.Parse(sqlDataReader["TempleEventMasterId"].ToString()))
                {
                    case 0:
                        GenerateTempleEventTimingWeekday((int)DayOfWeek.Monday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        GenerateTempleEventTimingWeekday((int)DayOfWeek.Tuesday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        GenerateTempleEventTimingWeekday((int)DayOfWeek.Wednesday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        GenerateTempleEventTimingWeekday((int)DayOfWeek.Thursday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        GenerateTempleEventTimingWeekday((int)DayOfWeek.Friday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        break;
                    case 1:
                        GenerateTempleEventTimingWeekend((int)DayOfWeek.Sunday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        GenerateTempleEventTimingWeekend((int)DayOfWeek.Saturday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        break;
                    case 10:
                        GenerateTempleEventShirdiSaiThursday(DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        break;
                    case 11:
                        GenerateTempleEventShirdiSai(DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        break;
                    case 20:
                        GenerateTemplEventDataDaily(DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        break;
                    case 30:
                    case 31:
                    //case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        GenerateTemplEventDataWeekly(int.Parse(sqlDataReader["FrequencyCategoryNameDesc"].ToString()), DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        break;
                        //case 51:
                        //    GenerateTemplEventDataPoornima(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 52:
                        //    GenerateTemplEventDataSankatChath(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 53:
                        //    GenerateTemplEventDataSashti(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 54:
                        //    GenerateTemplEventDataAshtami(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 55:
                        //    GenerateTemplEventDataTrayodashi(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 56:
                        //    GenerateTemplEventDataSwati(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 60:
                        //    GenerateTemplEventDataAmavasya(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 61:
                        //    GenerateTemplEventDataLunarMonth(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 62:
                        //    GenerateTemplEventDataLunarSeason(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 63:
                        //    GenerateTemplEventDataSolarMonth(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 64:
                        //    GenerateTemplEventDataEkadashi(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 2:
                        //case 3:
                        //case 4:
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Monday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Tuesday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Wednesday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Thursday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Friday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    break;
                        //case 5:
                        //case 6:
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Sunday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    GenerateTemplEventDataWeekly((int)DayOfWeek.Saturday, DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    break;
                        //case 7:
                        //case 8:
                        //case 9:
                        //case 10:
                        //    GenerateTemplEventDataDaily(DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    break;
                        //case 11:
                        //case 12:
                        //case 13:
                        //case 14:
                        //case 15:
                        //case 16:
                        //case 17:
                        //case 18:
                        //case 19:
                        //case 20:
                        //case 21:
                        //case 22:
                        //    GenerateTemplEventDataWeekly(int.Parse(sqlDataReader["FrequencyCategoryNameDesc"].ToString()), DateTime.Parse(startDate), DateTime.Parse(finishDate), sqlDataReader, sqlCommand, execUniqueId);
                        //    break;
                        //case 24:
                        //    GenerateTemplEventDataPoornima(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 25:
                        //    GenerateTemplEventDataSankatChath(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 26:
                        //    GenerateTemplEventDataSashti(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                        //case 27:
                        //    GenerateTemplEventDataAshtami(locationNameDesc, sqlCommand, execUniqueId);
                        //    break;
                }
            }
            sqlDataReader.Close();
            sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartTime" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,*" + Environment.NewLine;
            sqlStmt += "          FROM Calendar" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
            sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN TempleEventMaster" + Environment.NewLine;
            sqlStmt += "            ON TempleEventMaster.CalendarCodeId LIKE '%,' + CAST(CodeData.CodeDataId AS VARCHAR(50)) + ',%'" + Environment.NewLine;
            sqlStmt += "         WHERE Calendar.StartDate BETWEEN '" + startDate + "' AND '" + finishDate + "'" + Environment.NewLine;
            sqlStmt += "           AND Calendar.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY" + Environment.NewLine;
            sqlStmt += "               TempleEventMaster.TempleEventMasterId" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartTime" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishTime" + Environment.NewLine;
            //sqlStmt += "" + Environment.NewLine;
            //sqlStmt += "" + Environment.NewLine;
            //sqlStmt += "" + Environment.NewLine;
            sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlCommand = BuildSqlCommandTempleEventUpload(sqlConnectionUpload, execUniqueId);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            while (sqlDataReader.Read())
            {
                sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                sqlCommand.Parameters["@StartDate"].Value = sqlDataReader["StartDate"].ToString();
                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(sqlDataReader["StartDate"].ToString() + " " + sqlDataReader["StartTime"].ToString()).ToString("ddd MMM dd yyyy hh:mm tt");
                switch (sqlDataReader["TempleEventMasterId"].ToString())
                {
                    case "51":
                    case "61":
                    case "62":
                    case "63":
                    case "64":
                        sqlCommand.Parameters["@EventText2"].Value = "Start of " + sqlDataReader["CodeDataDesc0"].ToString();
                        break;
                    default:
                        sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                        break;
                }
                sqlCommand.ExecuteNonQuery();
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            sqlConnectionUpload.Close();
        }
        public void GenerateTempleScheduleUploadData(string locationNameDesc, string locationNameDesc1, string startDate, string finishDate, string dataType, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;//, sqlConnectionUpload = null;
            try
            {
                string yearParm = startDate.Substring(0, 4);
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("TRUNCATE TABLE TempleScheduleUpload", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT TempleScheduleUpload(LocationNameDesc, ScheduleMasterId, StartDate, EventText1, EventText2, CalendarId)" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT" + Environment.NewLine;
                sqlStmt += "               TempleScheduleMaster.LocationNameDesc" + Environment.NewLine;
                sqlStmt += "              ,TempleScheduleMaster.ScheduleMasterId" + Environment.NewLine;
                sqlStmt += "              ,Calendar.StartDate" + Environment.NewLine;
                sqlStmt += "              ,NULL --CodeData.CodeDataDesc0 AS EventText1" + Environment.NewLine;
                sqlStmt += "              ,NULL --CodeData.CodeDataDesc1 AS EventText2" + Environment.NewLine;
                sqlStmt += "              ,Calendar.CalendarId" + Environment.NewLine;
                sqlStmt += "          FROM TempleScheduleMaster" + Environment.NewLine;
                sqlStmt += "    INNER JOIN Calendar" + Environment.NewLine;
                sqlStmt += "            ON TempleScheduleMaster.CalendarCodeId = Calendar.CalendarCodeId" + Environment.NewLine;
                sqlStmt += "           AND TempleScheduleMaster.LocationNameDesc = Calendar.LocationNameDesc" + Environment.NewLine;
                sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
                sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN CodeType" + Environment.NewLine;
                sqlStmt += "            ON CodeData.CodeTypeId = CodeType.CodeTypeId" + Environment.NewLine;
                sqlStmt += "         WHERE TempleScheduleMaster.TempleScheduleMasterId BETWEEN 50 AND 99" + Environment.NewLine;
                sqlStmt += $"           AND Calendar.StartDate BETWEEN '{startDate}' AND '{finishDate}'" + Environment.NewLine;
                sqlStmt += "           AND TempleScheduleMaster.LocationNameDesc = 'FREMONT'" + Environment.NewLine;
                sqlStmt += "      ORDER BY TempleScheduleMaster.ScheduleMasterId" + Environment.NewLine;
                sqlStmt += "              ,Calendar.StartDate" + Environment.NewLine;
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                #endregion
                #region
                sqlStmt = "";
                sqlStmt += "        INSERT TempleScheduleUpload(LocationNameDesc, ScheduleMasterId, StartDate, FinishDate)" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT" + Environment.NewLine;
                sqlStmt += "               TempleScheduleMaster.LocationNameDesc" + Environment.NewLine;
                sqlStmt += "              ,TempleScheduleMaster.ScheduleMasterId" + Environment.NewLine;
                sqlStmt += "              ,SampleDateStart AS StartDate" + Environment.NewLine;
                sqlStmt += "              ,SampleDateFinish AS FinishDate" + Environment.NewLine;
                sqlStmt += "          FROM TempleScheduleMaster" + Environment.NewLine;
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                #endregion
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void GenerateTempleEventDataSacramento(string locationNameDesc, string startDate, string finishDate, bool deploy, string execUniqueId)
        {
            DateTime startDateTime, finishDateTime;
            startDateTime = DateTime.Parse(startDate);
            finishDateTime = DateTime.Parse(finishDate);
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("TRUNCATE TABLE TempleInfoUpload", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlCommand = TempleInfoUploadSqlCommandInsert(sqlConnection);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            while (startDateTime <= finishDateTime)
            //while (long.Parse(finishDateTime.ToString("yyyyMMdd")) < long.Parse(startDateTime.ToString("yyyyMMdd")))
            {
                //Daily Shiva Abhishekam
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5100;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 1;
                sqlCommand.Parameters["@StartDate"].Value = startDateTime.ToString("yyyy-MM-dd");
                sqlCommand.Parameters["@StartTime"].Value = "09:30";
                sqlCommand.Parameters["@InfoText1"].Value = "Gangadhareshwara (Shiva) Abhishekam";
                sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1077;
                sqlCommand.ExecuteNonQuery();
                //Weekly Events
                switch (startDateTime.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5051;
                        sqlCommand.Parameters["@SeqNum"].Value = 2;
                        sqlCommand.Parameters["@StartTime"].Value = "10:30";
                        sqlCommand.Parameters["@InfoText1"].Value = "Ganapathy Abhishekam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1035;
                        sqlCommand.ExecuteNonQuery();
                        break;
                    case DayOfWeek.Monday:
                        break;
                    case DayOfWeek.Tuesday:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5052;
                        sqlCommand.Parameters["@SeqNum"].Value = 3;
                        sqlCommand.Parameters["@StartTime"].Value = "19:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Gakara Ganapathy Sahasranamam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1035;
                        sqlCommand.ExecuteNonQuery();
                        break;
                    case DayOfWeek.Thursday:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5101;
                        sqlCommand.Parameters["@SeqNum"].Value = 4;
                        sqlCommand.Parameters["@StartTime"].Value = "19:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Shirdi Sai Aarthi";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1078;
                        sqlCommand.ExecuteNonQuery();
                        break;
                    case DayOfWeek.Friday:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5053;
                        sqlCommand.Parameters["@SeqNum"].Value = 5;
                        sqlCommand.Parameters["@StartTime"].Value = "10:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Saraswathi Abhishekam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1037;
                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5054;
                        sqlCommand.Parameters["@SeqNum"].Value = 6;
                        sqlCommand.Parameters["@StartTime"].Value = "10:30";
                        sqlCommand.Parameters["@InfoText1"].Value = "Lakshmi Abhishekam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1038;
                        sqlCommand.ExecuteNonQuery();
                        break;
                    case DayOfWeek.Saturday:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5055;
                        sqlCommand.Parameters["@SeqNum"].Value = 7;
                        sqlCommand.Parameters["@StartTime"].Value = "09:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Venkateswara Suprabatham";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5056;
                        sqlCommand.Parameters["@SeqNum"].Value = 8;
                        sqlCommand.Parameters["@StartTime"].Value = "10:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Navagraha Abhishekam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1039;
                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5057;
                        sqlCommand.Parameters["@SeqNum"].Value = 9;
                        sqlCommand.Parameters["@StartTime"].Value = "11:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Venkateshwara Abhishekam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1040;
                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5058;
                        sqlCommand.Parameters["@SeqNum"].Value = 10;
                        sqlCommand.Parameters["@StartTime"].Value = "17:00";
                        sqlCommand.Parameters["@InfoText1"].Value = "Aanjaneya (Hanuman) Abhishekam";
                        sqlCommand.Parameters["@InfoText2"].Value = DBNull.Value;
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1041;
                        sqlCommand.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }
                //Temple Timings
                switch (startDateTime.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5000;
                        sqlCommand.Parameters["@SeqNum"].Value = 90;
                        sqlCommand.Parameters["@StartTime"].Value = "99:99";
                        sqlCommand.Parameters["@InfoText1"].Value = "Temple Timings";
                        sqlCommand.Parameters["@InfoText2"].Value = "9am - 8pm";
                        sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                        sqlCommand.ExecuteNonQuery();
                        break;
                    default:
                        sqlCommand.Parameters["@ImportantDatesId"].Value = 5000;
                        sqlCommand.Parameters["@SeqNum"].Value = 90;
                        sqlCommand.Parameters["@StartTime"].Value = "99:99";
                        sqlCommand.Parameters["@InfoText1"].Value = "Temple Timings";
                        sqlCommand.Parameters["@InfoText2"].Value = "9am - 12pm";
                        sqlCommand.Parameters["@InfoText3"].Value = "6pm - 8:30pm";
                        sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                        sqlCommand.ExecuteNonQuery();
                        break;
                }
                startDateTime = startDateTime.AddDays(1);
            }
            List<string> templeInfoStartDates;
            //Poornima
            templeInfoStartDates = new List<string>
            {
                "2023-05-04",
                "2023-06-03",
                "2023-07-02",
                "2023-07-31",
                "2023-08-30",
                "2023-09-28",
                "2023-10-27",
                "2023-11-26",
                "2023-12-25",
            };
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5001;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 12;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = "18:30";
                sqlCommand.Parameters["@InfoText1"].Value = "Poornima";
                sqlCommand.Parameters["@InfoText2"].Value = "Satyanarayana Pooja";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1031;
                sqlCommand.ExecuteNonQuery();
            }
            //Sankat Chathurthi
            templeInfoStartDates = new List<string>
            {
                "2023-05-08",
                "2023-06-06",
                "2023-07-05",
                "2023-08-04",
                "2023-09-10",
                "2023-10-01",
                "2023-11-30",
                "2023-12-30",
            };
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5002;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 13;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                if (DateTime.Parse(templeInfoStartDate).DayOfWeek == DayOfWeek.Saturday || DateTime.Parse(templeInfoStartDate).DayOfWeek == DayOfWeek.Sunday)
                {
                    sqlCommand.Parameters["@StartTime"].Value = "07:30";
                    sqlCommand.Parameters["@InfoText1"].Value = "Ganapathy Homam";
                    sqlCommand.Parameters["@InfoText2"].Value = "Sankashtahara Chathurthi";
                    sqlCommand.Parameters["@InfoText3"].Value = "Chathurthi after New Moon";
                    sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    sqlCommand.Parameters["@StartTime"].Value = "06:30";
                    sqlCommand.Parameters["@InfoText1"].Value = "Ganapathy Homam";
                    sqlCommand.Parameters["@InfoText2"].Value = "Sankashtahara Chathurthi";
                    sqlCommand.Parameters["@InfoText3"].Value = "Chathurthi after New Moon";
                    sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
                sqlCommand.Parameters["@SeqNum"].Value = 14;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = "18:30";
                sqlCommand.Parameters["@InfoText1"].Value = "Ganapathy Abhishekam";
                sqlCommand.Parameters["@InfoText2"].Value = "Sankashtahara Chathurthi";
                sqlCommand.Parameters["@InfoText3"].Value = "Chathurthi after New Moon";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = 1032;
                sqlCommand.ExecuteNonQuery();
            }
            //Sashti
            templeInfoStartDates = new List<string>
            {
                "2023-05-10",
                "2023-05-24",
                "2023-06-08",
                "2023-06-23",
                "2023-07-07",
                "2023-07-24",
                "2023-08-05",
                "2023-08-21",
                "2023-09-04",
                "2023-09-20",
                "2023-10-03",
                "2023-10-19",
                "2023-11-02",
                "2023-11-17",
                "2023-12-02",
                "2023-12-17",
            };
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5005;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 15;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = "18:30";
                sqlCommand.Parameters["@InfoText1"].Value = "Sashti - Shukla & Krishna";
                sqlCommand.Parameters["@InfoText2"].Value = "Subhramanya Abhishekam";
                sqlCommand.Parameters["@InfoText3"].Value = DBNull.Value;
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
            //Pradosham
            templeInfoStartDates = new List<string>
            {
                "2023-05-02",
                "2023-05-16",
                "2023-06-01",
                "2023-06-15",
                "2023-06-30",
                "2023-07-14",
                "2023-07-30",
                "2023-08-13",
                "2023-08-28",
                "2023-09-11",
                "2023-09-26",
                "2023-10-11",
                "2023-10-26",
                "2023-11-11",
                "2023-11-24",
                "2023-12-09",
                "2023-12-23",
            };
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5006;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 16;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = "18:30";
                sqlCommand.Parameters["@InfoText1"].Value = "Pradosham - Shukla & Krishna";
                sqlCommand.Parameters["@InfoText2"].Value = "Shiva Abhishekam";
                sqlCommand.Parameters["@InfoText3"].Value = "Trayodashi";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
            List<string> templeInfoStartTimes;
            //Amavasya
            string sqlStmt = $"SELECT * FROM Calendar WHERE LocationNameDesc = 'SACRAMENTO' AND StartDate BETWEEN '{startDate}' AND '{finishDate}' AND CalendarCodeId = 306";
            templeInfoStartDates = new List<string>();
            templeInfoStartTimes = new List<string>();
            SqlCommand sqlCommand1 = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoStartDates.Add(sqlDataReader["StartDate"].ToString());
                templeInfoStartTimes.Add(sqlDataReader["StartTime"].ToString());
            }
            sqlDataReader.Close();
            int i = -1;
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                i++;
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5007;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 17;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = templeInfoStartTimes[i];
                sqlCommand.Parameters["@InfoText1"].Value = "Start of Amavasya";
                sqlCommand.Parameters["@InfoText2"].Value = "Info Only";
                sqlCommand.Parameters["@InfoText3"].Value = "No event at the temple";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
            //Lunar Month
            sqlStmt = $"SELECT * FROM Calendar WHERE LocationNameDesc = 'SACRAMENTO' AND StartDate BETWEEN '{startDate}' AND '{finishDate}' AND CalendarCodeId = 277";
            templeInfoStartDates = new List<string>();
            templeInfoStartTimes = new List<string>();
            sqlCommand1 = new SqlCommand(sqlStmt, sqlConnection);
            sqlDataReader = sqlCommand1.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoStartDates.Add(sqlDataReader["StartDate"].ToString());
                templeInfoStartTimes.Add(sqlDataReader["StartTime"].ToString());
            }
            sqlDataReader.Close();
            i = -1;
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                i++;
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5008;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 18;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = templeInfoStartTimes[i];
                sqlCommand.Parameters["@InfoText1"].Value = "Start of Lunar Month";
                sqlCommand.Parameters["@InfoText2"].Value = "Info Only";
                sqlCommand.Parameters["@InfoText3"].Value = "No event at the temple";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
            //Ekadashi
            sqlStmt = $"SELECT * FROM Calendar WHERE LocationNameDesc = 'SACRAMENTO' AND StartDate BETWEEN '{startDate}' AND '{finishDate}' AND CalendarCodeId IN(287, 302)";
            templeInfoStartDates = new List<string>();
            templeInfoStartTimes = new List<string>();
            sqlCommand1 = new SqlCommand(sqlStmt, sqlConnection);
            sqlDataReader = sqlCommand1.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoStartDates.Add(sqlDataReader["StartDate"].ToString());
                templeInfoStartTimes.Add(sqlDataReader["StartTime"].ToString());
            }
            sqlDataReader.Close();
            i = -1;
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                i++;
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5102;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 19;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = templeInfoStartTimes[i];
                sqlCommand.Parameters["@InfoText1"].Value = "Start of Ekadashi";
                sqlCommand.Parameters["@InfoText2"].Value = "Info Only";
                sqlCommand.Parameters["@InfoText3"].Value = "No event at the temple";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
            //Solar Month
            sqlStmt = $"SELECT * FROM Calendar WHERE LocationNameDesc = 'SACRAMENTO' AND StartDate BETWEEN '{startDate}' AND '{finishDate}' AND CalendarCodeId BETWEEN 265 AND 276";
            templeInfoStartDates = new List<string>();
            templeInfoStartTimes = new List<string>();
            sqlCommand1 = new SqlCommand(sqlStmt, sqlConnection);
            sqlDataReader = sqlCommand1.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoStartDates.Add(sqlDataReader["StartDate"].ToString());
                templeInfoStartTimes.Add(sqlDataReader["StartTime"].ToString());
            }
            sqlDataReader.Close();
            i = -1;
            foreach (var templeInfoStartDate in templeInfoStartDates)
            {
                i++;
                sqlCommand.Parameters["@ImportantDatesId"].Value = 5009;
                sqlCommand.Parameters["@InfoType"].Value = "HOMEPAGE";
                sqlCommand.Parameters["@SeqNum"].Value = 20;
                sqlCommand.Parameters["@StartDate"].Value = templeInfoStartDate;
                sqlCommand.Parameters["@StartTime"].Value = templeInfoStartTimes[i];
                sqlCommand.Parameters["@InfoText1"].Value = "Start of Solar Month";
                sqlCommand.Parameters["@InfoText2"].Value = "Info Only";
                sqlCommand.Parameters["@InfoText3"].Value = "No event at the temple";
                sqlCommand.Parameters["@SponsorshipGroupId"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
            if (deploy)
            {
                sqlStmt = $"DELETE TempleInfo WHERE StartDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}'";
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlStmt = "INSERT TempleInfo([LocationNameDesc], [ImportantDatesId], [InfoType], [SeqNum], [StartDate], [StartTime], [FinishDate], [FinishTime], [InfoText1], [InfoText2], [InfoText3], [HtmlFileName1], [ImageName1], [SponsorshipGroupId])";
                sqlStmt += "SELECT [LocationNameDesc], [ImportantDatesId], [InfoType], [SeqNum], [StartDate], [StartTime], [FinishDate], [FinishTime], [InfoText1], [InfoText2], [InfoText3], [HtmlFileName1], [ImageName1], [SponsorshipGroupId] FROM [TempleInfoUpload]";
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
        private SqlCommand TempleInfoUploadSqlCommandInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT TempleInfoUpload" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "                LocationNameDesc" + Environment.NewLine;
            sqlStmt += "               ,ImportantDatesId" + Environment.NewLine;
            sqlStmt += "               ,InfoType" + Environment.NewLine;
            sqlStmt += "               ,SeqNum" + Environment.NewLine;
            sqlStmt += "               ,StartDate" + Environment.NewLine;
            sqlStmt += "               ,StartTime" + Environment.NewLine;
            sqlStmt += "               ,InfoText1" + Environment.NewLine;
            sqlStmt += "               ,InfoText2" + Environment.NewLine;
            sqlStmt += "               ,InfoText3" + Environment.NewLine;
            sqlStmt += "               ,SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "                @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "               ,@ImportantDatesId" + Environment.NewLine;
            sqlStmt += "               ,@InfoType" + Environment.NewLine;
            sqlStmt += "               ,@SeqNum" + Environment.NewLine;
            sqlStmt += "               ,@StartDate" + Environment.NewLine;
            sqlStmt += "               ,@StartTime" + Environment.NewLine;
            sqlStmt += "               ,@InfoText1" + Environment.NewLine;
            sqlStmt += "               ,@InfoText2" + Environment.NewLine;
            sqlStmt += "               ,@InfoText3" + Environment.NewLine;
            sqlStmt += "               ,@SponsorshipGroupId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ImportantDatesId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@InfoType", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@StartTime", SqlDbType.NVarChar, 5);
            sqlCommand.Parameters.Add("@InfoText1", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@InfoText2", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@InfoText3", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@SponsorshipGroupId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private void GenerateTempleEventTimingWeekday(int dayOfWeek, DateTime startDate, DateTime finishDate, SqlDataReader sqlDataReader, SqlCommand sqlCommand, string execUniqueId)
        {
            int dayOfWeekTemp;
            string eventText1;
            for (; startDate <= finishDate; startDate = startDate.AddDays(1))
            {
                dayOfWeekTemp = (int)startDate.DayOfWeek;
                if (dayOfWeek == dayOfWeekTemp)
                {
                    eventText1 = "";
                    eventText1 += DateTime.Parse(sqlDataReader["EventName2"].ToString()).ToString("h:mm tt") + " - ";
                    eventText1 += DateTime.Parse(sqlDataReader["EventName3"].ToString()).ToString("h:mm tt") + Environment.NewLine;
                    eventText1 += DateTime.Parse(sqlDataReader["EventDesc1"].ToString()).ToString("h:mm tt") + " - ";
                    eventText1 += DateTime.Parse(sqlDataReader["EventDesc2"].ToString()).ToString("h:mm tt");
                    sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                    sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
                    sqlCommand.Parameters["@EventText1"].Value = eventText1;
                    sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void GenerateTempleEventTimingWeekend(int dayOfWeek, DateTime startDate, DateTime finishDate, SqlDataReader sqlDataReader, SqlCommand sqlCommand, string execUniqueId)
        {
            int dayOfWeekTemp;
            string eventText1;
            for (; startDate <= finishDate; startDate = startDate.AddDays(1))
            {
                dayOfWeekTemp = (int)startDate.DayOfWeek;
                if (dayOfWeek == dayOfWeekTemp)
                {
                    eventText1 = "";
                    eventText1 += DateTime.Parse(sqlDataReader["EventName2"].ToString()).ToString("h:mm tt") + " - ";
                    eventText1 += DateTime.Parse(sqlDataReader["EventName3"].ToString()).ToString("h:mm tt");
                    sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                    sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
                    sqlCommand.Parameters["@EventText1"].Value = eventText1;
                    sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void GenerateTempleEventShirdiSaiThursday(DateTime startDate, DateTime finishDate, SqlDataReader sqlDataReader, SqlCommand sqlCommand, string execUniqueId)
        {
            int dayOfWeekTemp;
            string eventText1;
            for (; startDate <= finishDate; startDate = startDate.AddDays(1))
            {
                dayOfWeekTemp = (int)startDate.DayOfWeek;
                if ((int)DayOfWeek.Thursday == dayOfWeekTemp)
                {
                    eventText1 = "";
                    eventText1 += DateTime.Parse(sqlDataReader["EventName2"].ToString()).ToString("h:mm tt") + Environment.NewLine;
                    eventText1 += DateTime.Parse(sqlDataReader["EventName3"].ToString()).ToString("h:mm tt") + Environment.NewLine;
                    eventText1 += DateTime.Parse(sqlDataReader["EventDesc1"].ToString()).ToString("h:mm tt");// + Environment.NewLine;
                    //eventText1 += DateTime.Parse(sqlDataReader["EventDesc2"].ToString()).ToString("h:mm tt");
                    sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                    sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
                    sqlCommand.Parameters["@EventText1"].Value = eventText1;
                    sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void GenerateTempleEventShirdiSai(DateTime startDate, DateTime finishDate, SqlDataReader sqlDataReader, SqlCommand sqlCommand, string execUniqueId)
        {
            int dayOfWeekTemp;
            string eventText1;
            for (; startDate <= finishDate; startDate = startDate.AddDays(1))
            {
                dayOfWeekTemp = (int)startDate.DayOfWeek;
                if ((int)DayOfWeek.Thursday != dayOfWeekTemp)
                {
                    eventText1 = "";
                    eventText1 += DateTime.Parse(sqlDataReader["EventName2"].ToString()).ToString("h:mm tt") + Environment.NewLine;
                    eventText1 += sqlDataReader["EventName3"].ToString() == "" ? "" : DateTime.Parse(sqlDataReader["EventName3"].ToString()).ToString("h:mm tt") + Environment.NewLine;
                    eventText1 += sqlDataReader["EventDesc1"].ToString() == "" ? "" : DateTime.Parse(sqlDataReader["EventDesc1"].ToString()).ToString("h:mm tt");
                    sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                    sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
                    sqlCommand.Parameters["@EventText1"].Value = eventText1;
                    sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void GenerateTemplEventDataDaily(DateTime startDate, DateTime finishDate, SqlDataReader sqlDataReader, SqlCommand sqlCommand, string execUniqueId)
        {
            for (; startDate <= finishDate; startDate = startDate.AddDays(1))
            {
                sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
                sqlCommand.Parameters["@EventText1"].Value = DBNull.Value;
                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                sqlCommand.ExecuteNonQuery();
            }
        }
        private void GenerateTemplEventDataWeekly(int dayOfWeek, DateTime startDate, DateTime finishDate, SqlDataReader sqlDataReader, SqlCommand sqlCommand, string execUniqueId)
        {
            int dayOfWeekTemp;
            for (; startDate <= finishDate; startDate = startDate.AddDays(1))
            {
                dayOfWeekTemp = (int)startDate.DayOfWeek;
                if (dayOfWeek == dayOfWeekTemp)
                {
                    sqlCommand.Parameters["@TempleEventMasterId"].Value = sqlDataReader["TempleEventMasterId"].ToString();
                    sqlCommand.Parameters["@StartDate"].Value = startDate.ToString("yyyy-MM-dd");
                    sqlCommand.Parameters["@EventText1"].Value = DBNull.Value;
                    sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        //private void GenerateTemplEventDataPoornima(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "51";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataSankatChath(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-10",
        //            "2023-02-08",
        //            "2023-03-10",
        //            "2023-04-09",
        //            "2023-05-08",
        //            "2023-06-06",
        //            "2023-07-05",
        //            "2023-08-04",
        //            "2023-09-02",
        //            "2023-10-02",
        //            "2023-10-31",
        //            "2023-11-30",
        //            "2023-12-30",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-09 22:39",
        //            "2023-02-08 16:53",
        //            "2023-03-10 08:12",
        //            "2023-04-08 21:05",
        //            "2023-05-08 05:49",
        //            "2023-06-06 12:20",
        //            "2023-07-05 18:00",
        //            "2023-08-04 00:15",
        //            "2023-09-02 08:19",
        //            "2023-10-01 19:06",
        //            "2023-10-31 09:00",
        //            "2023-11-30 00:55",
        //            "2023-12-29 20:14",
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "52";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataSashti(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-12",
        //            "2023-01-26",
        //            "2023-02-11",
        //            "2023-02-24",
        //            "2023-03-12",
        //            "2023-03-26",
        //            "2023-04-11",
        //            "2023-04-25",
        //            "2023-05-10",
        //            "2023-05-24",
        //            "2023-06-08",
        //            "2023-06-23",
        //            "2023-07-07",
        //            "2023-07-23",
        //            "2023-08-06",
        //            "2023-08-21",
        //            "2023-09-04",
        //            "2023-09-20",
        //            "2023-10-04",
        //            "2023-10-20",
        //            "2023-11-02",
        //            "2023-11-18",
        //            "2023-12-02",
        //            "2023-12-17",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-12 03:07",
        //            "2023-01-25 20:58",
        //            "2023-02-10 19:38",
        //            "2023-02-24 11:01",
        //            "2023-03-12 09:31",
        //            "2023-03-26 04:03",
        //            "2023-04-10 18:48",
        //            "2023-04-24 21:10",
        //            "2023-05-10 01:19",
        //            "2023-05-24 14:31",
        //            "2023-06-08 06:28",
        //            "2023-06-23 07:24",
        //            "2023-07-07 11:47",
        //            "2023-07-22 23:15",
        //            "2023-08-05 18:40",
        //            "2023-08-21 13:30",
        //            "2023-09-04 04:12",
        //            "2023-09-20 01:46",
        //            "2023-10-03 17:03",
        //            "2023-10-19 12:01",
        //            "2023-11-02 09:22",
        //            "2023-11-17 19:48",
        //            "2023-12-02 03:44",
        //            "2023-12-17 04:03",
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "53";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataAshtami(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-14",
        //            "2023-01-28",
        //            "2023-02-13",
        //            "2023-02-26",
        //            "2023-03-14",
        //            "2023-03-28",
        //            "2023-04-12",
        //            "2023-04-27",
        //            "2023-05-12",
        //            "2023-05-27",
        //            "2023-06-10",
        //            "2023-06-25",
        //            "2023-07-09",
        //            "2023-07-25",
        //            "2023-08-07",
        //            "2023-08-23",
        //            "2023-09-06",
        //            "2023-09-22",
        //            "2023-10-06",
        //            "2023-10-21",
        //            "2023-11-04",
        //            "2023-11-19",
        //            "2023-12-04",
        //            "2023-12-19",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-14 05:53",
        //            "2023-01-27 19:13",
        //            "2023-02-12 20:16",
        //            "2023-02-26 11:29",
        //            "2023-03-14 07:52",
        //            "2023-03-28 06:32",
        //            "2023-04-12 15:14",
        //            "2023-04-27 01:09",
        //            "2023-05-11 20:36",
        //            "2023-05-26 19:12",
        //            "2023-06-10 01:32",
        //            "2023-06-25 11:55",
        //            "2023-07-09 07:29",
        //            "2023-07-25 02:38",
        //            "2023-08-07 15:44",
        //            "2023-08-23 15:01",
        //            "2023-09-06 03:07",
        //            "2023-09-22 01:05",
        //            "2023-10-05 18:05",
        //            "2023-10-21 09:23",
        //            "2023-11-04 12:29",
        //            "2023-11-19 15:52",
        //            "2023-12-04 08:29",
        //            "2023-12-18 23:37",
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "54";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataTrayodashi(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-03",
        //            "2023-01-18",
        //            "2023-02-02",
        //            "2023-02-17",
        //            "2023-03-03",
        //            "2023-03-18",
        //            "2023-04-02",
        //            "2023-04-17",
        //            "2023-05-02",
        //            "2023-05-16",
        //            "2023-06-01",
        //            "2023-06-14",
        //            "2023-06-30",
        //            "2023-07-14",
        //            "2023-07-29",
        //            "2023-08-12",
        //            "2023-08-28",
        //            "2023-09-11",
        //            "2023-09-26",
        //            "2023-10-11",
        //            "2023-10-25",
        //            "2023-11-09",
        //            "2023-11-24",
        //            "2023-12-09",
        //            "2023-12-23",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-03 08:32",
        //            "2023-01-18 23:48",
        //            "2023-02-02 02:56",
        //            "2023-02-17 10:06",
        //            "2023-03-03 22:13",
        //            "2023-03-18 19:37",
        //            "2023-04-02 17:54",
        //            "2023-04-17 03:16",
        //            "2023-05-02 10:48",
        //            "2023-05-16 11:06",
        //            "2023-06-01 01:09",
        //            "2023-06-14 20:02",
        //            "2023-06-30 12:47",
        //            "2023-07-14 06:47",
        //            "2023-07-29 22:04",
        //            "2023-08-12 19:50",
        //            "2023-08-28 05:53",
        //            "2023-09-11 11:22",
        //            "2023-09-26 13:15",
        //            "2023-10-11 05:07",
        //            "2023-10-25 21:14",
        //            "2023-11-09 23:05",
        //            "2023-11-24 05:36",
        //            "2023-12-09 17:43",
        //            "2023-12-23 16:54",
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "55";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataSwati(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-15",
        //            "2023-02-11",
        //            "2023-03-11",
        //            "2023-04-07",
        //            "2023-05-04",
        //            "2023-06-01",
        //            "2023-06-28",
        //            "2023-07-25",
        //            "2023-08-22",
        //            "2023-09-18",
        //            "2023-10-15",
        //            "2023-11-11",
        //            "2023-12-09",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-15 05:41",
        //            "2023-02-11 12:09",
        //            "2023-03-10 17:41",
        //            "2023-04-07 01:03",
        //            "2023-05-04 09:05",
        //            "2023-05-31 18:18",
        //            "2023-06-28 03:30",
        //            "2023-07-25 11:33",
        //            "2023-08-21 18:01",
        //            "2023-09-17 23:37",
        //            "2023-10-15 05:42",
        //            "2023-11-11 12:16",
        //            "2023-12-08 21:13",
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "56";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataAmavasya(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-20",
        //            "2023-02-19",
        //            "2023-03-21",
        //            "2023-04-19",
        //            "2023-05-19",
        //            "2023-06-17",
        //            "2023-07-17",
        //            "2023-08-15",
        //            "2023-09-14",
        //            "2023-10-14",
        //            "2023-11-12",
        //            "2023-12-12",
        //            "",
        //            "",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-20 16:47",
        //            "2023-02-19 02:48",
        //            "2023-03-20 13:17",
        //            "2023-04-18 22:53",
        //            "2023-05-18 09:13",
        //            "2023-06-16 20:41",
        //            "2023-07-16 09:38",
        //            "2023-08-15 00:13",
        //            "2023-09-13 16:19",
        //            "2023-10-13 09:21",
        //            "2023-11-12 01:15",
        //            "2023-12-11 16:54",
        //            "",
        //            ""
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "60";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataLunarMonth(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //            "2023-01-21",
        //            "2023-02-19",
        //            "2023-03-21",
        //            "2023-04-19",
        //            "2023-05-19",
        //            "2023-06-17",
        //            "2023-07-17",
        //            "2023-08-16",
        //            "2023-09-14",
        //            "2023-10-14",
        //            "2023-11-13",
        //            "2023-12-12",
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //            "2023-01-21 12:53",
        //            "2023-02-19 23:05",
        //            "2023-03-21 10:22",
        //            "2023-04-19 21:12",
        //            "2023-05-19 08:53",
        //            "2023-06-17 21:36",
        //            "2023-07-17 11:31",
        //            "2023-08-16 02:38",
        //            "2023-09-14 18:39",
        //            "2023-10-14 10:54",
        //            "2023-11-13 01:27",
        //            "2023-12-12 15:31",
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "61";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataLunarSeason(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "62";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataSolarMonth(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "63";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        //private void GenerateTemplEventDataEkadashi(string locationNameDesc, SqlCommand sqlCommand, string execUniqueId)
        //{
        //    if (locationNameDesc == "FREMONT")
        //    {
        //        string[] eventDates =
        //        {
        //        };
        //        string[] eventStartDateTimes =
        //        {
        //        };
        //        for (int i = 0; i < eventDates.Length; i++)
        //        {
        //            if (eventDates[i] != "")
        //            {
        //                sqlCommand.Parameters["@TempleEventMasterId"].Value = "64";
        //                sqlCommand.Parameters["@StartDate"].Value = eventDates[i];
        //                sqlCommand.Parameters["@EventText1"].Value = DateTime.Parse(eventStartDateTimes[i]).ToString("ddd MMM dd yyyy hh:mm tt");
        //                sqlCommand.Parameters["@EventText2"].Value = DBNull.Value;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
        private SqlCommand BuildSqlCommandTempleEventUpload(SqlConnection sqlConnection, string execUniqueId)
        {
            string sqlStmt = "INSERT TempleEventUpload(LocationNameDesc, TempleEventMasterId, StartDate, EventText1, EventText2) SELECT @LocationNameDesc, @TempleEventMasterId, @StartDate, @EventText1, @EventText2";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@TempleEventMasterId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.NChar, 10);
            sqlCommand.Parameters.Add("@EventText1", SqlDbType.NVarChar, 1024);
            sqlCommand.Parameters.Add("@EventText2", SqlDbType.NVarChar, 1024);
            return sqlCommand;
        }
    }
}
/*
--TRUNCATE TABLE TempleEvent
INSERT TempleEvent(LocationNameDesc, TempleEventMasterId, StartDate, EventName1, EventName2, EventName3, EventDesc1, EventDesc2, EventDesc3, EventText1, EventText2, EventText3)
SELECT LocationNameDesc, TempleEventMasterId, StartDate, EventName1, EventName2, EventName3, EventDesc1, EventDesc2, EventDesc3, EventText1, EventText2, EventText3 FROM TempleEventCustom WHERE TempleEventMasterId = 99999
UNION
SELECT LocationNameDesc, TempleEventMasterId, StartDate, NULL EventName1, NULL EventName2, NULL EventName3, NULL EventDesc1, NULL EventDesc2, NULL EventDesc3, EventText1, NULL EventText2, NULL EventText3 FROM TempleEventUpload

--306 AMAVASYA_KRISHNA, 295 CHATHURTHI_KRISHNA, 282 SASHTI_SHUKLA, 297 SASHTI_KRISHNA, 284 ASHTAMI_SHUKLA, 299 ASHTAMI_KRISHNA, 323 SWATI, 287 EKAADASHI_SHUKLA, 302 EKAADASHI_KRISHNA, 277 PRATIPATH_SHUKLA
SELECT CalendarCodeId, '"' + StartDate + ' ' + StartTime + '",' AS StartDateTime, '"' + StartDate + '",', * FROM  Calendar
WHERE CalendarCodeId IN(306) AND LocationNameDesc = 'FREMONT' AND (StartDate BETWEEN '2023-01-01' AND '2023-12-31' OR FinishDate BETWEEN '2023-01-01' AND '2023-12-31')
ORDER BY 1, 2

        SELECT 
               TempleEventMaster.TempleEventMasterId
              ,Calendar.StartDate
              ,Calendar.StartTime
              ,Calendar.FinishDate
              ,Calendar.FinishTime
              ,*
          FROM Calendar
    INNER JOIN CodeData
            ON Calendar.CalendarCodeId = CodeData.CodeDataId
    INNER JOIN TempleEventMaster
            ON TempleEventMaster.CalendarCodeId LIKE '%,' + CAST(CodeData.CodeDataId AS VARCHAR(50)) + ',%'
         WHERE Calendar.StartDate BETWEEN '2022-12-31' AND '2024-01-01'
           AND Calendar.LocationNameDesc = 'FREMONT'
      ORDER BY TempleEventMaster.TempleEventMasterId
              ,Calendar.StartDate
              ,Calendar.StartTime
              ,Calendar.FinishDate
              ,Calendar.FinishTime
--
*/
