using ArchitectureLibraryEmailUtility;
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
using System.Web.Mvc;

namespace SVCCTempleWeb.ClassCode
{
    public partial class SVCCTempleBL
    {
        public BookingModel Booking(string locationNameDesc, string locationNameDesc1, long? bookingId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new BookingModel();
        }

        public void Booking(string locationNameDesc, string locationNameDesc1, ref BookingModel bookingModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                if (modelStateDictionary.IsValid)
                {
                    #region
                    string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                    sqlConnection = new SqlConnection(databaseConnectionString);
                    sqlConnection.Open();
                    string sqlStmt = "";
                    sqlStmt += "INSERT Booking(LocationNameDesc, EmailAddress, FirstName, LastName, TelephoneNumber, StartDate, StartTime, FinishDate, FinishTime, PatronComments, PriestComments, StatusNameDesc)" + Environment.NewLine;
                    sqlStmt += "OUTPUT INSERTED.BookingId" + Environment.NewLine;
                    sqlStmt += "SELECT @LocationNameDesc, @EmailAddress, @FirstName, @LastName, @TelephoneNumber, @StartDate, @StartTime, @FinishDate, @FinishTime, @PatronComments, @PriestComments, @StatusNameDesc)" + Environment.NewLine;
                    //sqlStmt += "" + Environment.NewLine;
                    SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                    sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
                    sqlCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 512);
                    sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100);
                    sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);
                    sqlCommand.Parameters.Add("@TelephoneNumber", SqlDbType.BigInt);
                    sqlCommand.Parameters.Add("@StartDate", SqlDbType.Char, 10);
                    sqlCommand.Parameters.Add("@StartTime", SqlDbType.Char, 5);
                    sqlCommand.Parameters.Add("@FinishDate", SqlDbType.Char, 10);
                    sqlCommand.Parameters.Add("@FinishDate", SqlDbType.NVarChar, 5);
                    sqlCommand.Parameters.Add("@PatronComments", SqlDbType.NVarChar, 2048);
                    sqlCommand.Parameters.Add("@PriestComments", SqlDbType.NVarChar, 2048);
                    sqlCommand.Parameters.Add("@StatusNameDesc", SqlDbType.NVarChar, 50);
                    #endregion
                    #region
                    sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                    sqlCommand.Parameters["@EmailAddress"].Value = bookingModel.EmailAddress;
                    sqlCommand.Parameters["@FirstName"].Value = bookingModel.FirstName;
                    sqlCommand.Parameters["@LastName"].Value = bookingModel.LastName;
                    sqlCommand.Parameters["@TelephoneNumber"].Value = bookingModel.TelephoneNumber;
                    sqlCommand.Parameters["@StartDate"].Value = bookingModel.StartDate;
                    sqlCommand.Parameters["@StartTime"].Value = bookingModel.StartTime;
                    sqlCommand.Parameters["@FinishDate"].Value = string.IsNullOrWhiteSpace(bookingModel.FinishDate) ? (object)DBNull.Value : bookingModel.FinishDate;
                    sqlCommand.Parameters["@FinishTime"].Value = string.IsNullOrWhiteSpace(bookingModel.FinishTime) ? (object)DBNull.Value : bookingModel.FinishTime;
                    sqlCommand.Parameters["@PatronComments"].Value = string.IsNullOrWhiteSpace(bookingModel.PatronComments) ? (object)DBNull.Value : bookingModel.PatronComments;
                    sqlCommand.Parameters["@PriestComments"].Value = string.IsNullOrWhiteSpace(bookingModel.PriestComments) ? (object)DBNull.Value : bookingModel.PriestComments;
                    sqlCommand.Parameters["@StatusNameDesc"].Value = "OPEN";
                    bookingModel.BookingId = (long)sqlCommand.ExecuteScalar();
                    #endregion
                }
                else
                {
                    bookingModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
                    };
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch
                {
                }
            }
        }
    }
}
