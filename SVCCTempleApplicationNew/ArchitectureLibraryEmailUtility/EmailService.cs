using ArchitectureLibraryEmailLibrary;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEmailUtility
{
    public class EmailService
    {
        public void SendEmail(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, string emailSubject, string emailBody, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, string privateKey = "", List<KeyValuePair<string, string>> replyToEmailAddresses = null, List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null, bool saveEmail = true)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter", toEmailAddresses, "aspNetUserId", aspNetUserId);
            try
            {
                string emailServiceType;
                try
                {
                    emailServiceType = emailServiceType = Utilities.GetApplicationValue("EmailServiceType");//ConfigurationManager.AppSettings["EmailServiceType"];
                }
                catch
                {
                    emailServiceType = "";
                }
                if (replyToEmailAddresses == null)
                {
                    replyToEmailAddresses = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value)
                    };
                }
                switch (emailServiceType)
                {
                    case "SENDGRID":
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        SendGridEmailService sendGridEmailService = new SendGridEmailService();
                        sendGridEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, replyToEmailAddresses, emailSubject, emailBody, toEmailAddresses, execUniqueId, privateKey, ccEmailAddresses, bccEmailAddresses,emailAttachmentFileNames, saveEmail);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 After SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                    case "GMAIL":
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 Before GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 After GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                    case "SMTP":
                    default:
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 Before SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        SmtpEmailService smtpEmailService = new SmtpEmailService();
                        smtpEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, emailSubject, emailBody, replyToEmailAddresses, toEmailAddresses, execUniqueId, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames, saveEmail);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 After SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 End SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId, "fromEmailAddress", fromEmailAddress.Key);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
        }
        public void SendEmailX(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, List<KeyValuePair<string, string>> replyToEmailAddresses, string emailSubject, string emailBody, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, string privateKey = "", List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter", toEmailAddresses, "aspNetUserId", aspNetUserId);
            try
            {
                var emailServiceType = Utilities.GetApplicationValue("EmailServiceType");//ConfigurationManager.AppSettings["EmailServiceType"];
                switch (emailServiceType?.ToUpper() ?? "")
                {
                    case "SENDGRID":
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        SendGridEmailService sendGridEmailService = new SendGridEmailService();
                        sendGridEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, replyToEmailAddresses, emailSubject, emailBody, toEmailAddresses, execUniqueId, privateKey, ccEmailAddresses, bccEmailAddresses);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 After SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                    case "GMAIL":
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 Before GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 After GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                    case "SMTP":
                    default:
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 Before SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        SmtpEmailService smtpEmailService = new SmtpEmailService();
                        smtpEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, emailSubject, emailBody, replyToEmailAddresses, toEmailAddresses, execUniqueId, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 After SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 End SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId, "fromEmailAddress", fromEmailAddress.Key);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
        }

        //public void SendEmail(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, List<KeyValuePair<string, string>> replyToEmailAddresses, string emailHtml, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, string privateKey = "", List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //    try
        //    {
        //        var emailServiceType = ConfigurationManager.AppSettings["EmailServiceType"];
        //        switch (emailServiceType?.ToUpper() ?? "")
        //        {
        //            case "SENDGRID":
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //                SendGridEmailService sendGridEmailService = new SendGridEmailService();
        //                //sendGridEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, replyToEmailAddresses, emailSubject, emailBody, toEmailAddresses, execUniqueId, privateKey, ccEmailAddresses, bccEmailAddresses);
        //                //sendGridEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, replyToEmailAddresses,emailHtml,toEmailAddresses, execUniqueId, privateKey, ccEmailAddresses, bccEmailAddresses);

        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 After SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //                break;
        //            case "GMAIL":
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 Before GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 After GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //                break;
        //            case "SMTP":
        //            default:
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 Before SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //                SmtpEmailService smtpEmailService = new SmtpEmailService();
        //                smtpEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress,emailHtml, replyToEmailAddresses, toEmailAddresses, execUniqueId, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 After SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
        //                break;
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 End SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId, "fromEmailAddress", fromEmailAddress.Key);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //    }
        //}
    }
}
