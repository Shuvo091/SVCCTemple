using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SVCCTempleWeb.Models;
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
        public CalendarDataViewModel CalendarDataView(string locationNameDesc, string locationNameDesc1, string yearParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                if (!long.TryParse(yearParm, out long year))
                {
                    year = DateTime.Now.Year + 1;
                }
                string startDate = $"{year}-01-01", finishDate = $"{year}-12-31";
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                CalendarDateDatas(locationNameDesc, locationNameDesc1, startDate, finishDate, sqlConnection, out List<CalendarDateDataModel> thithiDataModels, out List<CalendarDateDataModel> nakshatraDataModels, out List<CalendarDateDataModel> rasiDataModels, clientId, ipAddress, execUniqueId, loggedInUserId);
                CalendarDataViewModel calendarDataViewModel = new CalendarDataViewModel
                {
                    CalendarYear = year,
                    NakshatraDataModels = nakshatraDataModels,
                    FinishDate = finishDate,
                    StartDate = startDate,
                    RasiDataModels = rasiDataModels,
                    RiseSetModels = GetRiseSetDatas(locationNameDesc, locationNameDesc1, startDate, finishDate, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId),
                    TempleScheduleMasterList = new TempleScheduleMasterList
                    {
                        TempleScheduleMasterModels = GetTempleScheduleMasters(locationNameDesc, locationNameDesc1, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId),
                    },
                    TempleScheduleUploadList = new TempleScheduleUploadList
                    {
                        TempleScheduleUploadModels = GetTempleScheduleUploads(locationNameDesc, locationNameDesc1, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId),
                    },
                    TempleFestivalUploadList = new TempleScheduleUploadList
                    {
                        TempleScheduleUploadModels = GetTempleFestivalUploads(locationNameDesc, locationNameDesc1, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId),
                    },
                    ThithiDataModels = thithiDataModels,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return calendarDataViewModel;
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
        public void TempleScheduleUploadList(string locationNameDesc, string locationNameDesc1, ref TempleScheduleUploadList templeScheduleUploadList, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                var templeScheduleUploads = GetTempleScheduleUploads(locationNameDesc, locationNameDesc1, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                TempleScheduleUploadModel templeScheduleUploadTemp;
                #region
                string sqlStmt = "";
                sqlStmt += "    UPDATE TempleScheduleUpload" + Environment.NewLine;
                sqlStmt += "       SET StartDate = @StartDate" + Environment.NewLine;
                sqlStmt += "          ,UpdDateTime = GETDATE()" + Environment.NewLine;
                sqlStmt += "     WHERE TempleScheduleUploadId = @TempleScheduleUploadId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10);
                sqlCommand.Parameters.Add("@TempleScheduleUploadId", SqlDbType.BigInt);
                #endregion
                foreach (var templeScheduleUpload in templeScheduleUploads)
                {
                    templeScheduleUploadTemp = templeScheduleUploadList.TempleScheduleUploadModels.Find(x => x.TempleScheduleUploadId == templeScheduleUpload.TempleScheduleUploadId);
                    if (templeScheduleUploadTemp != null)
                    {
                        templeScheduleUploadTemp.StartDate = string.IsNullOrWhiteSpace(templeScheduleUploadTemp.StartDate) ? "" : templeScheduleUploadTemp.StartDate;
                        if (templeScheduleUploadTemp.StartDate != templeScheduleUpload.StartDate)
                        {
                            //templeScheduleUploadTemp.StartDate = "12345678901234567890";
                            //sqlCommand.Parameters["@StartDate"].Size = 50;
                            sqlCommand.Parameters["@StartDate"].Value = string.IsNullOrWhiteSpace(templeScheduleUploadTemp.StartDate) ? (object)DBNull.Value : templeScheduleUploadTemp.StartDate;
                            sqlCommand.Parameters["@TempleScheduleUploadId"].Value = templeScheduleUploadTemp.TempleScheduleUploadId;
                            var x = sqlCommand.ExecuteNonQuery();
                            //Update
                        }
                    }
                }
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
        public void TempleFestivalUploadList(string locationNameDesc, string locationNameDesc1, ref TempleScheduleUploadList templeScheduleUploadList, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                var templeScheduleUploads = GetTempleFestivalUploads(locationNameDesc, locationNameDesc1, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                TempleScheduleUploadModel templeScheduleUploadTemp;
                #region
                string sqlStmt = "";
                sqlStmt += "    UPDATE TempleScheduleUpload" + Environment.NewLine;
                sqlStmt += "       SET StartDate = @StartDate" + Environment.NewLine;
                sqlStmt += "          ,FinishDate = @FinishDate" + Environment.NewLine;
                sqlStmt += "          ,UpdDateTime = GETDATE()" + Environment.NewLine;
                sqlStmt += "     WHERE TempleScheduleUploadId = @TempleScheduleUploadId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10);
                sqlCommand.Parameters.Add("@FinishDate", SqlDbType.NVarChar, 10);
                sqlCommand.Parameters.Add("@TempleScheduleUploadId", SqlDbType.BigInt);
                #endregion
                foreach (var templeScheduleUpload in templeScheduleUploads)
                {
                    templeScheduleUploadTemp = templeScheduleUploadList.TempleScheduleUploadModels.Find(x => x.TempleScheduleUploadId == templeScheduleUpload.TempleScheduleUploadId);
                    if (templeScheduleUploadTemp != null)
                    {
                        templeScheduleUploadTemp.StartDate = string.IsNullOrWhiteSpace(templeScheduleUploadTemp.StartDate) ? "" : templeScheduleUploadTemp.StartDate;
                        templeScheduleUploadTemp.FinishDate = string.IsNullOrWhiteSpace(templeScheduleUploadTemp.FinishDate) ? "" : templeScheduleUploadTemp.FinishDate;
                        if (!(templeScheduleUploadTemp.StartDate == templeScheduleUpload.StartDate && templeScheduleUploadTemp.FinishDate == templeScheduleUpload.FinishDate))
                        {
                            //templeScheduleUploadTemp.StartDate = "12345678901234567890";
                            //sqlCommand.Parameters["@StartDate"].Size = 50;
                            sqlCommand.Parameters["@StartDate"].Value = string.IsNullOrWhiteSpace(templeScheduleUploadTemp.StartDate) ? (object)DBNull.Value : templeScheduleUploadTemp.StartDate;
                            sqlCommand.Parameters["@FinishDate"].Value = string.IsNullOrWhiteSpace(templeScheduleUploadTemp.FinishDate) ? (object)DBNull.Value : templeScheduleUploadTemp.FinishDate;
                            sqlCommand.Parameters["@TempleScheduleUploadId"].Value = templeScheduleUploadTemp.TempleScheduleUploadId;
                            var x = sqlCommand.ExecuteNonQuery();
                            //Update
                        }
                    }
                }
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
        public List<RiseSetModel> GetRiseSetDatas(string locationNameDesc, string locationNameDesc1, string startDate, string finishDate, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<RiseSetModel> riseSetModels = new List<RiseSetModel>();
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               *" + Environment.NewLine;
                sqlStmt += "          FROM RiseSet" + Environment.NewLine;
                sqlStmt += $"        WHERE GregorianDate BETWEEN '{startDate}' AND '{finishDate}'" + Environment.NewLine;
                sqlStmt += $"           AND LocationNameDesc = '{locationNameDesc}'" + Environment.NewLine;
                sqlStmt += "      ORDER BY GregorianDate" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                while (sqlDataReader.Read())
                {
                    riseSetModels.Add
                    (
                        new RiseSetModel
                        {
                            RiseSetId = long.Parse(sqlDataReader["RiseSetId"].ToString()),
                            GregorianDate = sqlDataReader["GregorianDate"].ToString(),
                            SunRise = sqlDataReader["SunRise"].ToString(),
                            SunSet = sqlDataReader["SunSet"].ToString(),
                            RKStart = sqlDataReader["RKStart"].ToString(),
                            RKFinish = sqlDataReader["RKFinish"].ToString(),
                            YGStart = sqlDataReader["YGStart"].ToString(),
                            YGFinish = sqlDataReader["YGFinish"].ToString(),
                        }
                    );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return riseSetModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }
        public List<TempleScheduleMasterModel> GetTempleScheduleMasters(string locationNameDesc, string locationNameDesc1, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<TempleScheduleMasterModel> templeScheduleMasterModels = new List<TempleScheduleMasterModel>();
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               *" + Environment.NewLine;
                sqlStmt += "          FROM TempleScheduleMaster" + Environment.NewLine;
                sqlStmt += "     LEFT JOIN CodeData ON CalendarCodeId = CodeDataId" + Environment.NewLine;
                sqlStmt += "         WHERE Editable = 1--FrequencyTypeNameDesc IN('RECURRING', 'FESTIVAL')" + Environment.NewLine;
                sqlStmt += $"           AND LocationNameDesc = '{locationNameDesc}'" + Environment.NewLine;
                sqlStmt += "      ORDER BY ScheduleMasterSeqNum, SeqNum" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                //sqlStmt += "" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                while (sqlDataReader.Read())
                {
                    templeScheduleMasterModels.Add
                    (
                        new TempleScheduleMasterModel
                        {
                            TempleScheduleMasterId = long.Parse(sqlDataReader["TempleScheduleMasterId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            ScheduleMasterId = long.Parse(sqlDataReader["ScheduleMasterId"].ToString()),
                            ScheduleMasterSeqNum = float.Parse(sqlDataReader["ScheduleMasterSeqNum"].ToString()),
                            CalendarCodeId = sqlDataReader["CalendarCodeId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString() == "" ? sqlDataReader["SampleDateStart"].ToString() : sqlDataReader["StartDate"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            FrequencyTypeNameDesc = sqlDataReader["FrequencyTypeNameDesc"].ToString(),
                            FrequencyCategoryNameDesc = sqlDataReader["FrequencyCategoryNameDesc"].ToString(),
                            EventDesc0 = sqlDataReader["EventDesc0"].ToString(),
                            EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                            EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                            EventDesc3 = sqlDataReader["EventDesc3"].ToString(),
                            EventDesc4 = sqlDataReader["EventDesc4"].ToString(),
                            EventDesc5 = sqlDataReader["EventDesc5"].ToString(),
                            EventDesc6 = sqlDataReader["EventDesc6"].ToString(),
                            EventDesc7 = sqlDataReader["EventDesc7"].ToString(),
                            EventDesc8 = sqlDataReader["EventDesc8"].ToString(),
                            EventDesc9 = sqlDataReader["EventDesc9"].ToString(),
                            InfoOnly = bool.Parse(sqlDataReader["InfoOnly"].ToString()),
                            EventAtTemple = bool.Parse(sqlDataReader["EventAtTemple"].ToString()),
                            MoreInfo = bool.Parse(sqlDataReader["MoreInfo"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            KioskGroupId = sqlDataReader["KioskGroupId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                            FinishDate = sqlDataReader["FinishDate"].ToString() == "" ? sqlDataReader["SampleDateFinish"].ToString() : sqlDataReader["FinishDate"].ToString(),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            ImageFileName1 = sqlDataReader["ImageFileName1"].ToString(),
                            ImageFileName2 = sqlDataReader["ImageFileName2"].ToString(),
                            ImageFileName3 = sqlDataReader["ImageFileName3"].ToString(),
                            FileName1 = sqlDataReader["FileName1"].ToString(),
                            FileName2 = sqlDataReader["FileName2"].ToString(),
                            FileName3 = sqlDataReader["FileName3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            HtmlFileName2 = sqlDataReader["HtmlFileName2"].ToString(),
                            HtmlFileName3 = sqlDataReader["HtmlFileName3"].ToString(),
                            CategoryNameDesc = sqlDataReader["CategoryNameDesc"].ToString(),
                            SampleDateStart = sqlDataReader["SampleDateStart"].ToString(),
                            SampleDateFinish = sqlDataReader["SampleDateFinish"].ToString(),
                            StatusNameDesc = sqlDataReader["StatusNameDesc"].ToString(),
                            CodeDataModel = new CodeDataModel
                            {
                                CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                                CodeDataDesc1 = sqlDataReader["CodeDataDesc1"].ToString(),
                                CodeDataDesc2 = sqlDataReader["CodeDataDesc2"].ToString(),
                                CodeDataDesc3 = sqlDataReader["CodeDataDesc3"].ToString(),
                                CodeDataNameDesc = sqlDataReader["CodeDataNameDesc"].ToString(),
                            },
                        }
                    );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return templeScheduleMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }
        public List<TempleScheduleUploadModel> GetTempleScheduleUploads(string locationNameDesc, string locationNameDesc1, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<TempleScheduleUploadModel> templeScheduleUploadModels = new List<TempleScheduleUploadModel>();
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               TempleScheduleUpload.*" + Environment.NewLine;
                sqlStmt += "              ,TempleScheduleMaster.*" + Environment.NewLine;
                sqlStmt += "              ,Calendar.*" + Environment.NewLine;
                sqlStmt += "              ,CodeData.*" + Environment.NewLine;
                sqlStmt += "          FROM TempleScheduleUpload" + Environment.NewLine;
                sqlStmt += "    INNER JOIN TempleScheduleMaster" + Environment.NewLine;
                sqlStmt += "            ON TempleScheduleUpload.ScheduleMasterId = TempleScheduleMaster.ScheduleMasterId" + Environment.NewLine;
                sqlStmt += "           AND TempleScheduleMaster.SeqNum = 1" + Environment.NewLine;
                sqlStmt += "    INNER JOIN Calendar" + Environment.NewLine;
                sqlStmt += "            ON TempleScheduleUpload.CalendarId = Calendar.CalendarId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
                sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
                sqlStmt += $"         WHERE TempleScheduleUpload.LocationNameDesc = '{locationNameDesc}'" + Environment.NewLine;
                sqlStmt += "           AND Editable = 1" + Environment.NewLine;
                sqlStmt += "      ORDER BY TempleScheduleMaster.ScheduleMasterSeqNum" + Environment.NewLine;
                sqlStmt += "              ,TempleScheduleUpload.StartDate" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                int startDateIndex = -1;
                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    if (sqlDataReader.GetName(i) == "StartDate")
                    {
                        for (i = i + 1; i < sqlDataReader.FieldCount; i++)
                        {
                            if (sqlDataReader.GetName(i) == "StartDate")
                            {
                                for (i = i + 1; i < sqlDataReader.FieldCount; i++)
                                {
                                    if (sqlDataReader.GetName(i) == "StartDate")
                                    {
                                        startDateIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                while (sqlDataReader.Read())
                {
                    templeScheduleUploadModels.Add
                    (
                        new TempleScheduleUploadModel
                        {
                            TempleScheduleUploadId = long.Parse(sqlDataReader["TempleScheduleUploadId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            ScheduleMasterId = long.Parse(sqlDataReader["TempleScheduleMasterId"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            EventText1 = sqlDataReader["EventText1"].ToString(),
                            EventText2 = sqlDataReader["EventText2"].ToString(),
                            CalendarId = sqlDataReader["CalendarId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["CalendarId"].ToString()),
                            CalendarDateDataModel = new CalendarDateDataModel
                            {
                                CalendarCodeId = long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                                FinishDate = sqlDataReader[startDateIndex + 2].ToString(),
                                FinishTime = sqlDataReader[startDateIndex + 3].ToString(),
                                StartDate = sqlDataReader[startDateIndex].ToString(),
                                StartTime = sqlDataReader[startDateIndex + 1].ToString(),
                                CodeDataModel = new CodeDataModel
                                {
                                    CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                                    CodeDataDesc1 = sqlDataReader["CodeDataDesc1"].ToString(),
                                    CodeDataDesc2 = sqlDataReader["CodeDataDesc2"].ToString(),
                                    CodeDataDesc3 = sqlDataReader["CodeDataDesc3"].ToString(),
                                    CodeDataNameDesc = sqlDataReader["CodeDataNameDesc"].ToString(),
                                },
                            },
                            TempleScheduleMasterModel = new TempleScheduleMasterModel
                            {
                                CalendarCodeId = long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                                EventDesc0 = sqlDataReader["EventDesc0"].ToString(),
                                EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                            }
                        }
                    );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return templeScheduleUploadModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }
        public List<TempleScheduleUploadModel> GetTempleFestivalUploads(string locationNameDesc, string locationNameDesc1, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<TempleScheduleUploadModel> templeScheduleUploadModels = new List<TempleScheduleUploadModel>();
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               TempleScheduleUpload.*" + Environment.NewLine;
                sqlStmt += "              ,TempleScheduleMaster.*" + Environment.NewLine;
                sqlStmt += "          FROM TempleScheduleUpload" + Environment.NewLine;
                sqlStmt += "    INNER JOIN TempleScheduleMaster" + Environment.NewLine;
                sqlStmt += "            ON TempleScheduleUpload.ScheduleMasterId = TempleScheduleMaster.ScheduleMasterId" + Environment.NewLine;
                sqlStmt += $"         WHERE TempleScheduleUpload.LocationNameDesc = '{locationNameDesc}'" + Environment.NewLine;
                sqlStmt += "           AND TempleScheduleMaster.FrequencyTypeNameDesc = 'FESTIVAL'" + Environment.NewLine;
                sqlStmt += "           AND TempleScheduleMaster.Editable = 1" + Environment.NewLine;
                sqlStmt += "      ORDER BY TempleScheduleUpload.StartDate" + Environment.NewLine;
                sqlStmt += "              ,TempleScheduleMaster.ScheduleMasterSeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                while (sqlDataReader.Read())
                {
                    templeScheduleUploadModels.Add
                    (
                        new TempleScheduleUploadModel
                        {
                            TempleScheduleUploadId = long.Parse(sqlDataReader["TempleScheduleUploadId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            ScheduleMasterId = long.Parse(sqlDataReader["TempleScheduleMasterId"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            FinishDate = sqlDataReader["FinishDate"].ToString(),
                            EventText1 = sqlDataReader["EventText1"].ToString(),
                            EventText2 = sqlDataReader["EventText2"].ToString(),
                            TempleScheduleMasterModel = new TempleScheduleMasterModel
                            {
                                EventDesc0 = sqlDataReader["EventDesc0"].ToString(),
                                EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                            }
                        }
                    );
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return templeScheduleUploadModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }
        public void CalendarDateDatas(string locationNameDesc, string locationNameDesc1, string startDate, string finishDate, SqlConnection sqlConnection, out List<CalendarDateDataModel> thithiDataModels, out List<CalendarDateDataModel> nakshatraDataModels, out List<CalendarDateDataModel> rasiDataModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                thithiDataModels = new List<CalendarDateDataModel>();
                nakshatraDataModels = new List<CalendarDateDataModel>();
                rasiDataModels = new List<CalendarDateDataModel>();
                CalendarDateDataModel calendarDateDataModel;
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               *" + Environment.NewLine;
                sqlStmt += "          FROM Calendar" + Environment.NewLine;
                sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
                sqlStmt += "            ON CalendarCodeId = CodeDataId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN CodeType" + Environment.NewLine;
                sqlStmt += "            ON CodeData.CodeTypeId = CodeType.CodeTypeId" + Environment.NewLine;
                sqlStmt += "         WHERE(" + Environment.NewLine;
                sqlStmt += $"                StartDate BETWEEN '{startDate}' AND '{finishDate}'" + Environment.NewLine;
                sqlStmt += $"             OR FinishDate BETWEEN '{startDate}' AND '{finishDate}'" + Environment.NewLine;
                sqlStmt += $"             OR '{startDate}' BETWEEN StartDate AND FinishDate" + Environment.NewLine;
                sqlStmt += $"             OR '{finishDate}' BETWEEN StartDate AND FinishDate" + Environment.NewLine;
                sqlStmt += "               )" + Environment.NewLine;
                sqlStmt += $"           AND LocationNameDesc = '{locationNameDesc}'" + Environment.NewLine;
                sqlStmt += "           AND CodeTypeNameDesc IN('PANCHANGAM_THITHI', 'PANCHANGAM_NAKSHATRA', 'PANCHANGAM_RAASI')" + Environment.NewLine;
                sqlStmt += "      ORDER BY StartDate" + Environment.NewLine;
                sqlStmt += "              ,CodeType.CodeTypeId" + Environment.NewLine;
                sqlStmt += "              ,CodeData.CodeDataId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                while (sqlDataReader.Read())
                {
                    #region
                    calendarDateDataModel = new CalendarDateDataModel
                    {
                        CalendarId = long.Parse(sqlDataReader["CalendarId"].ToString()),
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        DemogInfoCityId = long.Parse(sqlDataReader["DemogInfoCityId"].ToString()),
                        CalendarCodeId = long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                        StartDate = sqlDataReader["StartDate"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        FinishDate = sqlDataReader["FinishDate"].ToString(),
                        FinishTime = sqlDataReader["FinishTime"].ToString(),
                        CodeDataModel = new CodeDataModel
                        {
                            CodeDataId = long.Parse(sqlDataReader["CodeDataId"].ToString()),
                            CodeDataNameDesc = sqlDataReader["CodeDataNameDesc"].ToString(),
                            CodeDataNameId = long.Parse(sqlDataReader["CodeDataNameId"].ToString()),
                            CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                            CodeDataDesc1 = sqlDataReader["CodeDataDesc1"].ToString(),
                            CodeDataDesc2 = sqlDataReader["CodeDataDesc2"].ToString(),
                            CodeDataDesc3 = sqlDataReader["CodeDataDesc3"].ToString(),
                            CodeDataDesc4 = sqlDataReader["CodeDataDesc4"].ToString(),
                            CodeDataDesc5 = sqlDataReader["CodeDataDesc5"].ToString(),
                            CodeDataDesc6 = sqlDataReader["CodeDataDesc6"].ToString(),
                            CodeDataDesc7 = sqlDataReader["CodeDataDesc7"].ToString(),
                            CodeDataDesc8 = sqlDataReader["CodeDataDesc8"].ToString(),
                            CodeDataDesc9 = sqlDataReader["CodeDataDesc9"].ToString(),
                            CodeTypeModel = new CodeTypeModel
                            {
                                CodeTypeId = long.Parse(sqlDataReader["CodeTypeId"].ToString()),
                                CodeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString(),
                                CodeTypeNameId = long.Parse(sqlDataReader["CodeTypeNameId"].ToString()),
                                CodeTypeDesc = sqlDataReader["CodeTypeDesc"].ToString(),
                                CodeTypeDesc1 = sqlDataReader["CodeTypeDesc1"].ToString(),
                            },
                        },
                    };
                    #endregion
                    switch (sqlDataReader["CodeTypeNameDesc"].ToString())
                    {
                        case "PANCHANGAM_RAASI":
                            rasiDataModels.Add(calendarDateDataModel);
                            break;
                        case "PANCHANGAM_THITHI":
                            thithiDataModels.Add(calendarDateDataModel);
                            break;
                        case "PANCHANGAM_NAKSHATRA":
                            nakshatraDataModels.Add(calendarDateDataModel);
                            break;
                        default:
                            break;
                    }
                }
                sqlDataReader.Close();
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
            }
        }
    }
}
