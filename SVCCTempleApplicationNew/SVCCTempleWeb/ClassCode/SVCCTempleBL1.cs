using ArchitectureLibraryEmailUtility;
using ArchitectureLibraryException;
using ArchitectureLibraryPDFLibrary;
using ArchitectureLibraryShoppingCartService;
using ArchitectureLibraryUtility;
using SVCCTempleWeb.Enumerations;
using SVCCTempleWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.ClassCode
{
    public partial class SVCCTempleBL
    {
        //GET
        public ContactUsModel ContactUs(string locationNameDesc, string execUniqueId)
        {
            ContactUsModel contactUsModel = new ContactUsModel
            {
                ResponseObjectModel = new ResponseObjectModel0
                {
                    ResponseMessages = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("", "Please enter the below information & click Submit"),
                        new KeyValuePair<string, string>("", "Please check your email for a copy"),
                        new KeyValuePair<string, string>("", "We will get back to you, once we review your email"),
                    },
                    ResponseTypeId = ResponseTypeEnum.Info,
                    TextAlign = "left",
                    ListStyleType = "decimal",
                    StatusCode = HttpStatusCode.OK,
                },
            };
            return contactUsModel;
        }
        //POST
        public void ContactUs(string locationNameDesc, string locationNameDesc1, ContactUsModel contactUsModel, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            WebUtilities webUtilities = new WebUtilities();
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                InsertContactUs(locationNameDesc, contactUsModel, sqlConnection, execUniqueId);
                sqlConnection.Close();
                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                Dictionary<string, string> emailTemplateKeywordValues = ContactUsEmailKeywordValues(locationNameDesc, locationNameDesc1, contactUsModel, execUniqueId);
                SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, null, execUniqueId);
                contactUsModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel(contactUsModel.ResponseObjectModel);
                contactUsModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Success;
                contactUsModel.ResponseObjectModel.TextAlign = "left";
                contactUsModel.ResponseObjectModel.ListStyleType = "decimal";
                contactUsModel.ResponseObjectModel.StatusCode = HttpStatusCode.OK;
                contactUsModel.ResponseObjectModel.ResponseMessages = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", "Your request has been successfully saved in our database and emailed"),
                    new KeyValuePair<string, string>("", "You should see a copy of the request in your Inbox"),
                    new KeyValuePair<string, string>("", "Please make suer to check your Junk / Spam folder"),
                    new KeyValuePair<string, string>("", "If you still need to contact us, please feel free to call us"),
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
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
        //EventRegister GET
        public EventRegisterModel EventRegister(string locationNameDesc, string locationNameDesc1, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT SessionId, COUNT(*) AS RegisteredCount FROM EventRegister GROUP BY SessionId", sqlConnection);
                EventRegisterModel eventRegisterModel = new EventRegisterModel
                {
                    SessionRegisters = new List<SessionRegister>
                    {
                        new SessionRegister
                        {
                            SessionId = 1,
                            SessionDesc = "Fremont - Sat - Apr 1 2023 3pm - 5:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                        new SessionRegister
                        {
                            SessionId = 2,
                            SessionDesc = "Fremont - Sun - Apr 2 2023 10am - 12:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                        new SessionRegister
                        {
                            SessionId = 3,
                            SessionDesc = "Sacramento - Sat - Apr 8 2023 10am - 12:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                        new SessionRegister
                        {
                            SessionId = 4,
                            SessionDesc = "Sacramento - Sat - Apr 8 2023 4pm - 6:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                    },
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Success,
                    }
                };
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    eventRegisterModel.SessionRegisters[int.Parse(sqlDataReader["SessionId"].ToString()) - 1].RegisteredCount = long.Parse(sqlDataReader[1].ToString());
                }
                eventRegisterModel.SessionRegisters.RemoveAt(0);
                eventRegisterModel.SessionRegisters.RemoveAt(0);
                sqlDataReader.Close();
                return eventRegisterModel;
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
                    sqlConnection.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        //EventRegister POST
        public void EventRegister(string locationNameDesc, string locationNameDesc1, ref EventRegisterModel eventRegisterModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = BuildSqlCommandEventRegisterInsert(locationNameDesc, locationNameDesc1, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@Comments"].Value = string.IsNullOrWhiteSpace(eventRegisterModel.Comments) ? (object)DBNull.Value : eventRegisterModel.Comments;
                sqlCommand.Parameters["@EmailAddress"].Value = eventRegisterModel.EmailAddress;
                sqlCommand.Parameters["@FirstName"].Value = eventRegisterModel.FirstName;
                sqlCommand.Parameters["@LastName"].Value = eventRegisterModel.LastName;
                sqlCommand.Parameters["@SessionDesc"].Value = eventRegisterModel.SessionDesc;
                sqlCommand.Parameters["@SessionId"].Value = eventRegisterModel.SessionId;
                sqlCommand.Parameters["@TelephoneNumber"].Value = eventRegisterModel.TelephoneNumber;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                eventRegisterModel.EventRegisterId = (long)sqlCommand.ExecuteScalar();
                string emailFromEmailAddress = HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString();
                string emailFromEmailDisplayName = HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString();
                EmailService emailService = new EmailService();
                string emailDirectoryName = HttpContext.Current.Application["EmailDirectoryName"].ToString();
                var toEmailAddresses = new List<KeyValuePair<string, string>>();
                toEmailAddresses.Add(new KeyValuePair<string, string>(eventRegisterModel.EmailAddress, ""));
                toEmailAddresses.Add(new KeyValuePair<string, string>(emailFromEmailAddress, emailFromEmailDisplayName));
                var fromEmailAddress = new KeyValuePair<string, string>(emailFromEmailAddress, emailFromEmailDisplayName);
                List<KeyValuePair<string, string>> replyToEmailAddresses = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(emailFromEmailAddress, emailFromEmailDisplayName),
                };
                List<KeyValuePair<string, string>> bccEmailAddresss = new List<KeyValuePair<string, string>>();
                var emailBccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
                foreach (var emailBccEmailAddress in emailBccEmailAddresss.Split(';'))
                {
                    bccEmailAddresss.Add(new KeyValuePair<string, string>(emailBccEmailAddress, ""));
                }
                string emailSubject = "Your request successfully processed " + eventRegisterModel.EventRegisterId + " " + eventRegisterModel.EmailAddress + " " + DateTime.Now.ToString("ddd MMM-dd-yyyy hh:mm tt");
                string emailBody = "";

                #region
                emailBody += "Dear Devotee," + "<br />" + Environment.NewLine;
                emailBody += "You request is successfully submitted" + "<br />" + "<br />" + Environment.NewLine;
                emailBody += "Below are the details of your request" + "<br />" + "<br />" + Environment.NewLine;
                emailBody += "<table style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px;\">" + Environment.NewLine;
                //emailBody += "<tr>" + Environment.NewLine;
                //emailBody += "    <th colspan=\"2\" style=\"border-collapse: collpase; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                //emailBody += "        Dear Devotee" + Environment.NewLine;
                //emailBody += "    </th>" + Environment.NewLine;
                //emailBody += "</tr>" + Environment.NewLine;
                //emailBody += "<tr>" + Environment.NewLine;
                //emailBody += "    <th colspan=\"2\" style=\"border-collapse: collpase; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                //emailBody += "        You are successfully registered" + Environment.NewLine;
                //emailBody += "    </th>" + Environment.NewLine;
                //emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; text-align: left; padding: 5px; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        Order#" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; text-align: left; padding: 5px; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + eventRegisterModel.EventRegisterId + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; text-align: left; padding: 5px; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        Pickup Date" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; text-align: left; padding: 5px; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + eventRegisterModel.SessionDesc + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        Email" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collpase; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + eventRegisterModel.EmailAddress + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        First Name" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + eventRegisterModel.FirstName + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        Last Name" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + eventRegisterModel.LastName + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        Telephone#" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + long.Parse(eventRegisterModel.TelephoneNumber).ToString("(###) ###-####") + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "<tr>" + Environment.NewLine;
                emailBody += "    <th style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "        Comments" + Environment.NewLine;
                emailBody += "    </th>" + Environment.NewLine;
                emailBody += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; padding: 5px; text-align: left; vertical-align: top;\">" + Environment.NewLine;
                emailBody += "         " + eventRegisterModel.Comments.Replace("\n", "<br />") + Environment.NewLine;
                emailBody += "    </td>" + Environment.NewLine;
                emailBody += "</tr>" + Environment.NewLine;
                emailBody += "</table>" + Environment.NewLine;
                emailBody += "<br /><br />";
                #endregion
                var templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
                templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
                emailBody += SignatureTemplate(locationNameDesc1, templatesDirectoryName);
                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                emailService.SendEmail(emailDirectoryName, "", fromEmailAddress, emailSubject, emailBody, toEmailAddresses, execUniqueId, privateKey, replyToEmailAddresses, null, bccEmailAddresss, null, true);
                eventRegisterModel = new EventRegisterModel
                {
                    SessionRegisters = new List<SessionRegister>
                    {
                        new SessionRegister
                        {
                            SessionId = 1,
                            SessionDesc = "Fremont - Sat - Apr 1 2023 3pm - 5:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                        new SessionRegister
                        {
                            SessionId = 2,
                            SessionDesc = "Fremont - Sun - Apr 2 2023 10am - 12:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                        new SessionRegister
                        {
                            SessionId = 3,
                            SessionDesc = "Sacramento - Sat - Apr 8 2023 10am - 12:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                        new SessionRegister
                        {
                            SessionId = 4,
                            SessionDesc = "Sacramento - Sat - Apr 8 2023 4pm - 6:30pm",
                            MaxCount = 90,
                            RegisteredCount = 0,
                        },
                    },
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            "Your request is successfully submitted",
                            "Please check your inbox",
                            "May Lord Ganesha bless one and all",
                        },
                        ResponseTypeId = ResponseTypeEnum.Success,
                    }
                };
                sqlCommand = new SqlCommand("SELECT SessionId, COUNT(*) AS RegisteredCount FROM EventRegister GROUP BY SessionId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    eventRegisterModel.SessionRegisters[int.Parse(sqlDataReader["SessionId"].ToString()) - 1].RegisteredCount = long.Parse(sqlDataReader[1].ToString());
                }
                sqlDataReader.Close();
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
                    sqlConnection.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        //GET
        public RegisterUserProfModel RegisterUserProf(string locationNameDesc, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RegisterUserProfModel registerUserProfModel = new RegisterUserProfModel
            {
                ResponseObjectModel0 = new ResponseObjectModel0
                {
                    ResponseMessages = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("", "Please enter email address, confirm email address"),
                        new KeyValuePair<string, string>("", "Click \"REGISTER\" to continue"),
                        new KeyValuePair<string, string>("", "You will receive an email with a link to reset your password"),
                        new KeyValuePair<string, string>("", "Check your Spam, Bulk or Junk Mail folder..."),
                        new KeyValuePair<string, string>("", "If so move to Inbox and mark the sender as safe"),
                        new KeyValuePair<string, string>("", "Continue with creating a new password"),
                        new KeyValuePair<string, string>("", "Take advantage of your receipts, sankalpam info etc."),
                    },
                    ResponseTypeId = ResponseTypeEnum.Info,
                    TextAlign = "left",
                    ListStyleType = "decimal",
                    StatusCode = HttpStatusCode.OK,
                },
            };
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return registerUserProfModel;
        }
        //POST
        //public void RegisterUserProfBacup(string locationNameDesc, string locationNameDesc1, RegisterUserProfModel registerUserProfModel, string execUniqueId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    WebUtilities webUtilities = new WebUtilities();
        //    registerUserProfModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel(registerUserProfModel.ResponseObjectModel);
        //    registerUserProfModel.ResponseObjectModel.ListStyleType = "decimal";
        //    registerUserProfModel.ResponseObjectModel.TextAlign = "left";
        //    SqlConnection sqlConnection = null;
        //    SqlDataReader sqlDataReader = null;
        //    try
        //    {
        //        string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        //        sqlConnection = new SqlConnection(databaseConnectionString);
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = BuildSqlCommandLoginUserSelect(sqlConnection);
        //        sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
        //        sqlCommand.Parameters["@LoginNameId1"].Value = registerUserProfModel.RegisterEmailAddress;
        //        sqlCommand.Parameters["@LoginNameId2"].Value = "";
        //        sqlCommand.Parameters["@LoginNameId3"].Value = "";
        //        sqlDataReader = sqlCommand.ExecuteReader();
        //        if (sqlDataReader.Read())
        //        {
        //            registerUserProfModel.ResponseObjectModel.ListStyleType = "none";
        //            registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
        //            registerUserProfModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
        //            registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
        //            registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "The below email is already registered"));
        //            registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Please try and register with a different email address"));
        //        }
        //        else
        //        {
        //            sqlDataReader.Close();
        //            long? personId = InsertPerson(locationNameDesc, registerUserProfModel, sqlConnection, execUniqueId);
        //            if (personId != null)
        //            {
        //                long? loginUserId = InsertLoginUser(locationNameDesc, (long)personId, registerUserProfModel, sqlConnection, execUniqueId);
        //                if (loginUserId != null)
        //                {
        //                    DateTime resetPasswordDateTime = DateTime.Now.AddDays(3);
        //                    Random random = new Random();
        //                    string randomNumber1 = random.Next(0, 999999999).ToString();
        //                    string randomNumber2 = random.Next(0, 999999999).ToString();
        //                    string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
        //                    string resetPasswordQueryString = personId + "_" + loginUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
        //                    string resetPasswordExpiryDateTime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
        //                    string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
        //                    string resetPasswordKey = GenerateRandomKey(8);
        //                    UpdateLoginUserResetPassword((long)loginUserId, resetPasswordQueryString, resetPasswordExpiryDateTime, resetPasswordKey, privateKey, sqlConnection, execUniqueId);
        //                    Dictionary<string, string> emailTemplateKeywordValues = RegisterEmailKeywordValues(locationNameDesc, locationNameDesc1, registerUserProfModel.RegisterEmailAddress, resetPasswordQueryString, resetPasswordDateTime, resetPasswordKey, execUniqueId);
        //                    SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, null, execUniqueId);
        //                    registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.OK;
        //                    registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Your email has been successfully registered"));
        //                    registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Check your email and follow instructions"));
        //                    registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "If you do not see your email in Inbox please check Spam, Bulk or Junk folder"));
        //                    registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "If so, move the email to Inbox and mark sender as safe"));
        //                }
        //                else
        //                {
        //                    registerUserProfModel.ResponseObjectModel.ListStyleType = "none";
        //                    registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
        //                    registerUserProfModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
        //                    registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
        //                    registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Error while registering email address"));
        //                    registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Please try aagain or contact support"));
        //                }
        //            }
        //            else
        //            {
        //                registerUserProfModel.ResponseObjectModel.ListStyleType = "none";
        //                registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
        //                registerUserProfModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
        //                registerUserProfModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
        //                registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Error while registering email address"));
        //                registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Please try aagain or contact support"));
        //            }
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
        //        registerUserProfModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Error occurred during processing. Please contact support"));
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            sqlDataReader.Close();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //        try
        //        {
        //            sqlConnection.Close();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //    }
        //    return;
        //}
        //GET
        public SponsorListModel SponsorList(string locationNameDesc, string locationNameDesc1, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            WebUtilities webUtilities = new WebUtilities();
            SponsorListModel sponsorListModel = new SponsorListModel
            {
                SponsorshipGroupModels = new List<SponsorshipGroupModel>(),
                ResponseObjectModel = webUtilities.InitializeResponseObjectModel(),
            };
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM SponsorshipGroup" + Environment.NewLine;
                sqlStmt += "         WHERE SponsorshipGroup.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
                sqlStmt += "           AND '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN SponsorshipGroup.BegEffDate AND SponsorshipGroup.EndEffDate" + Environment.NewLine;
                sqlStmt += "      ORDER BY SponsorshipGroup.DisplaySeqNum" + Environment.NewLine;
                sqlStmt += "              ,SponsorshipGroup.SponsorshipGroupId" + Environment.NewLine;
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    sponsorListModel.SponsorshipGroupModels.Add
                    (
                        new SponsorshipGroupModel
                        {
                            SponsorshipGroupId = long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            SponsorshipGroupDesc = sqlDataReader["SponsorshipGroupDesc"].ToString(),
                            DisplaySeqNum = float.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            BegEffDate = sqlDataReader["BegEffDate"].ToString(),
                            EndEffDate = sqlDataReader["EndEffDate"].ToString(),
                        }
                    );
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sponsorListModel;
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
        //GET
        public List<SponsorListDataModel> SponsorListData(string locationNameDesc, string locationNameDesc1, string sponsorshipGroupId, string fromDate, string toDate, string sankalpamStatus)
        {
            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = BuildSqlCommandSponsorListData(locationNameDesc, sponsorshipGroupId, fromDate, toDate, sankalpamStatus, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<SponsorListDataModel> sponsorListDataModels = new List<SponsorListDataModel>();
            while (sqlDataReader.Read())
            {
                sponsorListDataModels.Add
                (
                    new SponsorListDataModel
                    {
                        AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                        SponsorshipId = long.Parse(sqlDataReader["SponsorshipId"].ToString()),
                        SankalpamDateTime = sqlDataReader["SankalpamDateTime"].ToString(),
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        EmailAddress = sqlDataReader["EmailAddress"].ToString(),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        LastName = sqlDataReader["LastName"].ToString(),
                        PrimaryTelephoneNum = sqlDataReader["PrimaryTelephoneNum"].ToString(),
                        ShoppingCartAmount = float.Parse(sqlDataReader["ShoppingCartAmount"].ToString()),
                        ShoppingCartComments = sqlDataReader["ShoppingCartComments"].ToString(),
                        SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                        SponsorshipGroupDesc = sqlDataReader["SponsorshipGroupDesc"].ToString(),
                        ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                        ItemDesc = sqlDataReader["ItemDesc"].ToString(),
                        ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                        OrderQty = long.Parse(sqlDataReader["OrderQty"].ToString()),
                        OrderAmount = float.Parse(sqlDataReader["OrderAmount"].ToString()),
                        StatusNameDesc = sqlDataReader["StatusNameDesc"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return sponsorListDataModels;
        }
        //GET
        public SponsorshipReportModel SponsorshipReport(string locationNameDesc, string locationNameDesc1, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            WebUtilities webUtilities = new WebUtilities();
            SponsorshipReportModel sponsorshipReportModel = new SponsorshipReportModel();
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sponsorshipReportModel;
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
        //GET
        public List<SponsorshipReportDataModel> SponsorshipRepotData(string locationNameDesc, string locationNameDesc1, string fromDate, string toDate, string emailAddress)
        {
            SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = BuildSqlCommandSponsorshipReportData(locationNameDesc, fromDate, toDate, emailAddress, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<SponsorshipReportDataModel> sponsorshipReportDataModels = new List<SponsorshipReportDataModel>();
            while (sqlDataReader.Read())
            {
                sponsorshipReportDataModels.Add
                (
                    new SponsorshipReportDataModel
                    {
                        AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                        SponsorshipId = long.Parse(sqlDataReader["SponsorshipId"].ToString()),
                        SankalpamDateTime = sqlDataReader["SankalpamDateTime"].ToString(),
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        EmailAddress = sqlDataReader["EmailAddress"].ToString(),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        LastName = sqlDataReader["LastName"].ToString(),
                        PrimaryTelephoneNum = sqlDataReader["PrimaryTelephoneNum"].ToString(),
                        ShoppingCartAmount = float.Parse(sqlDataReader["ShoppingCartAmount"].ToString()),
                        ShoppingCartComments = sqlDataReader["ShoppingCartComments"].ToString(),
                        StatusNameDesc = sqlDataReader["StatusNameDesc"].ToString(),
                        RequestData = sqlDataReader["RequestData"].ToString(),
                        ResponseData = sqlDataReader["ResponseData"].ToString(),
                        //SponsorshipGroupDesc = sqlDataReader["SponsorshipGroupDesc"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return sponsorshipReportDataModels;
        }
        //GET
        public byte[] SponsorListDataDownload(string locationNameDesc, string locationNameDesc1, string sponsorshipGroupId, string fromDate, string toDate, string sankalpamStatus)
        {
            byte[] fileBytes = null;
            //string[] excelColumnNames = { "Id", "Date", "Location", "Email", "First Name", "Last Name", "Tel#", "Amount", "Comments", "Sponsorship" };
            //string[] queryColumnNames = { "SponsorshipId", "AddDateTime", "LocationNameDesc", "EmailAddress", "FirstName", "LastName", "PrimaryTelephoneNum", "ShoppingCartAmount", "ShoppingCartComments" };
            string[] excelColumnNames = { "Id", "Sankalpam", "Date", "Location", "Email", "First Name", "Last Name", "Tel#", "Amount", "Comments", "Sponsorship" };
            string[] queryColumnNames = { "SponsorshipId", "SankalpamDateTime", "AddDateTime", "LocationNameDesc", "EmailAddress", "FirstName", "LastName", "PrimaryTelephoneNum", "ShoppingCartAmount", "ShoppingCartComments" };
            using (var memoryStream = new MemoryStream())
            {
                TextWriter textWriter = new StreamWriter(memoryStream);
                textWriter.Write("\"" + fromDate + " - " + toDate + " - " + DateTime.Now.ToString("MMM dd yyyy hh:mm tt") + "\"");
                textWriter.Write(Environment.NewLine);
                int i;
                i = 0;
                textWriter.Write("\"" + excelColumnNames[i] + "\"");
                for (i = 1; i < excelColumnNames.Length; i++)
                {
                    textWriter.Write("," + "\"" + excelColumnNames[i] + "\"");
                }
                textWriter.Write(Environment.NewLine);
                string sponsorshipId, sponsorshipText, prefixChar;
                SqlConnection sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
                sqlConnection.Open();
                SqlCommand sqlCommand = BuildSqlCommandSponsorListData(locationNameDesc, sponsorshipGroupId, fromDate, toDate, sankalpamStatus, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    i = 0;
                    textWriter.Write("\"" + sqlDataReader[queryColumnNames[i]].ToString() + "\"");
                    for (i = 1; i < queryColumnNames.Length; i++)
                    {
                        textWriter.Write("," + "\"" + sqlDataReader[queryColumnNames[i]].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    sponsorshipId = sqlDataReader["SponsorshipId"].ToString();
                    sponsorshipText = "";
                    prefixChar = "";
                    while (sqlDataReaderRead && sponsorshipId == sqlDataReader["SponsorshipId"].ToString())
                    {
                        sponsorshipText += prefixChar + prefixChar + sqlDataReader["ItemId"].ToString() + ";" + sqlDataReader["SponsorshipGroupDesc"].ToString() + ";" + sqlDataReader["ItemDesc"].ToString() + ";" + sqlDataReader["OrderAmount"].ToString();
                        prefixChar = Environment.NewLine;
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                    textWriter.Write("," + "\"" + sponsorshipText.Replace("\"", "\"\"") + "\"");
                    textWriter.Write(Environment.NewLine);
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                textWriter.Flush();
                memoryStream.Position = 0;
                fileBytes = memoryStream.ToArray();
            }
            return fileBytes;
        }
        //GET
        public SponsorshipModel Sponsorship(string locationNameDesc, string locationNameDesc1, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            WebUtilities webUtilities = new WebUtilities();
            SponsorshipModel sponsorshipModel = new SponsorshipModel
            {
                SponsorModel = new SponsorModel(),
                PaymentModel = new PaymentModel(),
                ResponseObjectModel = webUtilities.InitializeResponseObjectModel(),
            };
            sponsorshipModel.ResponseObjectModel.ListStyleType = "decimal";
            sponsorshipModel.ResponseObjectModel.ResponseMessages = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "Please select items to sponsor and additionally amount for general donation"),
                new KeyValuePair<string, string>("", "Special request / sankalam info enter in \"Other Instructions\""),
                new KeyValuePair<string, string>("", "Enter your email address, credit card info and click \"Make Payment\""),
                new KeyValuePair<string, string>("", "Please check your email for the receipt"),
            };
            sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Info;
            sponsorshipModel.ResponseObjectModel.StatusCode = HttpStatusCode.OK;
            sponsorshipModel.ResponseObjectModel.TextAlign = "left";
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = BuildSqlCommandSponsorshipGroupSponsorshipList(locationNameDesc, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                string sponsorshipGroupId;
                SponsorshipGroupModel sponsorshipGroupModel;
                sponsorshipModel.SponsorModel.SponsorshipGroupModels = new List<SponsorshipGroupModel>();
                while (sqlDataReaderRead)
                {
                    sponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString();
                    sponsorshipModel.SponsorModel.SponsorshipGroupModels.Add
                    (
                        sponsorshipGroupModel = new SponsorshipGroupModel
                        {
                            SponsorshipGroupId = long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            SponsorshipGroupDesc = sqlDataReader["SponsorshipGroupDesc"].ToString(),
                            DisplaySeqNum = float.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            BegEffDate = sqlDataReader["BegEffDate"].ToString(),
                            EndEffDate = sqlDataReader["EndEffDate"].ToString(),
                            SponsorshipListModels = new List<SponsorshipListModel>(),
                        }
                    );
                    while (sqlDataReaderRead && sqlDataReader["SponsorshipGroupId"].ToString() == sponsorshipGroupId)
                    {
                        sponsorshipGroupModel.SponsorshipListModels.Add
                        (
                            new SponsorshipListModel
                            {
                                SponsorshipListId = long.Parse(sqlDataReader["SponsorshipListId"].ToString()),
                                SponsorshipListDesc = sqlDataReader["SponsorshipListDesc"].ToString(),
                                SponsorshipListMinQty = int.Parse(sqlDataReader["SponsorshipListMinQty"].ToString()),
                                SponsorshipListRate = float.Parse(sqlDataReader["SponsorshipListRate"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sponsorshipModel;
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
        //GET
        public SponsorshipModel Sponsorship1(string sponsorshipGroupId, string locationNameDesc, string locationNameDesc1, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            WebUtilities webUtilities = new WebUtilities();
            SponsorshipModel sponsorshipModel = new SponsorshipModel
            {
                SponsorModel = new SponsorModel(),
                PaymentModel = new PaymentModel(),
                ResponseObjectModel = webUtilities.InitializeResponseObjectModel(),
            };
            sponsorshipModel.ResponseObjectModel.ListStyleType = "decimal";
            sponsorshipModel.ResponseObjectModel.ResponseMessages = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "Please select items to sponsor and additionally amount for general donation"),
                new KeyValuePair<string, string>("", "Special request / sankalam info enter in \"Other Instructions\""),
                new KeyValuePair<string, string>("", "Enter your email address, credit card info and click \"Make Payment\""),
                new KeyValuePair<string, string>("", "Please check your email for the receipt"),
            };
            sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Info;
            sponsorshipModel.ResponseObjectModel.StatusCode = HttpStatusCode.OK;
            sponsorshipModel.ResponseObjectModel.TextAlign = "left";
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                if (string.IsNullOrWhiteSpace(sponsorshipGroupId))
                {
                    sponsorshipGroupId = "8";
                }
                SqlCommand sqlCommand = BuildSqlCommandSponsorshipGroupSponsorshipList1(sponsorshipGroupId, locationNameDesc, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                SponsorshipGroupModel sponsorshipGroupModel;
                sponsorshipModel.SponsorModel.SponsorshipGroupModels = new List<SponsorshipGroupModel>();
                while (sqlDataReaderRead)
                {
                    sponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString();
                    sponsorshipModel.SponsorModel.SponsorshipGroupModels.Add
                    (
                        sponsorshipGroupModel = new SponsorshipGroupModel
                        {
                            SponsorshipGroupId = long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            SponsorshipGroupDesc = sqlDataReader["SponsorshipGroupDesc"].ToString(),
                            DisplaySeqNum = float.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            BegEffDate = sqlDataReader["BegEffDate"].ToString(),
                            EndEffDate = sqlDataReader["EndEffDate"].ToString(),
                            SponsorshipListModels = new List<SponsorshipListModel>(),
                        }
                    );
                    while (sqlDataReaderRead && sqlDataReader["SponsorshipGroupId"].ToString() == sponsorshipGroupId)
                    {
                        sponsorshipGroupModel.SponsorshipListModels.Add
                        (
                            new SponsorshipListModel
                            {
                                SponsorshipListId = long.Parse(sqlDataReader["SponsorshipListId"].ToString()),
                                SponsorshipListDesc = sqlDataReader["SponsorshipListDesc"].ToString(),
                                SponsorshipListMinQty = int.Parse(sqlDataReader["SponsorshipListMinQty"].ToString()),
                                SponsorshipListRate = float.Parse(sqlDataReader["SponsorshipListRate"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sponsorshipModel;
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
        //POST
        public void Sponsorship(string locationNameDesc, string locationNameDesc1, SponsorshipModel sponsorshipModel, string execUniqueId)
        {
            //Validate shopping cart
            //Process Shopping Cart & Credit Card
            //Create Sponsorship
            //Send email
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            WebUtilities webUtilities = new WebUtilities();
            sponsorshipModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel(sponsorshipModel.ResponseObjectModel);
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                //Validate shopping cart
                if (ValidateShoppingCart(sponsorshipModel, execUniqueId))
                {
                    //Create Shopping Cart
                    ShoppingCartBL shoppingCartBL = new ShoppingCartBL();
                    ShoppingCart shoppingCart = new ShoppingCart
                    {
                        EmailAddress = sponsorshipModel.PaymentModel.EmailAddress,
                        FirstName = sponsorshipModel.PaymentModel.FirstName,
                        LastName = sponsorshipModel.PaymentModel.LastName,
                        PrimaryTelephoneNum = sponsorshipModel.PaymentModel.PrimaryTelephoneNum,
                        ShoppingCartAmount = sponsorshipModel.PaymentModel.SponsorshipAmount,
                        ShoppingCartComments = sponsorshipModel.PaymentModel.Comments,
                        ShoppingCartItems = new List<ShoppingCartItem>(),
                    };
                    short seqNum = 0;
                    for (int i = 0; i < sponsorshipModel.SponsorModel.SponsorshipGroupModels.Count; i++)
                    {
                        for (int j = 0; j < sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels.Count; j++)
                        {
                            if (sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount != null)
                            {
                                shoppingCart.ShoppingCartItems.Add
                                (
                                    new ShoppingCartItem
                                    {
                                        ItemDesc = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListDesc,
                                        ItemId = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListId,
                                        ItemRate = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListRate,
                                        OrderAmount = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount,
                                        OrderComments = "",
                                        OrderQty = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty,
                                        RateTypeNameDesc = "CHARGE",
                                        SeqNum = seqNum++,
                                    }
                                );
                            }
                        }
                    }
                    if (sponsorshipModel.SponsorModel.OtherSponsorshipAmount > 0)
                    {
                        if (string.IsNullOrWhiteSpace(sponsorshipModel.SponsorModel.OtherSponsorshipDescription))
                        {
                            sponsorshipModel.SponsorModel.OtherSponsorshipDescription = "Other Sponsorship";
                        }
                        shoppingCart.ShoppingCartItems.Add
                        (
                            new ShoppingCartItem
                            {
                                ItemDesc = sponsorshipModel.SponsorModel.OtherSponsorshipDescription,
                                ItemId = 0,
                                ItemRate = sponsorshipModel.SponsorModel.OtherSponsorshipAmount,
                                OrderAmount = sponsorshipModel.SponsorModel.OtherSponsorshipAmount,
                                OrderComments = "",
                                OrderQty = 1,
                                RateTypeNameDesc = "CHARGE",
                                SeqNum = seqNum++,
                            }
                        );
                    }
                    CreditCardDataBO creditCardDataBO = new CreditCardDataBO
                    {
                        CreditCardAmount = sponsorshipModel.PaymentModel.SponsorshipAmount.ToString(),
                        CreditCardCVV = sponsorshipModel.PaymentModel.CreditCardSecCode,
                        CreditCardExpiryMM = sponsorshipModel.PaymentModel.CreditCardExpiryMM,
                        CreditCardExpiryYYYY = sponsorshipModel.PaymentModel.CreditCardExpiryYYYY,//.Substring(sponsorshipModel.PaymentModel.CreditCardExpiryYYYY.Length - 2),
                        CreditCardHolderName = sponsorshipModel.PaymentModel.NameOnCreditCard,
                        CreditCardNumber = sponsorshipModel.PaymentModel.CreditCardNumber,
                        Currency = "USD",
                    };
                    sponsorshipModel.PaymentModel.CreditCardProcessStatus = shoppingCartBL.ProcessShoppingCart(shoppingCart, creditCardDataBO, null, execUniqueId);
                    if (sponsorshipModel.PaymentModel.CreditCardProcessStatus) //CC Process Success
                    {
                        sponsorshipModel.PaymentModel.UniqeRefId = creditCardDataBO.CreditCardProcessMessage;
                        sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Success;
                        sponsorshipModel.ResponseObjectModel.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        sponsorshipModel.PaymentModel.CreditCardProcessMessage = creditCardDataBO.CreditCardProcessMessage.Replace("\r\n", "<br />").Replace("\n", "<br />");
                        sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                        sponsorshipModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
                        sponsorshipModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", sponsorshipModel.PaymentModel.CreditCardProcessMessage));
                        sponsorshipModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Error while processing credit card"));
                    }
                    string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                    //Register User
                    #region
                    string emailStatus;
                    if (!string.IsNullOrWhiteSpace(sponsorshipModel.PaymentModel.RegiterEmailAddress) && sponsorshipModel.PaymentModel.RegiterEmailAddress.ToUpper() == "ON")
                    {
                        string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                        sqlConnection = new SqlConnection(databaseConnectionString);
                        sqlConnection.Open();
                        SqlCommand sqlCommand = BuildSqlCommandLoginUserSelect(sqlConnection);
                        sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                        sqlCommand.Parameters["@LoginNameId1"].Value = sponsorshipModel.PaymentModel.EmailAddress;
                        sqlCommand.Parameters["@LoginNameId2"].Value = "";
                        sqlCommand.Parameters["@LoginNameId3"].Value = "";
                        sqlDataReader = sqlCommand.ExecuteReader();
                        if (sqlDataReader.Read())
                        {
                            emailStatus = "1";
                        }
                        else
                        {
                            sqlDataReader.Close();
                            long? personId = InsertPerson(locationNameDesc, sponsorshipModel.PaymentModel.EmailAddress, sqlConnection, execUniqueId);
                            if (personId != null)
                            {
                                long? loginUserId = InsertLoginUser(locationNameDesc, (long)personId, sponsorshipModel.PaymentModel.EmailAddress, sqlConnection, execUniqueId);
                                if (loginUserId != null)
                                {
                                    DateTime resetPasswordDateTime = DateTime.Now.AddDays(3);
                                    Random random = new Random();
                                    string randomNumber1 = random.Next(0, 999999999).ToString();
                                    string randomNumber2 = random.Next(0, 999999999).ToString();
                                    string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                                    string resetPasswordQueryString = personId + "_" + loginUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
                                    string resetPasswordExpiryDateTime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
                                    string resetPasswordKey = GenerateRandomKey(8);
                                    UpdateLoginUserResetPassword((long)loginUserId, resetPasswordQueryString, resetPasswordExpiryDateTime, resetPasswordKey, privateKey, sqlConnection, execUniqueId);
                                    emailStatus = "0";
                                }
                                else
                                {
                                    emailStatus = "9";
                                }
                            }
                            else
                            {
                                emailStatus = "9";
                            }
                        }
                    }
                    else
                    {
                        emailStatus = "";
                    }
                    #endregion
                    //Insert Sponsorship
                    long sponsorshipId = SponsorshipInsertRow(locationNameDesc, -1, shoppingCart.ShoppingCartId);
                    sponsorshipModel.SponsorshipId = sponsorshipId;
                    //Send out email
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before SponsorshipReceiptEmailKeywordValues");
                    Dictionary<string, string> emailTemplateKeywordValues = SponsorshipReceiptEmailKeywordValues(locationNameDesc, locationNameDesc1, sponsorshipId, sponsorshipModel, emailStatus, execUniqueId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before SponsorShipRecieptPDF");
                    //Convert to Pdf
                    SponsorShipRecieptPDF(sponsorshipId, sponsorshipModel.SponsorshipReceipt);
                    List<string> emailAttachmentFileNames = null;
                    emailAttachmentFileNames = new List<string>();
                    //string documentsImagesDirectoryName = @"C:\Code\Dev\SVCCTemple\SVCCTempleApplicationNew\SVCCTempleWeb\Documents\SponsorshipReceipts\";
                    string documentsImagesDirectoryName = HttpContext.Current.Server.MapPath("~/Documents/SponsorshipReceipts/") + @"\";
                    string emailAttachmentFileName = documentsImagesDirectoryName + @"\SponsorShipReciept_" + sponsorshipId + ".pdf";
                    emailAttachmentFileNames.Add(emailAttachmentFileName);
                    sponsorshipModel.DownloadFileName = Guid.NewGuid().ToString() + ".pdf";
                    string downloadFileName = HttpContext.Current.Server.MapPath("~/Documents/TempDownload/") + @"\" + sponsorshipModel.DownloadFileName;
                    File.Copy(emailAttachmentFileName, downloadFileName);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before SendEmail");
                    SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, emailAttachmentFileNames, execUniqueId);
                }
                else
                {
                    sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                    sponsorshipModel.ResponseObjectModel.StatusCode = HttpStatusCode.InternalServerError;
                    sponsorshipModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "One or more validation errors"));
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                sponsorshipModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Error occurred during processing. Please contact support"));
                throw;
            }
        }
        ////GET
        //public UpdatePasswordModel UpdatePassword(string locationNameDesc, string locationNameDesc1, string resetPasswordQueryString, string execUniqueId)
        //{
        //    //1. Extract LoginUserId, PersonId
        //    //2. Select record from LoginUser, Person
        //    //3. If not found - error condition
        //    //4. If exists and expiry, completed etc. not valid - error condition
        //    //5. Update Password good to go
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    SqlConnection sqlConnection = null;
        //    SqlDataReader sqlDataReader = null;
        //    try
        //    {
        //        int indexOf1, indexOf2;
        //        string personId, loginUserId;
        //        bool success;
        //        indexOf1 = resetPasswordQueryString.IndexOf("_", 0);
        //        personId = resetPasswordQueryString.Substring(0, indexOf1);
        //        indexOf2 = resetPasswordQueryString.IndexOf("_", indexOf1 + 1);
        //        loginUserId = resetPasswordQueryString.Substring(indexOf1 + 1, indexOf2 - indexOf1 - 1);
        //        string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        //        sqlConnection = new SqlConnection(databaseConnectionString);
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = new SqlCommand("SELECT LoginUser.* FROM LoginUser INNER JOIN Person ON LoginUser.LocationNameDesc = Person.LocationNameDesc AND LoginUser.PersonId = Person.PersonId WHERE LoginUser.LocationNameDesc = '" + locationNameDesc + "' AND Person.PersonId = " + personId + " AND LoginUser.LoginUserId = " + loginUserId, sqlConnection);
        //        sqlDataReader = sqlCommand.ExecuteReader();
        //        if (sqlDataReader.Read())
        //        {
        //            success = true;
        //        }
        //        else
        //        {
        //            success = false;
        //        }
        //        sqlDataReader.Close();
        //        sqlConnection.Close();
        //        UpdatePasswordModel updatePasswordModel;
        //        if (success)
        //        {
        //            updatePasswordModel = new UpdatePasswordModel
        //            {
        //                ResetPasswordQueryString = resetPasswordQueryString,
        //                ResponseObjectModel0 = new ResponseObjectModel0
        //                {
        //                    ResponseMessages = new List<KeyValuePair<string, string>>
        //                    {
        //                        new KeyValuePair<string, string>("", "Please enter email address, login password, confirm login password and password key"),
        //                        new KeyValuePair<string, string>("", "Password should be 9 to 18 characters long"),
        //                        new KeyValuePair<string, string>("", "Password should contain upper case A to Z"),
        //                        new KeyValuePair<string, string>("", "Password should contain lower case a to z"),
        //                        new KeyValuePair<string, string>("", "Password should contain number 0 to 9"),
        //                        new KeyValuePair<string, string>("", "Password should contain special characters !#$%^&*()"),
        //                        new KeyValuePair<string, string>("", "Click UPDATE PASSWORD & check your email"),
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                    TextAlign = "left",
        //                    ListStyleType = "decimal",
        //                    StatusCode = HttpStatusCode.OK,
        //                }
        //            };
        //        }
        //        else
        //        {
        //            updatePasswordModel = new UpdatePasswordModel
        //            {
        //                ResetPasswordQueryString = resetPasswordQueryString,
        //                ResponseObjectModel = new ResponseObjectModel0
        //                {
        //                    ResponseMessages = new List<KeyValuePair<string, string>>
        //                    {
        //                        new KeyValuePair<string, string>("", "Invalid update password request"),
        //                        new KeyValuePair<string, string>("", "Please try again"),
        //                        new KeyValuePair<string, string>("", "If problem exists, request resetting your password"),
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Error,
        //                    TextAlign = "left",
        //                    ListStyleType = "decimal",
        //                    StatusCode = HttpStatusCode.OK,
        //                }
        //            };
        //        }
        //        return updatePasswordModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            sqlDataReader.Close();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //        try
        //        {
        //            sqlConnection.Close();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //    }
        //}
        ////POST
        //public void UpdatePassword(string locationNameDesc, string locationNameDesc1, ref UpdatePasswordModel updatePasswordModel, string execUniqueId)
        //{
        //    updatePasswordModel.ResponseObjectModel = new ResponseObjectModel0
        //    {
        //        ListStyleType = "decimal",
        //        ResponseMessages = new List<KeyValuePair<string, string>>
        //        {
        //           new KeyValuePair<string, string>("", "Your password has been updated successfully!!!"),
        //           new KeyValuePair<string, string>("", "If you did not intend to do this, please contact support personnel"),
        //           new KeyValuePair<string, string>("", "You will receive a confirmation email of your action"),
        //           //new KeyValuePair<string, string>("", ""),
        //        },
        //        ResponseTypeId = ResponseTypeEnum.Success,
        //        TextAlign = "left",
        //    };
        //}
        public bool ValidateShoppingCart(SponsorshipModel sponsorshipModel, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            int i, j;
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT SponsorshipGroupDesc, SponsorshipList.* FROM SponsorshipList INNER JOIN SponsorshipGroup ON SponsorshipGroup.SponsorshipGroupId = SponsorshipList.SponsorshipGroupId WHERE SponsorshipList.SponsorshipListId = @SponsorshipListId", sqlConnection);
                sqlCommand.Parameters.Add("@SponsorshipListId", SqlDbType.BigInt);
                float sponsorshipAmount = 0;
                for (i = 0; i < sponsorshipModel.SponsorModel.SponsorshipGroupModels.Count; i++)
                {
                    for (j = 0; j < sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels.Count; j++)
                    {
                        if (sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty != null && sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty != 0)
                        {
                            sqlCommand.Parameters["@SponsorshipListId"].Value = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListId;
                            sqlDataReader = sqlCommand.ExecuteReader();
                            if (sqlDataReader.Read())
                            {
                                sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipGroupDesc = sqlDataReader["SponsorshipGroupDesc"].ToString();
                                sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListDesc = sqlDataReader["SponsorshipListDesc"].ToString();
                                sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListRate = float.Parse(sqlDataReader["SponsorshipListRate"].ToString());
                                sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount = sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty * sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListRate;

                                sponsorshipAmount += sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount.Value;
                            }
                            sqlDataReader.Close();
                        }
                    }
                }
                sponsorshipModel.PaymentModel.SponsorshipAmount = sponsorshipAmount;
                try
                {
                    if (sponsorshipModel.SponsorModel.OtherSponsorshipAmount.Value > 0)
                    {
                        sponsorshipModel.PaymentModel.SponsorshipAmount += sponsorshipModel.SponsorModel.OtherSponsorshipAmount.Value;
                    }
                }
                catch
                {
                    ;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00990000 :: Error", exception);
            }
            finally
            {
                try
                {
                    sqlDataReader.Close();
                }
                catch
                {

                }
                try
                {
                    sqlConnection.Close();
                }
                catch
                {

                }
            }
            List<string> errorMessages = new List<string>();
            int qtyAvailable = 0, amountAvailable = 0, qtyAmountAvailable = 0;
            for (i = 0; i < sponsorshipModel.SponsorModel.SponsorshipGroupModels.Count; i++)
            {
                for (j = 0; j < sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels.Count; j++)
                {
                    if (sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty != null)
                    {
                        qtyAvailable++;
                    }
                    if (sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount != null)
                    {
                        amountAvailable++;
                    }
                    if (sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty != null &&
                        sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount != null)
                    {
                        qtyAmountAvailable++;
                    }
                }
            }
            try
            {
                var x = sponsorshipModel.SponsorModel.OtherSponsorshipAmount.ToString();
                sponsorshipModel.SponsorModel.OtherSponsorshipAmount = float.Parse(x);
                if (sponsorshipModel.SponsorModel.OtherSponsorshipAmount < 0)
                {
                    sponsorshipModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Invalid other sponsorship amount"));
                }
                if (sponsorshipModel.SponsorModel.OtherSponsorshipAmount > 0)
                {
                    qtyAvailable++;
                    amountAvailable++;
                    qtyAmountAvailable++;
                }
                try
                {
                    var y = sponsorshipModel.SponsorModel.OtherSponsorshipDescription.Length;
                    if (y <= 0)
                    {
                        sponsorshipModel.SponsorModel.OtherSponsorshipDescription = "General Sponsorship";
                    }
                }
                catch
                {
                    sponsorshipModel.SponsorModel.OtherSponsorshipDescription = "General Sponsorship";
                }
            }
            catch
            {
            }
            bool returnValue;
            if (qtyAmountAvailable == 0)
            {
                sponsorshipModel.ResponseObjectModel.ResponseMessages.Add(new KeyValuePair<string, string>("", "Please select one or more items for sponsorship"));
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return returnValue;
        }
        public void SendEmailForEmailBlast(string fromEmailAddress, string fromEmailAddressDisplayName, string toEmailAddress, string bccEmailAddresss, string emailSubject, string emailBody, string execUniqueId)
        {
            string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
            EmailService emailService = new EmailService();
            string emailDirectoryName = HttpContext.Current.Application["EmailDirectoryName"].ToString();
            var fromEmailAddressKVP = new KeyValuePair<string, string>(fromEmailAddress, fromEmailAddressDisplayName);
            var toEmailAddresses = new List<KeyValuePair<string, string>>();
            toEmailAddresses.Add(new KeyValuePair<string, string>(toEmailAddress, ""));
            //toEmailAddresses.Add(new KeyValuePair<string, string>(fromEmailAddress, ""));
            List<KeyValuePair<string, string>> replyToEmailAddresses = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(fromEmailAddress, fromEmailAddressDisplayName),
            };
            List<KeyValuePair<string, string>> bccEmailAddressKVP;
            if (string.IsNullOrWhiteSpace(bccEmailAddresss))
            {
                bccEmailAddressKVP = null;
            }
            else
            {
                bccEmailAddressKVP = new List<KeyValuePair<string, string>>();
                foreach (var bccEmailAddress in bccEmailAddresss.Split(';'))
                {
                    bccEmailAddressKVP.Add(new KeyValuePair<string, string>(bccEmailAddress, ""));
                }
            }
            emailService.SendEmail(emailDirectoryName, "", fromEmailAddressKVP, emailSubject, emailBody, toEmailAddresses, execUniqueId, privateKey, replyToEmailAddresses, null, bccEmailAddressKVP, null, true);
        }
        private long? InsertPerson(string locationNameDesc, RegisterUserProfModel registerUserProfModel, SqlConnection sqlConnection, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long? personId = null;
                SqlCommand sqlCommand = BuildSqlCommadPersonInsert(sqlConnection);

                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@SalutationId"].Value = "12";
                sqlCommand.Parameters["@FirstName"].Value = "";
                sqlCommand.Parameters["@MiddleName"].Value = "";
                sqlCommand.Parameters["@LastName"].Value = "";
                sqlCommand.Parameters["@SuffixId"].Value = "17";
                sqlCommand.Parameters["@DateOfBirth"].Value = "1900-01-01";
                sqlCommand.Parameters["@PrimaryAddressId"].Value = "0";
                sqlCommand.Parameters["@PrimaryTelephoneNum"].Value = "0";
                sqlCommand.Parameters["@PrimaryTelephoneExtn"].Value = "";
                sqlCommand.Parameters["@PrimaryEmailAddress"].Value = registerUserProfModel.RegisterEmailAddress;
                sqlCommand.Parameters["@AlternateAddressId"].Value = "0";
                sqlCommand.Parameters["@AlternateTelephoneNum"].Value = "0";
                sqlCommand.Parameters["@AlternateTelephoneExtn"].Value = "";
                sqlCommand.Parameters["@AlternateEmailAddress"].Value = "";

                personId = (long)sqlCommand.ExecuteScalar();

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        private long? InsertPerson(string locationNameDesc, string registerEmailAddress, SqlConnection sqlConnection, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long? personId = null;
                SqlCommand sqlCommand = BuildSqlCommadPersonInsert(sqlConnection);

                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@SalutationId"].Value = "12";
                sqlCommand.Parameters["@FirstName"].Value = "";
                sqlCommand.Parameters["@MiddleName"].Value = "";
                sqlCommand.Parameters["@LastName"].Value = "";
                sqlCommand.Parameters["@SuffixId"].Value = "17";
                sqlCommand.Parameters["@DateOfBirth"].Value = "1900-01-01";
                sqlCommand.Parameters["@PrimaryAddressId"].Value = "0";
                sqlCommand.Parameters["@PrimaryTelephoneNum"].Value = "0";
                sqlCommand.Parameters["@PrimaryTelephoneExtn"].Value = "";
                sqlCommand.Parameters["@PrimaryEmailAddress"].Value = registerEmailAddress;
                sqlCommand.Parameters["@AlternateAddressId"].Value = "0";
                sqlCommand.Parameters["@AlternateTelephoneNum"].Value = "0";
                sqlCommand.Parameters["@AlternateTelephoneExtn"].Value = "";
                sqlCommand.Parameters["@AlternateEmailAddress"].Value = "";

                personId = (long)sqlCommand.ExecuteScalar();

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        private long? InsertLoginUser(string locationNameDesc, long personId, RegisterUserProfModel registerUserProfModel, SqlConnection sqlConnection, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long? loginUserId = null;

                SqlCommand sqlCommand = BuildSqlCommadLoginUserInsert(sqlConnection);

                sqlCommand.Parameters["@LoginNameId1"].Value = registerUserProfModel.RegisterEmailAddress;
                sqlCommand.Parameters["@LoginNameId2"].Value = "";
                sqlCommand.Parameters["@LoginNameId3"].Value = "";
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@LoginTypeNameDesc"].Value = "EMAIL_ADDRESS";
                sqlCommand.Parameters["@zzz_LoginPassword"].Value = "";
                sqlCommand.Parameters["@LoginPassword"].Value = "";
                sqlCommand.Parameters["@PasswordExpiryDate"].Value = "1900-01-01";
                sqlCommand.Parameters["@UserTypeId"].Value = "8";
                sqlCommand.Parameters["@UserStatusId"].Value = "9";
                sqlCommand.Parameters["@PersonId"].Value = personId;

                loginUserId = (long)sqlCommand.ExecuteScalar();

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return loginUserId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        private long? InsertLoginUser(string locationNameDesc, long personId, string registerEmailAddress, SqlConnection sqlConnection, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long? loginUserId = null;

                SqlCommand sqlCommand = BuildSqlCommadLoginUserInsert(sqlConnection);

                sqlCommand.Parameters["@LoginNameId1"].Value = registerEmailAddress;
                sqlCommand.Parameters["@LoginNameId2"].Value = "";
                sqlCommand.Parameters["@LoginNameId3"].Value = "";
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@LoginTypeNameDesc"].Value = "EMAIL_ADDRESS";
                sqlCommand.Parameters["@zzz_LoginPassword"].Value = "";
                sqlCommand.Parameters["@LoginPassword"].Value = "";
                sqlCommand.Parameters["@PasswordExpiryDate"].Value = "1900-01-01";
                sqlCommand.Parameters["@UserTypeId"].Value = "8";
                sqlCommand.Parameters["@UserStatusId"].Value = "9";
                sqlCommand.Parameters["@PersonId"].Value = personId;

                loginUserId = (long)sqlCommand.ExecuteScalar();

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return loginUserId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        public void InsertContactUs(string locationNameDesc, ContactUsModel contactUsModel, SqlConnection sqlConnection, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommadContactUsInsert(sqlConnection);

                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@ContactUsTypeId"].Value = contactUsModel.ContactUsTypeId;
                sqlCommand.Parameters["@FirstName"].Value = contactUsModel.FirstName;
                sqlCommand.Parameters["@LastName"].Value = contactUsModel.LastName;
                sqlCommand.Parameters["@EmailAddress"].Value = contactUsModel.EmailAddress;
                sqlCommand.Parameters["@TelephoneNum"].Value = contactUsModel.TelephoneNumber;
                sqlCommand.Parameters["@Comments"].Value = contactUsModel.Comments;

                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        private bool UpdateLoginUserResetPassword(long loginUserId, string resetPasswordQueryString, string resetPasswordExpiryDateTime, string resetPasswordKey, string privateKey, SqlConnection sqlConnection, string execUniqueId)
        {
            bool returnValue = false;
            SqlCommand sqlCommand = new SqlCommand("UPDATE LoginUser SET ResetPasswordCompletedDateTime = NULL, ResetPasswordQueryString = @ResetPasswordQueryString, ResetPasswordExpiryDateTime = @ResetPasswordExpiryDateTime, ResetPasswordKey = @ResetPasswordKey, UpdDateTime = SYSDATETIME()  WHERE LoginUserId = @LoginUserId", sqlConnection);
            sqlCommand.Parameters.Add("@ResetPasswordQueryString", SqlDbType.NVarChar, 1000);
            sqlCommand.Parameters.Add("@ResetPasswordExpiryDateTime", SqlDbType.VarChar, 100);
            sqlCommand.Parameters.Add("@ResetPasswordKey", SqlDbType.NVarChar, 200);
            sqlCommand.Parameters.Add("@LoginUserId", SqlDbType.BigInt);

            //resetPasswordQueryString = EncryptDecrypt.EncryptDataMd5(loginUserId + DateTime.Now.ToString("yyyyMMddHHmmssfffff"), privateKey);
            sqlCommand.Parameters["@ResetPasswordQueryString"].Value = resetPasswordQueryString;
            sqlCommand.Parameters["@ResetPasswordExpiryDateTime"].Value = resetPasswordExpiryDateTime;
            sqlCommand.Parameters["@ResetPasswordKey"].Value = resetPasswordKey;
            sqlCommand.Parameters["@LoginUserId"].Value = loginUserId;
            sqlCommand.ExecuteNonQuery();
            returnValue = true;

            return returnValue;
        }
        private void SendEmail(string locationNameDesc, string locationNameDesc1, Dictionary<string, string> emailTemplateKeywordValues, string privateKey, List<string> emailAttachmentFileNames, string execUniqueId)
        {
            EmailService emailService = new EmailService();
            string emailDirectoryName = HttpContext.Current.Application["EmailDirectoryName"].ToString();
            var toEmailAddresses = new List<KeyValuePair<string, string>>();
            toEmailAddresses.Add(new KeyValuePair<string, string>(emailTemplateKeywordValues["ToEmailAddress"], ""));
            toEmailAddresses.Add(new KeyValuePair<string, string>(emailTemplateKeywordValues["FromEmailAddress"], ""));
            var fromEmailAddress = new KeyValuePair<string, string>(emailTemplateKeywordValues["FromEmailAddress"], emailTemplateKeywordValues["FromEmailDisplayName"]);
            List<KeyValuePair<string, string>> replyToEmailAddresses = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(emailTemplateKeywordValues["FromEmailAddress"], emailTemplateKeywordValues["FromEmailDisplayName"]),
            };
            List<KeyValuePair<string, string>> bccEmailAddresss = new List<KeyValuePair<string, string>>();
            foreach (var bccEmailAddress in emailTemplateKeywordValues["EmailBccEmailAddresss"].Split(';'))
            {
                bccEmailAddresss.Add(new KeyValuePair<string, string>(bccEmailAddress, ""));
            }
            emailService.SendEmail(emailDirectoryName, "", fromEmailAddress, emailTemplateKeywordValues["EMAIL_SUBJECT"], emailTemplateKeywordValues["EMAIL_BODY"], toEmailAddresses, execUniqueId, privateKey, replyToEmailAddresses, null, bccEmailAddresss, emailAttachmentFileNames, true);
        }
        private Dictionary<string, string> ContactUsEmailKeywordValues(string locationNameDesc, string locationNameDesc1, ContactUsModel contactUsModel, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\ContactUsEmailTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd() + " (" + DateTime.Now.ToString("MMM dd yyyy hh:mm:ss tt - ddd") + ")";
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\ContactUsEmailTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            emailBody = emailBody.Replace("@@##BaseUrl##@@", HttpContext.Current.Application[locationNameDesc1 + "BaseUrl"].ToString());
            emailBody = emailBody.Replace("@@##FirstName##@@", contactUsModel.FirstName);
            emailBody = emailBody.Replace("@@##LastName##@@", contactUsModel.LastName);
            emailBody = emailBody.Replace("@@##EmailAddress##@@", contactUsModel.EmailAddress);
            try
            {
                emailBody = emailBody.Replace("@@##TelephoneNum##@@", long.Parse(contactUsModel.TelephoneNumber).ToString("(###) ###-####"));
            }
            catch
            {
                emailBody = emailBody.Replace("@@##TelephoneNum##@@", contactUsModel.TelephoneNumber);
            }
            emailBody = emailBody.Replace("@@##CommentsRequests##@@", contactUsModel.Comments);
            emailBody = emailBody.Replace("@@##TempleName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleName"].ToString());
            emailBody = emailBody.Replace("@@##SignatureTemplate##@@", SignatureTemplate(locationNameDesc1, templatesDirectoryName));
            string bccEmailAddresss;
            try
            {
                bccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
            }
            catch
            {
                bccEmailAddresss = "";
            }
            Dictionary<string, string> registerEmailTemplateKeywordValues = new Dictionary<string, string>()
            {
                { "EMAIL_SUBJECT", emailSubject },
                { "EMAIL_BODY", emailBody },
                { "FromEmailAddress", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString() },
                { "FromEmailDisplayName", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString() },
                { "ToEmailAddress", contactUsModel.EmailAddress },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            return registerEmailTemplateKeywordValues;
        }
        private Dictionary<string, string> RegisterEmailKeywordValues(string locationNameDesc, string locationNameDesc1, string registerEmailAddress, string resetPasswordQueryString, DateTime resetPasswordDateTime, string resetPasswordKey, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\RegisterEmailTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd() + " (" + DateTime.Now.ToString("MMM dd yyyy hh:mm:ss tt - ddd") + ")";
            emailSubject = emailSubject.Replace("@@##RegisterEmailAddress##@@", registerEmailAddress);
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\RegisterEmailTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            emailBody = emailBody.Replace("@@##BaseUrl##@@", HttpContext.Current.Application[locationNameDesc1 + "BaseUrl"].ToString());
            emailBody = emailBody.Replace("@@##RegisterEmailAddress##@@", registerEmailAddress);
            emailBody = emailBody.Replace("@@##TempleName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleName"].ToString());
            emailBody = emailBody.Replace("@@##UpdatePasswordUrl##@@", HttpContext.Current.Application["UpdatePasswordUrl"].ToString());
            emailBody = emailBody.Replace("@@##ResetPasswordQueryString##@@", resetPasswordQueryString);
            emailBody = emailBody.Replace("@@##ResetPasswordKey##@@", resetPasswordKey);
            emailBody = emailBody.Replace("@@##SignatureTemplate##@@", SignatureTemplate(locationNameDesc1, templatesDirectoryName));
            string bccEmailAddresss;
            try
            {
                bccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
            }
            catch
            {
                bccEmailAddresss = "";
            }
            Dictionary<string, string> registerEmailTemplateKeywordValues = new Dictionary<string, string>()
            {
                { "EMAIL_SUBJECT", emailSubject },
                { "EMAIL_BODY", emailBody },
                { "FromEmailAddress", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString() },
                { "FromEmailDisplayName", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString() },
                { "ToEmailAddress", registerEmailAddress },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            return registerEmailTemplateKeywordValues;
        }
        private Dictionary<string, string> ResetPasswordKeywordValues(string locationNameDesc, string locationNameDesc1, string resetPasswordEmailAddress, string resetPasswordQueryString, DateTime resetPasswordDateTime, string resetPasswordKey, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\ResetPasswordEmailTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd() + " (" + DateTime.Now.ToString("MMM dd yyyy hh:mm:ss tt - ddd") + ")";
            emailSubject = emailSubject.Replace("@@##ResetPasswordEmailAddress##@@", resetPasswordEmailAddress);
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\ResetPasswordEmailTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            emailBody = emailBody.Replace("@@##BaseUrl##@@", HttpContext.Current.Application[locationNameDesc1 + "BaseUrl"].ToString());
            emailBody = emailBody.Replace("@@##ResetPasswordEmailAddress##@@", resetPasswordEmailAddress);
            emailBody = emailBody.Replace("@@##TempleName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleName"].ToString());
            emailBody = emailBody.Replace("@@##UpdatePasswordUrl##@@", HttpContext.Current.Application["UpdatePasswordUrl"].ToString());
            emailBody = emailBody.Replace("@@##ResetPasswordQueryString##@@", resetPasswordQueryString);
            emailBody = emailBody.Replace("@@##ResetPasswordKey##@@", resetPasswordKey);
            emailBody = emailBody.Replace("@@##SignatureTemplate##@@", SignatureTemplate(locationNameDesc1, templatesDirectoryName));
            string bccEmailAddresss;
            try
            {
                bccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
            }
            catch
            {
                bccEmailAddresss = "";
            }
            Dictionary<string, string> registerEmailTemplateKeywordValues = new Dictionary<string, string>()
            {
                { "EMAIL_SUBJECT", emailSubject },
                { "EMAIL_BODY", emailBody },
                { "FromEmailAddress", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString() },
                { "FromEmailDisplayName", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString() },
                { "ToEmailAddress", resetPasswordEmailAddress },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            return registerEmailTemplateKeywordValues;
        }
        private Dictionary<string, string> UpdatePasswordKeywordValues(string locationNameDesc, string locationNameDesc1, string emailAddress, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\UpdatePasswordEmailTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd() + " (" + DateTime.Now.ToString("MMM dd yyyy hh:mm:ss tt - ddd") + ")";
            emailSubject = emailSubject.Replace("@@##UpdatePasswordEmailAddress##@@", emailAddress);
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\UpdatePasswordEmailTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            emailBody = emailBody.Replace("@@##BaseUrl##@@", HttpContext.Current.Application[locationNameDesc1 + "BaseUrl"].ToString());
            emailBody = emailBody.Replace("@@##UpdatePasswordEmailAddress##@@", emailAddress);
            emailBody = emailBody.Replace("@@##TempleName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleName"].ToString());
            emailBody = emailBody.Replace("@@##SignatureTemplate##@@", SignatureTemplate(locationNameDesc1, templatesDirectoryName));
            string bccEmailAddresss;
            try
            {
                bccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
            }
            catch
            {
                bccEmailAddresss = "";
            }
            Dictionary<string, string> registerEmailTemplateKeywordValues = new Dictionary<string, string>()
            {
                { "EMAIL_SUBJECT", emailSubject },
                { "EMAIL_BODY", emailBody },
                { "FromEmailAddress", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString() },
                { "FromEmailDisplayName", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString() },
                { "ToEmailAddress", emailAddress },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            return registerEmailTemplateKeywordValues;
        }
        private Dictionary<string, string> SponsorshipReceiptEmailKeywordValues(string locationNameDesc, string locationNameDesc1, long sponsorshipId, SponsorshipModel sponsorshipModel, string registerStatus, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\SponsorshipReceiptEmailTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd() + sponsorshipId;
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\SponsorshipReceiptEmailTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            string creditCardNumber, sponsorshipItems = "", sponsorshipItemsTemp, sponsorshipItemsTemplate;
            streamReader = new StreamReader(templatesDirectoryName + @"\SponsorshipItemTemplate.html");
            sponsorshipItemsTemplate = streamReader.ReadToEnd();
            streamReader.Close();
            int seqNum = 0;
            for (int i = 0; i < sponsorshipModel.SponsorModel.SponsorshipGroupModels.Count; i++)
            {
                for (int j = 0; j < sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels.Count; j++)
                {
                    if (sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount != null)
                    {
                        seqNum++;
                        sponsorshipItemsTemp = sponsorshipItemsTemplate;
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SeqNum##@@", seqNum.ToString());
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipListId##@@", sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListId.ToString());
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipGroupDesc##@@", sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipGroupDesc);
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipListDesc##@@", sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListDesc);
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorQty##@@", sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderQty.ToString());
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipListRate##@@", sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListRate.ToString("c"));
                        sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorAmount##@@", ((float)sponsorshipModel.SponsorModel.SponsorshipGroupModels[i].SponsorshipListModels[j].SponsorshipListOrderAmount).ToString("c"));
                        sponsorshipItems += sponsorshipItemsTemp;
                    }
                }
            }
            if (sponsorshipModel.SponsorModel.OtherSponsorshipAmount > 0)
            {
                if (string.IsNullOrWhiteSpace(sponsorshipModel.SponsorModel.OtherSponsorshipDescription))
                {
                    sponsorshipModel.SponsorModel.OtherSponsorshipDescription = "Other Sponsorship";
                }
                seqNum++;
                sponsorshipItemsTemp = sponsorshipItemsTemplate;
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SeqNum##@@", seqNum.ToString());
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipListId##@@", "");
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipGroupDesc##@@", sponsorshipModel.SponsorModel.OtherSponsorshipDescription);
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipListDesc##@@", sponsorshipModel.SponsorModel.OtherSponsorshipDescription);
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorQty##@@", "");
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorshipListRate##@@", "");
                sponsorshipItemsTemp = sponsorshipItemsTemp.Replace("@@##SponsorAmount##@@", ((float)sponsorshipModel.SponsorModel.OtherSponsorshipAmount).ToString("c"));
                sponsorshipItems += sponsorshipItemsTemp;
            }
            try
            {
                creditCardNumber = sponsorshipModel.PaymentModel.CreditCardNumber.Substring(sponsorshipModel.PaymentModel.CreditCardNumber.Length - 4);
            }
            catch
            {
                creditCardNumber = "";
            }
            emailBody = emailBody.Replace("@@##SponsorshipId##@@", sponsorshipId.ToString());
            emailBody = emailBody.Replace("@@##EmailAddress##@@", sponsorshipModel.PaymentModel.EmailAddress);
            emailBody = emailBody.Replace("@@##SponsorshipAmount##@@", ((float)sponsorshipModel.PaymentModel.SponsorshipAmount).ToString("c"));
            emailBody = emailBody.Replace("@@##EmailAddress##@@", sponsorshipModel.PaymentModel.EmailAddress);
            emailBody = emailBody.Replace("@@##CreditCardNumber##@@", creditCardNumber);
            emailBody = emailBody.Replace("@@##CreditCardProcessMessage##@@", sponsorshipModel.PaymentModel.CreditCardProcessStatus ? "SUCCESS" : sponsorshipModel.PaymentModel.CreditCardProcessMessage);
            emailBody = emailBody.Replace("@@##UniqeRefId##@@", sponsorshipModel.PaymentModel.UniqeRefId);
            try
            {
                emailBody = emailBody.Replace("@@##ShoppingCartComments##@@", sponsorshipModel.PaymentModel.Comments.Replace("\n", "<br />"));
            }
            catch
            {
                emailBody = emailBody.Replace("@@##ShoppingCartComments##@@", "");
            }
            emailBody = emailBody.Replace("@@##SponsorshipItems##@@", sponsorshipItems);
            emailBody = emailBody.Replace("@@##SignatureTemplate##@@", SignatureTemplate(locationNameDesc1, templatesDirectoryName));
            string bccEmailAddresss;
            try
            {
                bccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
            }
            catch
            {
                bccEmailAddresss = "";
            }
            switch (registerStatus)
            {
                case "":
                    emailBody = emailBody.Replace("@@##RegisterEmailAddress##@@", "");
                    break;
                case "0":
                    emailBody = emailBody.Replace("@@##RegisterEmailAddress##@@", "Email address you used for donation is registered successfully!!!");
                    break;
                case "1":
                    emailBody = emailBody.Replace("@@##RegisterEmailAddress##@@", "Email address is already registered");
                    break;
                case "9":
                default:
                    emailBody = emailBody.Replace("@@##RegisterEmailAddress##@@", "Error while registering email - please contact support personnel");
                    break;
            }
            Dictionary<string, string> sponsorshipEmailTemplateKeywordValues = new Dictionary<string, string>()
            {
                { "EMAIL_SUBJECT", emailSubject },
                { "EMAIL_BODY", emailBody },
                { "FromEmailAddress", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString() },
                { "FromEmailDisplayName", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString() },
                { "ToEmailAddress", sponsorshipModel.PaymentModel.EmailAddress },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            sponsorshipModel.SponsorshipReceipt = emailBody;
            return sponsorshipEmailTemplateKeywordValues;
        }
        private void SponsorShipRecieptPDF(long sponsorShipId, string htmlContent)
        {
            string execUniqueId = "";
            string documentsImagesDirectoryName = HttpContext.Current.Server.MapPath("~/Documents/SponsorshipReceipts/") + @"\";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string dataFullFileName;
                PDFUtility pDFUtility = new PDFUtility();
                dataFullFileName = documentsImagesDirectoryName + @"\SponsorShipReciept_" + sponsorShipId + ".html";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: TemplateHTML", "dataFullFileName", dataFullFileName);
                if (File.Exists(dataFullFileName))
                {
                    File.Delete(dataFullFileName);
                }
                File.WriteAllText(dataFullFileName, htmlContent);
                //Create PDF
                //https://stackoverflow.com/questions/25164257/how-to-convert-html-to-pdf-using-itextsharp
                //The below output looks weird. Try the above
                string pdfFileFullName = documentsImagesDirectoryName + @"\SponsorShipReciept_" + sponsorShipId + ".pdf";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: PDF", "pdfFileFullName", pdfFileFullName);
                if (File.Exists(pdfFileFullName))
                {
                    File.Delete(pdfFileFullName);
                }
                pDFUtility.CreatePDFFileFromHtml(htmlContent, pdfFileFullName, execUniqueId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: After PDFUtility.CreatePDFFileFromHtml");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        private string SignatureTemplate(string locationNameDesc1, string templatesDirectoryName)
        {
            Dictionary<string, string> keywordValues = new Dictionary<string, string>();
            string templateData;
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\SignatureTemplate.html");
            templateData = streamReader.ReadToEnd();
            streamReader.Close();

            templateData = templateData.Replace("@@##BaseUrl##@@", HttpContext.Current.Application[locationNameDesc1 + "BaseUrl"].ToString());
            templateData = templateData.Replace("@@##ContactUsUrl##@@", HttpContext.Current.Application["ContactUsUrl"].ToString());
            templateData = templateData.Replace("@@##TempleName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleName"].ToString());
            templateData = templateData.Replace("@@##TempleFullName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleFullName"].ToString());
            templateData = templateData.Replace("@@##TempleAddressLine1##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleAddressLine1"].ToString());
            templateData = templateData.Replace("@@##TempleCityName##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleCityName"].ToString());
            templateData = templateData.Replace("@@##TempleStateAbbrev##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleStateAbbrev"].ToString());
            templateData = templateData.Replace("@@##TempleZipCode##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleZipCode"].ToString());
            templateData = templateData.Replace("@@##ServerDateTime##@@", DateTime.Now.ToString("MMM-dd-yyyy hh:mm:ss tt"));
            try
            {
                templateData = templateData.Replace("@@##TemplePhone1##@@", long.Parse(HttpContext.Current.Application[locationNameDesc1 + "TemplePhone1"].ToString()).ToString("(###) ###-####"));
            }
            catch
            {
                templateData = templateData.Replace("@@##TemplePhone1##@@", "");
            }
            try
            {
                templateData = templateData.Replace("@@##TemplePhone2##@@", long.Parse(HttpContext.Current.Application[locationNameDesc1 + "TemplePhone2"].ToString()).ToString("(###) ###-####"));
            }
            catch
            {
                templateData = templateData.Replace("@@##TemplePhone2##@@", "");
            }
            try
            {
                templateData = templateData.Replace("@@##TemplePhone3##@@", long.Parse(HttpContext.Current.Application[locationNameDesc1 + "TemplePhone3"].ToString()).ToString("(###) ###-####"));
            }
            catch
            {
                templateData = templateData.Replace("@@##TemplePhone3##@@", "");
            }
            try
            {
                templateData = templateData.Replace("@@##TemplePhone4##@@", long.Parse(HttpContext.Current.Application[locationNameDesc1 + "TemplePhone4"].ToString()).ToString("(###) ###-####"));
            }
            catch
            {
                templateData = templateData.Replace("@@##TemplePhone4##@@", "");
            }
            templateData = templateData.Replace("@@##TempleWebUrl1##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleWebUrl1"].ToString());
            templateData = templateData.Replace("@@##TempleWebUrl2##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleWebUrl2"].ToString());
            try
            {
                templateData = templateData.Replace("@@##TempleWebUrl3##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleWebUrl3"].ToString());
            }
            catch
            {
                templateData = templateData.Replace("@@##TempleWebUrl3##@@", "");
            }
            templateData = templateData.Replace("@@##TempleFacebookUrl##@@", HttpContext.Current.Application[locationNameDesc1 + "TempleFacebookUrl"].ToString());
            templateData = templateData.Replace("@@##FromEmailAddress##@@", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString());

            return templateData;
        }
        private long SponsorshipInsertRow(string locationNameDesc, long personId, long shoppingCartId, string sponsorshipSource = "WEB")
        {
            long sponsorshipId = -1;
            SqlConnection sqlConnection = new SqlConnection(Utilities.GetDatabaseConnectionString("DatabaseConnectionString"));
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("INSERT Sponsorship(LocationNameDesc, PersonId, ShoppingCartId, SponsorshipSource) OUTPUT INSERTED.SponsorshipId SELECT @LocationNameDesc, @PersonId, @ShoppingCartId, @SponsorshipSource", sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ShoppingCartId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@SponsorshipSource", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@PersonId"].Value = personId;
            sqlCommand.Parameters["@ShoppingCartId"].Value = shoppingCartId;
            sqlCommand.Parameters["@SponsorshipSource"].Value = sponsorshipSource;
            //sqlCommand.ExecuteNonQuery();
            sponsorshipId = (long)(sqlCommand.ExecuteScalar());
            sqlConnection.Close();
            return sponsorshipId;
        }
        private SqlCommand BuildSqlCommandKioskOrderSelect(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT KioskGroup.KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.KioskGroupDesc" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.KioskGroupType" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.ItemImageName AS KioskGroup_ItemImageName" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.ItemImageHeight AS KioskGroup_ItemImageHeight" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.ItemImageWidth AS KioskGroup_ItemImageWidth" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.KioskItemId" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE WHEN ISNULL(KioskItem.ItemDesc, '') = '' THEN SponsorshipList.SponsorshipListDesc ELSE KioskItem.ItemDesc END AS ItemDesc" + Environment.NewLine;
            //sqlStmt += "              ,KioskItem.ItemDesc1" + Environment.NewLine;
            //sqlStmt += "              ,KioskItem.ItemDesc2" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.ItemImageName" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.ItemImageHeight" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.ItemImageWidth" + Environment.NewLine;
            sqlStmt += "              ,CASE WHEN KioskItem.ItemRate IS NULL THEN SponsorshipList.SponsorshipListRate ELSE KioskItem.ItemRate END AS ItemRate" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "          FROM KioskGroup" + Environment.NewLine;
            sqlStmt += "    INNER JOIN KioskItem" + Environment.NewLine;
            sqlStmt += "            ON KioskGroup.KioskGroupId = KioskItem.KioskGroupId" + Environment.NewLine;
            sqlStmt += "           AND KioskGroup.LocationNameDesc = KioskItem.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN SponsorshipList" + Environment.NewLine;
            sqlStmt += "            ON KioskItem.SponsorshipListId = SponsorshipList.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "         WHERE KioskGroup.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND @EffDate BETWEEN KioskGroup.BegEffDate AND KioskGroup.EndEffDate" + Environment.NewLine;
            sqlStmt += "           AND KioskGroup.KioskGroupId NOT IN(0, 1000)" + Environment.NewLine;
            sqlStmt += "      ORDER BY" + Environment.NewLine;
            sqlStmt += "               KioskGroup.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.SeqNum" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@EffDate", SqlDbType.NVarChar, 10);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandKioskCheckoutSelect(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT KioskGroup.KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.KioskGroupDesc" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,KioskGroup.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.KioskItemId" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.KioskGroupId" + Environment.NewLine;
            sqlStmt += "              ,CASE WHEN ISNULL(KioskItem.ItemDesc, '') = '' THEN SponsorshipList.SponsorshipListDesc ELSE KioskItem.ItemDesc END AS ItemDesc" + Environment.NewLine;
            //sqlStmt += "              ,KioskItem.ItemDesc1" + Environment.NewLine;
            //sqlStmt += "              ,KioskItem.ItemDesc2" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.ItemImageName" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.ItemImageHeight" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.ItemImageWidth" + Environment.NewLine;
            sqlStmt += "              ,CASE WHEN KioskItem.ItemRate IS NULL THEN SponsorshipList.SponsorshipListRate ELSE KioskItem.ItemRate END AS ItemRate" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "          FROM KioskGroup" + Environment.NewLine;
            sqlStmt += "    INNER JOIN KioskItem" + Environment.NewLine;
            sqlStmt += "            ON KioskGroup.KioskGroupId = KioskItem.KioskGroupId" + Environment.NewLine;
            sqlStmt += "           AND KioskGroup.LocationNameDesc = KioskItem.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN SponsorshipList" + Environment.NewLine;
            sqlStmt += "            ON KioskItem.SponsorshipListId = SponsorshipList.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "         WHERE KioskGroup.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND KioskGroup.KioskGroupId > 0" + Environment.NewLine;
            sqlStmt += "           AND KioskItem.KioskItemId IN(@KioskItemIds)" + Environment.NewLine;
            sqlStmt += "      ORDER BY" + Environment.NewLine;
            sqlStmt += "               KioskGroup.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,KioskItem.SeqNum" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandLoginUserSelect(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT LoginUser.LoginUserId";
            sqlStmt += "              ,LoginUser.PersonId";
            sqlStmt += "              ,LoginUser.UserTypeId";
            sqlStmt += "              ,LoginUser.UserStatusId";
            sqlStmt += "              ,LoginUser.LoginPassword";
            sqlStmt += "              ,LoginUser.LoginNameId1";
            sqlStmt += "              ,LoginUser.PasswordExpiryDate";
            sqlStmt += "              ,UserType.CodeDataNameDesc AS UserTypeNameDesc";
            sqlStmt += "              ,UserType.CodeDataNameId AS UserTypeNameId";
            sqlStmt += "              ,UserStatus.CodeDataNameDesc AS UserStatusNameDesc";
            sqlStmt += "              ,Person.FirstName";
            sqlStmt += "              ,Person.LastName";
            sqlStmt += "          FROM LoginUser";
            sqlStmt += "    INNER JOIN Person";
            sqlStmt += "            ON LoginUser.PersonId = Person.PersonId";
            sqlStmt += "    INNER JOIN CodeData AS UserType";
            sqlStmt += "            ON LoginUser.UserTypeId = UserType.CodeDataId";
            sqlStmt += "    INNER JOIN CodeData AS UserStatus";
            sqlStmt += "            ON LoginUser.UserStatusId = UserStatus.CodeDataId";
            sqlStmt += "         WHERE LoginUser.LocationNameDesc = @LocationNameDesc";
            sqlStmt += "           AND LoginUser.LoginNameId1 = @LoginNameId1";
            sqlStmt += "           AND ISNULL(LoginUser.LoginNameId2, '') = @LoginNameId2";
            sqlStmt += "           AND ISNULL(LoginUser.LoginNameId3, '') = @LoginNameId3";

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId1", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId3", SqlDbType.VarChar);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommadPersonInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "INSERT Person(LocationNameDesc, SalutationId, FirstName, MiddleName, LastName, SuffixId, DateOfBirth, PrimaryAddressId, PrimaryTelephoneNum, PrimaryTelephoneExtn, PrimaryEmailAddress, AlternateAddressId, AlternateTelephoneNum, AlternateTelephoneExtn, AlternateEmailAddress) OUTPUT INSERTED.PersonId VALUES(@LocationNameDesc, @SalutationId, @FirstName, @MiddleName, @LastName, @SuffixId, @DateOfBirth, @PrimaryAddressId, @PrimaryTelephoneNum, @PrimaryTelephoneExtn, @PrimaryEmailAddress, @AlternateAddressId, @AlternateTelephoneNum, @AlternateTelephoneExtn, @AlternateEmailAddress)";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@SalutationId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@SuffixId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@DateOfBirth", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryAddressId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryTelephoneNum", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryTelephoneExtn", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryEmailAddress", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateAddressId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateTelephoneNum", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateTelephoneExtn", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateEmailAddress", SqlDbType.VarChar);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommadLoginUserInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "INSERT LoginUser(LoginNameId1, LoginNameId2, LoginNameId3, LocationNameDesc, LoginTypeNameDesc, zzz_LoginPassword, LoginPassword, PasswordExpiryDate, UserTypeId, UserStatusId, PersonId) OUTPUT INSERTED.LoginUserId VALUES(@LoginNameId1, @LoginNameId2, @LoginNameId3, @LocationNameDesc, @LoginTypeNameDesc, @zzz_LoginPassword, @LoginPassword, @PasswordExpiryDate, @UserTypeId, @UserStatusId, @PersonId)";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LoginNameId1", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId3", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginTypeNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@zzz_LoginPassword", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginPassword", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PasswordExpiryDate", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserTypeId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserStatusId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.VarChar);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandSponsorshipGroupSponsorshipList(string locationNameDesc, SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT SponsorshipGroup.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.SponsorshipGroupDesc" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.DisplaySeqNum" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.BegEffDate" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.EndEffDate" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListDesc" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListMinQty" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListRate" + Environment.NewLine;
            sqlStmt += "          FROM SponsorshipGroup" + Environment.NewLine;
            sqlStmt += "    INNER JOIN SponsorshipList" + Environment.NewLine;
            sqlStmt += "            ON SponsorshipGroup.SponsorshipGroupId = SponsorshipList.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "         WHERE SponsorshipGroup.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN SponsorshipGroup.BegEffDate AND SponsorshipGroup.EndEffDate" + Environment.NewLine;
            sqlStmt += "      ORDER BY SponsorshipGroup.DisplaySeqNum" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListId" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandSponsorshipGroupSponsorshipList1(string sponsorshipGroupId, string locationNameDesc, SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT SponsorshipGroup.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.SponsorshipGroupDesc" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.DisplaySeqNum" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.BegEffDate" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipGroup.EndEffDate" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListDesc" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListMinQty" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListRate" + Environment.NewLine;
            sqlStmt += "          FROM SponsorshipGroup" + Environment.NewLine;
            sqlStmt += "    INNER JOIN SponsorshipList" + Environment.NewLine;
            sqlStmt += "            ON SponsorshipGroup.SponsorshipGroupId = SponsorshipList.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "         WHERE SponsorshipGroup.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND SponsorshipGroup.SponsorshipGroupId = " + sponsorshipGroupId + Environment.NewLine;
            sqlStmt += "           AND '" + DateTime.Now.ToString("yyyy-MM-dd") + "' BETWEEN SponsorshipGroup.BegEffDate AND SponsorshipGroup.EndEffDate" + Environment.NewLine;
            sqlStmt += "      ORDER BY SponsorshipGroup.DisplaySeqNum" + Environment.NewLine;
            sqlStmt += "              ,SponsorshipList.SponsorshipListId" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            return sqlCommand;
        }
        private SqlCommand BuildSqlCommadContactUsInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "INSERT ContactUs(LocationNameDesc, ContactUsTypeId, FirstName, LastName, EmailAddress, TelephoneNum, Comments) SELECT @LocationNameDesc, @ContactUsTypeId, @FirstName, @LastName, @EmailAddress, @TelephoneNum, @Comments";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ContactUsTypeId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@TelephoneNum", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@Comments", SqlDbType.NVarChar, 2048);

            return sqlCommand;

        }
        private SqlCommand BuildSqlCommandSponsorListData(string locationNameDesc, string sponsorshipGroupId, string fromDate, string toDate, string sankalpamStatus, SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "            SELECT " + Environment.NewLine;
            sqlStmt += "                   Sponsorship.AddDateTime" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.SankalpamDateTime" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.SponsorshipId" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.EmailAddress" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.FirstName" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.LastName" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.PrimaryTelephoneNum" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.ShoppingCartAmount" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.ShoppingCartComments" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCartItem.SeqNum" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCartItem.ItemId" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCartItem.ItemDesc" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCartItem.ItemRate" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCartItem.OrderQty" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCartItem.OrderAmount" + Environment.NewLine;
            sqlStmt += "                  ,CreditCardData.StatusNameDesc" + Environment.NewLine;
            sqlStmt += "                  ,SponsorshipGroup.SponsorshipGroupDesc" + Environment.NewLine;
            sqlStmt += "              FROM Sponsorship" + Environment.NewLine;
            sqlStmt += "        INNER JOIN ArchLib.ShoppingCart" + Environment.NewLine;
            sqlStmt += "                ON Sponsorship.ShoppingCartId = ShoppingCart.ShoppingCartId" + Environment.NewLine;
            sqlStmt += "        INNER JOIN ArchLib.ShoppingCartItem" + Environment.NewLine;
            sqlStmt += "                ON ShoppingCart.ShoppingCartId = ShoppingCartItem.ShoppingCartId" + Environment.NewLine;
            sqlStmt += "        INNER JOIN ArchLib.CreditCardData" + Environment.NewLine;
            sqlStmt += "                ON ShoppingCart.CreditCardDataId = CreditCardData.CreditCardDataId" + Environment.NewLine;
            sqlStmt += "         LEFT JOIN SponsorshipList" + Environment.NewLine;
            sqlStmt += "                ON ShoppingCartItem.ItemId = SponsorshipList.SponsorshipListId" + Environment.NewLine;
            sqlStmt += "         LEFT JOIN SponsorshipGroup" + Environment.NewLine;
            sqlStmt += "                ON SponsorshipList.SponsorshipGroupId = SponsorshipGroup.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "             WHERE Sponsorship.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "               AND ShoppingCart.EmailAddress != 'rramaswamy1@hotmail.com'" + Environment.NewLine;
            sqlStmt += "               AND CreditCardData.StatusNameDesc = 'SUCCESS'" + Environment.NewLine;
            sqlStmt += "               AND CAST(Sponsorship.AddDateTime AS DATE) BETWEEN '" + fromDate + "' AND '" + toDate + "'" + Environment.NewLine;
            if (sponsorshipGroupId != "")
            {
                sqlStmt += "               AND SponsorshipGroup.SponsorshipGroupId = " + sponsorshipGroupId + Environment.NewLine;
            }
            if (!string.IsNullOrWhiteSpace(sankalpamStatus))
            {
                switch (sankalpamStatus)
                {
                    case "1": //Not Completed
                        sqlStmt += "               AND Sponsorship.SankalpamDateTime IS NULL" + Environment.NewLine;
                        break;
                    case "2": //Competed
                        sqlStmt += "               AND Sponsorship.SankalpamDateTime IS NOT NULL" + Environment.NewLine;
                        break;
                    default: //All
                        break;
                }
            }
            else
            {
            }
            sqlStmt += "          ORDER BY Sponsorship.AddDateTime DESC, Sponsorship.SponsorshipId, ShoppingCartItem.SeqNum" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandSponsorshipReportData(string locationNameDesc, string fromDate, string toDate, string emailAddress, SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "            SELECT DISTINCT" + Environment.NewLine;
            sqlStmt += "                   Sponsorship.AddDateTime" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.SankalpamDateTime" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.SponsorshipId" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.EmailAddress" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.FirstName" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.LastName" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.PrimaryTelephoneNum" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.ShoppingCartAmount" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.ShoppingCartComments" + Environment.NewLine;
            sqlStmt += "                  ,CreditCardData.StatusNameDesc" + Environment.NewLine;
            sqlStmt += "                  ,CreditCardData.RequestData" + Environment.NewLine;
            sqlStmt += "                  ,CreditCardData.ResponseData" + Environment.NewLine;
            //sqlStmt += "                  ,SponsorshipGroup.SponsorshipGroupDesc" + Environment.NewLine;
            sqlStmt += "              FROM Sponsorship" + Environment.NewLine;
            sqlStmt += "        INNER JOIN ArchLib.ShoppingCart" + Environment.NewLine;
            sqlStmt += "                ON Sponsorship.ShoppingCartId = ShoppingCart.ShoppingCartId" + Environment.NewLine;
            //sqlStmt += "        INNER JOIN ArchLib.ShoppingCartItem" + Environment.NewLine;
            //sqlStmt += "                ON ShoppingCart.ShoppingCartId = ShoppingCartItem.ShoppingCartId" + Environment.NewLine;
            sqlStmt += "        INNER JOIN ArchLib.CreditCardData" + Environment.NewLine;
            sqlStmt += "                ON ShoppingCart.CreditCardDataId = CreditCardData.CreditCardDataId" + Environment.NewLine;
            //sqlStmt += "         LEFT JOIN SponsorshipList" + Environment.NewLine;
            //sqlStmt += "                ON ShoppingCartItem.ItemId = SponsorshipList.SponsorshipListId" + Environment.NewLine;
            //sqlStmt += "         LEFT JOIN SponsorshipGroup" + Environment.NewLine;
            //sqlStmt += "                ON SponsorshipList.SponsorshipGroupId = SponsorshipGroup.SponsorshipGroupId" + Environment.NewLine;
            sqlStmt += "             WHERE Sponsorship.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                sqlStmt += "               AND ShoppingCart.EmailAddress = '" + emailAddress + "'" + Environment.NewLine;
            }
            sqlStmt += "               AND CreditCardData.StatusNameDesc = 'SUCCESS'" + Environment.NewLine;
            sqlStmt += "               AND CAST(Sponsorship.AddDateTime AS DATE) BETWEEN '" + fromDate + "' AND '" + toDate + "'" + Environment.NewLine;
            sqlStmt += "          ORDER BY ShoppingCart.EmailAddress, Sponsorship.AddDateTime DESC, Sponsorship.SponsorshipId" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandEventRegisterInsert(string locationNameDesc, string locationNameDesc1, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            #region
            string sqlStmt = "";
            sqlStmt += "            INSERT dbo.EventRegister" + Environment.NewLine;
            sqlStmt += "                  (" + Environment.NewLine;
            sqlStmt += "                   Comments" + Environment.NewLine;
            sqlStmt += "                  ,EmailAddress" + Environment.NewLine;
            sqlStmt += "                  ,FirstName" + Environment.NewLine;
            sqlStmt += "                  ,LastName" + Environment.NewLine;
            sqlStmt += "                  ,SessionDesc" + Environment.NewLine;
            sqlStmt += "                  ,SessionId" + Environment.NewLine;
            sqlStmt += "                  ,TelephoneNumber" + Environment.NewLine;
            sqlStmt += "                  ,AddUserId" + Environment.NewLine;
            sqlStmt += "                  ,UpdUserId" + Environment.NewLine;
            sqlStmt += "                  )" + Environment.NewLine;
            sqlStmt += "            OUTPUT INSERTED.EventRegisterId" + Environment.NewLine;
            sqlStmt += "            SELECT" + Environment.NewLine;
            sqlStmt += "                   @Comments" + Environment.NewLine;
            sqlStmt += "                  ,@EmailAddress" + Environment.NewLine;
            sqlStmt += "                  ,@FirstName" + Environment.NewLine;
            sqlStmt += "                  ,@LastName" + Environment.NewLine;
            sqlStmt += "                  ,@SessionDesc" + Environment.NewLine;
            sqlStmt += "                  ,@SessionId" + Environment.NewLine;
            sqlStmt += "                  ,@TelephoneNumber" + Environment.NewLine;
            sqlStmt += "                  ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "                  ,@LoggedInUserId" + Environment.NewLine;
            #endregion
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@Comments", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 300);
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@SessionDesc", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@SessionId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@TelephoneNumber", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);

            return sqlCommand;
        }
        private string GenerateRandomKey(int keyLength)
        {
            string randomKey = "";
            string randomChar;
            string[] upperCaseChars = new string[26];
            string[] lowerCaseChars = new string[26];
            int[] numbers = new int[10];
            string[] specialChars = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+" };
            int i;
            int randomNumber;
            Random randomNumberObject = new Random();
            Random randomValueObject = new Random();
            for (i = 0; i < 26; i++)
            {
                upperCaseChars[i] = Convert.ToChar(i + 65).ToString();
                lowerCaseChars[i] = Convert.ToChar(i + 97).ToString();
            }
            for (i = 0; i < 10; i++)
            {
                numbers[i] = i;
            }
            for (i = 0; i < keyLength; i++)
            {
                randomNumber = randomNumberObject.Next(0, 3);
                switch (randomNumber)
                {
                    case 0: //Upper Case
                        randomNumber = randomValueObject.Next(0, 25);
                        randomChar = upperCaseChars[randomNumber];
                        break;
                    case 1: //Lower Case
                        randomNumber = randomValueObject.Next(0, 25);
                        randomChar = lowerCaseChars[randomNumber];
                        break;
                    case 2: //Number
                        randomNumber = randomValueObject.Next(0, 9);
                        randomChar = numbers[randomNumber].ToString();
                        break;
                    case 3: //Special Char
                        randomNumber = randomValueObject.Next(0, specialChars.Length - 1);
                        randomChar = specialChars[randomNumber];
                        break;
                    default:
                        randomChar = "";
                        break;
                }
                randomKey = randomKey + randomChar;
            }
            return randomKey;
        }
        //SankalpamDateTime POST
        public List<SponsorListDataModel> SankalpamDateTime(string locationNameDesc, string locationNameDesc1, string sponsorshipGroupId, string fromDate, string toDate, string sankalpamStatus, List<string> sponsorshipIds, HttpSessionStateBase httpSessionStateBase, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                string sponsorshipIdParms = "", prefixChar = "";
                foreach (var sponsorshipId in sponsorshipIds)
                {
                    sponsorshipIdParms += prefixChar + sponsorshipId;
                    prefixChar = ", ";
                }
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = BuildSqlCommandSponsorshipSankalpamDateTimeUpdate(sqlConnection);
                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject" + locationNameDesc1];
                string loginUserId = EncryptDecrypt.DecryptDataMd5(sessionObjectModel.LoginUserId, privateKey);
                sqlCommand.Parameters["@SankalpamDoneBy"].Value = loginUserId;
                sqlCommand.CommandText = sqlCommand.CommandText.Replace("@SponsorshipIds", sponsorshipIdParms);
                sqlCommand.ExecuteNonQuery();
                SendEmailUpdateSankalpamDateTime(locationNameDesc, locationNameDesc1, execUniqueId, sponsorshipIds, sponsorshipIdParms, sqlConnection);
                List<SponsorListDataModel> sponsorListDataModels = SponsorListData(locationNameDesc, locationNameDesc1, sponsorshipGroupId, fromDate, toDate, sankalpamStatus);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sponsorListDataModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                return null;
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
        private void SendEmailUpdateSankalpamDateTime(string locationNameDesc, string locationNameDesc1, string execUniqueId, List<string> sponsorshipIds, string sponsorShipIdParms, SqlConnection sqlConnection)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                string emailSubject, emailBody, emailBody1;
                Dictionary<string, string> emailTemplateKeywordValues = SankalpamDateTimeKeywordValues(locationNameDesc, locationNameDesc1, execUniqueId);
                emailSubject = emailTemplateKeywordValues["EMAIL_SUBJECT"];
                emailBody = emailTemplateKeywordValues["EMAIL_BODY"];
                SqlCommand sqlCommand = BuildSqlCommandSponsorshipSankalpamDateTimeSelect(sqlConnection);
                sqlCommand.CommandText = sqlCommand.CommandText.Replace("@SponsorshipIds", sponsorShipIdParms);
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    emailBody1 = emailBody.Replace("@@##SponsorshipId##@@", sqlDataReader["SponsorShipId"].ToString());
                    emailBody1 = emailBody1.Replace("@@##SankalpamDateTime##@@", sqlDataReader["SankalpamDateTime"].ToString());
                    emailTemplateKeywordValues["EMAIL_SUBJECT"] = emailSubject.Replace("@@##SponsorshipId##@@", sqlDataReader["SponsorShipId"].ToString());
                    emailTemplateKeywordValues["EMAIL_BODY"] = emailBody1;
                    emailTemplateKeywordValues["ToEmailAddress"] = sqlDataReader["EmailAddress"].ToString();
                    SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, null, execUniqueId);
                }
                sqlDataReader.Close();
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
            }
        }
        private SqlCommand BuildSqlCommandSponsorshipSankalpamDateTimeSelect(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "            SELECT " + Environment.NewLine;
            sqlStmt += "                   Sponsorship.AddDateTime" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.LocationNameDesc" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.SankalpamDateTime" + Environment.NewLine;
            sqlStmt += "                  ,Sponsorship.SponsorshipId" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.EmailAddress" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.FirstName" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.LastName" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.PrimaryTelephoneNum" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.ShoppingCartAmount" + Environment.NewLine;
            sqlStmt += "                  ,ShoppingCart.ShoppingCartComments" + Environment.NewLine;
            sqlStmt += "              FROM Sponsorship" + Environment.NewLine;
            sqlStmt += "        INNER JOIN ArchLib.ShoppingCart" + Environment.NewLine;
            sqlStmt += "                ON Sponsorship.ShoppingCartId = ShoppingCart.ShoppingCartId" + Environment.NewLine;
            sqlStmt += "             WHERE Sponsorship.SponsorshipId IN(@SponsorshipIds)" + Environment.NewLine;
            sqlStmt += "               AND Sponsorship.LocationNameDesc IN(@LocationNameDesc)" + Environment.NewLine;

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 100);
            return sqlCommand;
        }
        private SqlCommand BuildSqlCommandSponsorshipSankalpamDateTimeUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        UPDATE dbo.Sponsorship";
            sqlStmt += "           SET SankalpamDateTime = GETDATE()";
            sqlStmt += "              ,SankalpamDoneBy = @SankalpamDoneBy";
            sqlStmt += "         WHERE SponsorshipId IN(@SponsorshipIds)";

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@SankalpamDoneBy", SqlDbType.BigInt);

            return sqlCommand;
        }
        private Dictionary<string, string> SankalpamDateTimeKeywordValues(string locationNameDesc, string locationNameDesc1, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\SankalpamUpdateTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\SankalpamUpdateTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            emailBody = emailBody.Replace("@@##SignatureTemplate##@@", SignatureTemplate(locationNameDesc1, templatesDirectoryName));
            string bccEmailAddresss;
            try
            {
                bccEmailAddresss = HttpContext.Current.Application[locationNameDesc1 + "EmailBccEmailAddresss"].ToString();
            }
            catch
            {
                bccEmailAddresss = "";
            }
            Dictionary<string, string> emailTemplateKeywordValues = new Dictionary<string, string>()
            {
                { "EMAIL_SUBJECT", emailSubject },
                { "EMAIL_BODY", emailBody },
                { "FromEmailAddress", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailAddress"].ToString() },
                { "FromEmailDisplayName", HttpContext.Current.Application[locationNameDesc1 + "EmailFromEmailDisplayName"].ToString() },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            return emailTemplateKeywordValues;
        }
    }
}
