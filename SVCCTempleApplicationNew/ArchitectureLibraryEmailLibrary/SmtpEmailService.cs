﻿using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEmailLibrary
{
    public class SmtpEmailService
    {
        public void SendEmail(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, string emailSubject, string emailBody, List<KeyValuePair<string, string>> replyToEmailAddresses, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null, bool saveEmail = true)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter", toEmailAddresses, "aspNetUserId", aspNetUserId);
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Create MailMessage", toEmailAddresses, "aspNetUserId", aspNetUserId, "fromEmailAddress", fromEmailAddress.Key);
                MailMessage mailMessage = new MailMessage
                {
                    Body = emailBody,
                    From = new MailAddress(fromEmailAddress.Key, fromEmailAddress.Value),
                    IsBodyHtml = true,
                    Subject = emailSubject.Replace("\r\n", ""),
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Add ReplyTo emails to MailMessage", replyToEmailAddresses, "aspNetUserId", aspNetUserId);
                AddToMailAddressCollection(mailMessage.ReplyToList, replyToEmailAddresses, execUniqueId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 Add To emails to MailMessage", toEmailAddresses, "aspNetUserId", aspNetUserId);
                AddToMailAddressCollection(mailMessage.To, toEmailAddresses, execUniqueId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 Add Cc emails to MailMessage", ccEmailAddresses, "aspNetUserId", aspNetUserId);
                AddToMailAddressCollection(mailMessage.CC, ccEmailAddresses, execUniqueId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 Add Bcc emails to MailMessage", bccEmailAddresses, "aspNetUserId", aspNetUserId);
                AddToMailAddressCollection(mailMessage.Bcc, bccEmailAddresses, execUniqueId);
                if (emailAttachmentFileNames != null)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 Add attachments to MailMessage", "aspNetUserId", aspNetUserId);
                    foreach (var emailAtatchmentFileName in emailAttachmentFileNames)
                    {
                        Attachment emailAttachment = new Attachment(emailAtatchmentFileName, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = emailAttachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(emailAtatchmentFileName);
                        disposition.ModificationDate = File.GetLastWriteTime(emailAtatchmentFileName);
                        disposition.ReadDate = File.GetLastAccessTime(emailAtatchmentFileName);
                        disposition.FileName = Path.GetFileName(emailAtatchmentFileName);
                        disposition.Size = new FileInfo(emailAtatchmentFileName).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;

                        mailMessage.Attachments.Add(emailAttachment);
                    }
                }
                if (saveEmail)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 Before EmailRepository.SaveEmail", "aspNetUserId", aspNetUserId);
                    EmailRepository emailRepository = new EmailRepository();
                    emailRepository.SaveEmail(emailDirectory, aspNetUserId, fromEmailAddress, emailSubject, emailBody, replyToEmailAddresses, toEmailAddresses, execUniqueId, mailMessage, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
                }
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00008000 After SmtpClient.Send", "aspNetUserId", aspNetUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00009000 Error while sending email", exception);
                throw;
            }
        }
        private void AddToMailAddressCollection(MailAddressCollection mailAddressCollection, List<KeyValuePair<string, string>> emailAddresses, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            if (emailAddresses != null)
            {
                MailAddress mailAddress;
                foreach (var emailaddress in emailAddresses)
                {
                    mailAddress = new MailAddress(emailaddress.Key, emailaddress.Value);
                    mailAddressCollection.Add(mailAddress);
                }
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 Exit");
        }
    }
}
