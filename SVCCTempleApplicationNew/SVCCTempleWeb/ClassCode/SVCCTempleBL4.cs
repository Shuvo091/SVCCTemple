using ArchitectureLibraryException;
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
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.ClassCode
{
    public partial class SVCCTempleBL
    {
        //Kiosk Order GET
        public List<KioskGroupModel> KioskOrder(string locationNameDesc, string locationNameDesc1, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            int rowNum = 0;
            try
            {
                List<KioskGroupModel> kioskGroupModels = new List<KioskGroupModel>();
                KioskGroupModel kioskGroupModel;
                sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
                sqlConnection.Open();
                SqlCommand sqlCommand = BuildSqlCommandKioskOrderSelect(sqlConnection);
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@EffDate"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    kioskGroupModels.Add
                    (
                        kioskGroupModel = new KioskGroupModel
                        {
                            KioskGroupId = long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                            LocationNameDesc = sqlDataReader["KioskGroupId"].ToString(),
                            KioskGroupDesc = sqlDataReader["KioskGroupDesc"].ToString(),
                            KioskGroupType = sqlDataReader["KioskGroupType"].ToString(),
                            ItemImageName = sqlDataReader["KioskGroup_ItemImageName"].ToString(),
                            ItemImageHeight = sqlDataReader["KioskGroup_ItemImageHeight"].ToString(),
                            ItemImageWidth = sqlDataReader["KioskGroup_ItemImageWidth"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SponsorshipGroupId"].ToString()),
                            KioskItemModels = new List<KioskItemModel>(),
                        }
                    );
                    while (sqlDataReaderRead && kioskGroupModel.KioskGroupId == long.Parse(sqlDataReader["KioskGroupId"].ToString()))
                    {
                        rowNum++;
                        kioskGroupModel.KioskItemModels.Add
                        (
                            new KioskItemModel
                            {
                                KioskItemId = long.Parse(sqlDataReader["KioskItemId"].ToString()),
                                LocationNameDesc = sqlDataReader["KioskGroupId"].ToString(),
                                KioskGroupId = long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                                ItemDesc = sqlDataReader["ItemDesc"].ToString(),
                                //ItemDesc1 = sqlDataReader["ItemDesc1"].ToString(),
                                //ItemDesc2 = sqlDataReader["ItemDesc2"].ToString(),
                                ItemImageName = sqlDataReader["ItemImageName"].ToString(),
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
                return kioskGroupModels;
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
        //Kiosk Checkout POST
        public KioskCheckoutModel KioskCheckout(string locationNameDesc, string locationNameDesc1, string generalDonationDescription, string generalDonationAmount, string kioskOrderItemIds, string kioskOrderOrderQtys, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
                sqlConnection.Open();
                KioskCheckoutModel kioskCheckoutModel = new KioskCheckoutModel
                {
                    GeneralDonationAmount = generalDonationAmount,
                    GeneralDonationDescription = generalDonationDescription,
                    KioskOrderItemIds = kioskOrderItemIds,
                    KioskOrderedOrderQtys = kioskOrderOrderQtys,
                    KioskItemModels = BuildKioskItemModels(locationNameDesc, locationNameDesc1, generalDonationDescription, generalDonationAmount, kioskOrderItemIds, kioskOrderOrderQtys, sqlConnection, out int orderItemCount, out float orderItemAmount, clientId, ipAddress, execUniqueId, loggedInUserId),
                    OrderItemAmount = orderItemAmount,
                    OrderItemCount = orderItemCount,
                    PaymentModel = new PaymentModel
                    {
                        SponsorshipAmount = orderItemAmount,
                    },
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return kioskCheckoutModel;
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
        //Kiosk Payment POST
        public void KioskPayment(string locationNameDesc, string locationNameDesc1, ref KioskCheckoutModel kioskCheckoutModel, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
                sqlConnection.Open();
                kioskCheckoutModel.KioskItemModels = BuildKioskItemModels(locationNameDesc, locationNameDesc1, kioskCheckoutModel.GeneralDonationDescription, kioskCheckoutModel.GeneralDonationAmount, kioskCheckoutModel.KioskOrderItemIds, kioskCheckoutModel.KioskOrderedOrderQtys, sqlConnection, out int orderItemCount, out float orderItemAmount, clientId, ipAddress, execUniqueId, loggedInUserId);
                kioskCheckoutModel.OrderItemAmount = orderItemAmount;
                kioskCheckoutModel.OrderItemCount = orderItemCount;
                kioskCheckoutModel.PaymentModel.SponsorshipAmount = orderItemAmount;
                //Create Shopping Cart
                ShoppingCartBL shoppingCartBL = new ShoppingCartBL();
                ShoppingCart shoppingCart = CreateShoppingCart(locationNameDesc, locationNameDesc1, kioskCheckoutModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                CreditCardDataBO creditCardDataBO = CreateCreditCardData(locationNameDesc, locationNameDesc1, kioskCheckoutModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                kioskCheckoutModel.PaymentModel.CreditCardProcessStatus = shoppingCartBL.ProcessShoppingCart(shoppingCart, creditCardDataBO, null, execUniqueId);
                if (kioskCheckoutModel.PaymentModel.CreditCardProcessStatus) //CC Process Success
                {
                    kioskCheckoutModel.PaymentModel.UniqeRefId = creditCardDataBO.CreditCardProcessMessage;
                }
                else
                {
                    kioskCheckoutModel.PaymentModel.CreditCardProcessMessage = creditCardDataBO.CreditCardProcessMessage.Replace("\r\n", "<br />").Replace("\n", "<br />");
                    modelStateDictionary.AddModelError("", kioskCheckoutModel.PaymentModel.CreditCardProcessMessage);
                }
                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                //Insert Sponsorship
                long sponsorshipId = SponsorshipInsertRow(locationNameDesc, -1, shoppingCart.ShoppingCartId, "KIOSK");
                kioskCheckoutModel.SponsorshipId = sponsorshipId;
                //Send out email
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before KioskOrderReceiptEmailKeywordValues");
                string emailStatus = "";
                Dictionary<string, string> emailTemplateKeywordValues = KioskOrderReceiptEmailKeywordValues(locationNameDesc, locationNameDesc1, sponsorshipId, kioskCheckoutModel, emailStatus, execUniqueId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before KioskOrderReceiptPDF");
                //Convert to Pdf
                SponsorShipRecieptPDF(sponsorshipId, kioskCheckoutModel.SponsorshipReceipt);
                List<string> emailAttachmentFileNames = null;
                emailAttachmentFileNames = new List<string>();
                string documentsImagesDirectoryName = HttpContext.Current.Server.MapPath("~/Documents/SponsorshipReceipts/") + @"\";
                string emailAttachmentFileName = documentsImagesDirectoryName + @"\SponsorShipReciept_" + sponsorshipId + ".pdf";
                emailAttachmentFileNames.Add(emailAttachmentFileName);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before SendEmail");
                SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, emailAttachmentFileNames, execUniqueId);
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
                }
            }
        }
        //LoginUserProf POST
        public SessionObjectModel LoginUserProf(string locationNameDesc, string locationNameDesc1, ref LoginUserProfModel loginUserProfModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        { string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1", loginUserProfModel.CaptchaAnswerLogin))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerLogin", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                    sqlConnection = new SqlConnection(databaseConnectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = BuildSqlCommandLoginUserSelect(sqlConnection);
                    sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                    sqlCommand.Parameters["@LoginNameId1"].Value = loginUserProfModel.LoginEmailAddress;
                    sqlCommand.Parameters["@LoginNameId2"].Value = "";
                    sqlCommand.Parameters["@LoginNameId3"].Value = "";
                    sqlDataReader = sqlCommand.ExecuteReader();
                    bool success;
                    if (sqlDataReader.Read()) //True - Email Address exists in the database
                    {
                        if (
                            sqlDataReader["LoginPassword"].ToString() == EncryptDecrypt.EncryptDataMd5(loginUserProfModel.LoginPassword, privateKey) &&
                            long.Parse(DateTime.Parse(sqlDataReader["PasswordExpiryDate"].ToString()).ToString("yyyyMMddHHmmss")) > long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) &&
                            sqlDataReader["UserStatusNameDesc"].ToString() == "ACTIVE"
                           )
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        success = false;
                    }
                    if (success)
                    {
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("", "Unable to login with credentials supplied");
                        modelStateDictionary.AddModelError("LoginEmailAddress", "Please check your email address");
                        modelStateDictionary.AddModelError("LoginPassword", "Please check your password");
                    }
                }
                else
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098300 :: Exception", new Exception("Register User Profile Model Errors"), "", "Register User Profile Model Errors");
                }
                SessionObjectModel sessionObjectModel;
                if (modelStateDictionary.IsValid)
                {
                    sessionObjectModel = new SessionObjectModel
                    {
                        LoginUserId = EncryptDecrypt.EncryptDataMd5(sqlDataReader["LoginUserId"].ToString(), privateKey),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        UserStatusId = long.Parse(sqlDataReader["UserStatusId"].ToString()),
                        UserTypeNameId = EncryptDecrypt.EncryptDataMd5(sqlDataReader["UserTypeNameId"].ToString(), privateKey),
                        LoginNameId1 = sqlDataReader["LoginNameId1"].ToString(),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        LastName = sqlDataReader["LastName"].ToString(),
                    };
                }
                else
                {
                    sessionObjectModel = null;
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                loginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                loginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                loginUserProfModel.CaptchaAnswerLogin = null;
                return sessionObjectModel;
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
                    ;
                }
            }
        }
        //RegisterUserLoginUser GET
        public RegisterUserLoginUserModel RegisterUserLoginUser(string locationNameDesc, string locationNameDesc1, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                modelStateDictionary.Clear();
                RegisterUserLoginUserModel registerUserLoginUserModel = new RegisterUserLoginUserModel
                {
                    //ContactUsModel = new ContactUsModel
                    //{
                    //    ResponseObjectModel = new ResponseObjectModel
                    //    {
                    //        ResponseTypeId = ResponseTypeEnum.Info,
                    //    },
                    //},
                    LoginUserProfModel = new LoginUserProfModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    //QueryString = queryString,
                    RegisterUserProfModel = new RegisterUserProfModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    ResetPasswordModel = new ResetPasswordModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    },
                };
                List<string> numberSessions = new List<string>
                {
                    "CaptchaNumberLogin0",
                    "CaptchaNumberLogin1",
                    "CaptchaNumberRegister0",
                    "CaptchaNumberRegister1",
                    "CaptchaNumberResetPassword0",
                    "CaptchaNumberResetPassword1",
                    //"CaptchaNumberContactUs0",
                    //"CaptchaNumberContactUs1",
                };
                GenerateCaptchaQuesion(httpSessionStateBase, numberSessions);
                //if (registerUserLoginUserModel.ContactUsModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                //{
                //    ;
                //}
                //else
                //{
                //    registerUserLoginUserModel.ContactUsModel.CaptchaAnswerContactUs = null;
                //    registerUserLoginUserModel.ContactUsModel.CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString();
                //    registerUserLoginUserModel.ContactUsModel.CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString();
                //}
                if (registerUserLoginUserModel.LoginUserProfModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.LoginUserProfModel.CaptchaAnswerLogin = null;
                    registerUserLoginUserModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                    registerUserLoginUserModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                }
                if (registerUserLoginUserModel.RegisterUserProfModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.RegisterUserProfModel.CaptchaAnswerRegister = null;
                    registerUserLoginUserModel.RegisterUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
                    registerUserLoginUserModel.RegisterUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
                }
                if (registerUserLoginUserModel.ResetPasswordModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.ResetPasswordModel.CaptchaAnswerResetPassword = null;
                    registerUserLoginUserModel.ResetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
                    registerUserLoginUserModel.ResetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
                }
                return registerUserLoginUserModel;
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
        //RegisterUserProf POST
        public void RegisterUserProf(string locationNameDesc, string locationNameDesc1, ref RegisterUserProfModel registerUserProfModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1", registerUserProfModel.CaptchaAnswerRegister))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerRegister", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                    sqlConnection = new SqlConnection(databaseConnectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = BuildSqlCommandLoginUserSelect(sqlConnection);
                    sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                    sqlCommand.Parameters["@LoginNameId1"].Value = registerUserProfModel.RegisterEmailAddress;
                    sqlCommand.Parameters["@LoginNameId2"].Value = "";
                    sqlCommand.Parameters["@LoginNameId3"].Value = "";
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read()) //True Email address exists
                    {
                        modelStateDictionary.AddModelError("RegisterEmailAddress", "Email address already registered");
                        modelStateDictionary.AddModelError("RegisterEmailAddress", "Try with a different email address");
                        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exception", new Exception("Email Address already registered"), "", "Email Address already registered");
                    }
                    else
                    {
                        sqlDataReader.Close();
                        long? personId = InsertPerson(locationNameDesc, registerUserProfModel, sqlConnection, execUniqueId);
                        if (personId != null)
                        {
                            long? loginUserId = InsertLoginUser(locationNameDesc, (long)personId, registerUserProfModel, sqlConnection, execUniqueId);
                            if (loginUserId != null)
                            {
                                DateTime resetPasswordDateTime = DateTime.Now.AddDays(3);
                                Random random = new Random();
                                string randomNumber1 = random.Next(0, 999999999).ToString();
                                string randomNumber2 = random.Next(0, 999999999).ToString();
                                string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                                string resetPasswordQueryString = personId + "_" + loginUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
                                string resetPasswordExpiryDateTime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
                                string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                                string resetPasswordKey = GenerateRandomKey(8);
                                UpdateLoginUserResetPassword((long)loginUserId, resetPasswordQueryString, resetPasswordExpiryDateTime, resetPasswordKey, privateKey, sqlConnection, execUniqueId);
                                Dictionary<string, string> emailTemplateKeywordValues = RegisterEmailKeywordValues(locationNameDesc, locationNameDesc1, registerUserProfModel.RegisterEmailAddress, resetPasswordQueryString, resetPasswordDateTime, resetPasswordKey, execUniqueId);
                                SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, null, execUniqueId);
                                registerUserProfModel = new RegisterUserProfModel
                                {
                                    ResponseObjectModel = new ResponseObjectModel
                                    {
                                        ColumnCount = 1,
                                        ListStyleType = "decimal",
                                        ResponseMessages = new List<string>
                                        {
                                            "Your email has been successfully registered",
                                            "Welcome to our family",
                                            "Please check your email to complete your registration",
                                            "Your email could be in Junk/Spam folder",
                                            "If so, mark this email address as not spam",
                                        },
                                        ResponseTypeId = ResponseTypeEnum.Success,
                                        TextAlign = "left",
                                    },
                                };
                                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                            }
                            else
                            {
                                modelStateDictionary.AddModelError("RegisterEmailAddress", "Error while registering email address");
                                modelStateDictionary.AddModelError("RegisterEmailAddress", "Please try again or contact support");
                                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098100 :: Exception", new Exception("Error while registering email address"), "", "Email Address already registered");
                            }
                        }
                        else
                        {
                            modelStateDictionary.AddModelError("RegisterEmailAddress", "Error while registering email address");
                            modelStateDictionary.AddModelError("RegisterEmailAddress", "Please try again or contact support");
                            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098200 :: Exception", new Exception("Error while registering email address"), "", "Email Address already registered");
                        }
                    }
                }
                else
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098300 :: Exception", new Exception("Register User Profile Model Errors"), "", "Register User Profile Model Errors");
                }
                if (modelStateDictionary.IsValid)
                {

                }
                else
                {
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                registerUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
                registerUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
                registerUserProfModel.CaptchaAnswerRegister = null;
                return;
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
        //Reset Password POST
        public void ResetPassword(string locationNameDesc, string locationNameDesc1, ref ResetPasswordModel resetPasswordModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1", resetPasswordModel.CaptchaAnswerResetPassword))
                {
                    string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                    sqlConnection = new SqlConnection(databaseConnectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = BuildSqlCommandLoginUserSelect(sqlConnection);
                    sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                    sqlCommand.Parameters["@LoginNameId1"].Value = resetPasswordModel.ResetPasswordEmailAddress;
                    sqlCommand.Parameters["@LoginNameId2"].Value = "";
                    sqlCommand.Parameters["@LoginNameId3"].Value = "";
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read()) //True Email Address exists in database
                    {
                        long loginUserId, personId;
                        loginUserId = long.Parse(sqlDataReader["LoginUserId"].ToString());
                        personId = long.Parse(sqlDataReader["PersonId"].ToString());
                        DateTime resetPasswordDateTime = DateTime.Now.AddDays(3);
                        Random random = new Random();
                        string randomNumber1 = random.Next(0, 999999999).ToString();
                        string randomNumber2 = random.Next(0, 999999999).ToString();
                        string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                        string resetPasswordQueryString = personId + "_" + loginUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
                        string resetPasswordExpiryDateTime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
                        string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                        string resetPasswordKey = GenerateRandomKey(8);
                        sqlDataReader.Close();
                        UpdateLoginUserResetPassword((long)loginUserId, resetPasswordQueryString, resetPasswordExpiryDateTime, resetPasswordKey, privateKey, sqlConnection, execUniqueId);
                        Dictionary<string, string> emailTemplateKeywordValues = ResetPasswordKeywordValues(locationNameDesc, locationNameDesc1, resetPasswordModel.ResetPasswordEmailAddress, resetPasswordQueryString, resetPasswordDateTime, resetPasswordKey, execUniqueId);
                        SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, null, execUniqueId);
                        resetPasswordModel = new ResetPasswordModel
                        {
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                ColumnCount = 1,
                                ListStyleType = "decimal",
                                ResponseMessages = new List<string>
                                {
                                    "Your request to reset your password is successful",
                                    "Please check your email for further instructions",
                                    "Your email could be in Junk/Spam folder",
                                    "If so, mark this email address as not spam",
                                    "If you did not request this, please ignore the email &",
                                    "Contact our support staff",
                                },
                                ResponseTypeId = ResponseTypeEnum.Success,
                                TextAlign = "left",
                            },
                        };
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("ResetPasswordEmailAddress", "Email address not found - please check");
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswer", "Incorrect captcha answer");
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1");
                resetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
                resetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
                if (modelStateDictionary.IsValid)
                {
                }
                else
                {
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
        //Update Password GET
        public UpdatePasswordModel UpdatePassword(string locationNameDesc, string locationNameDesc1, string resetPasswordQueryString, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool success;
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                success = ValidateUpdatePassword(locationNameDesc, locationNameDesc1, resetPasswordQueryString, sqlConnection, out string resetPasswordKey, out string loginUserId, clientId, ipAddress, execUniqueId, loggedInUserId);
                UpdatePasswordModel updatePasswordModel = new UpdatePasswordModel
                {
                    ResponseObjectModel = new ResponseObjectModel(),
                };
                if (success)
                {
                    updatePasswordModel.PasswordStrengthMessages = CreatePasswordStrengthMessages();
                    updatePasswordModel.ResetPasswordQueryString = resetPasswordQueryString;
                    updatePasswordModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Info;
                    GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
                    updatePasswordModel.CaptchaNumber0 = httpSessionStateBase["CaptchaNumber0"].ToString();
                    updatePasswordModel.CaptchaNumber1 = httpSessionStateBase["CaptchaNumber1"].ToString();
                }
                else
                {
                    updatePasswordModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                    modelStateDictionary.AddModelError("", "Your request is either invalid or expired");
                    modelStateDictionary.AddModelError("", "Please use the correct URL to update your password");
                    modelStateDictionary.AddModelError("", "You can try resetting the password one more time");
                    modelStateDictionary.AddModelError("", "Should the problem continue to persist");
                    modelStateDictionary.AddModelError("", "Please contact our support personnel");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return updatePasswordModel;
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
                }
                catch
                {
                    ;
                }
            }
        }
        //Update Password POST
        public void UpdatePassword(string locationNameDesc, string locationNameDesc1, ref UpdatePasswordModel updatePasswordModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool success;
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                success = ValidateUpdatePassword(locationNameDesc, locationNameDesc1, updatePasswordModel.ResetPasswordQueryString, sqlConnection, out string resetPasswordKey, out string loginUserId, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (success)
                {
                    if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1", updatePasswordModel.CaptchaAnswer))
                    {
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("CaptchaAnswer", "Incorrect captcha answer");
                    }
                    if (resetPasswordKey == updatePasswordModel.ResetPasswordKey)
                    {
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("ResetPasswordKey", "Enter valid reset password key");
                    }
                    short passwordStrength = CalculatePasswordStrength(updatePasswordModel.LoginPassword, out string passwordStrengthColor, out string passwordStrengthMessage);
                    updatePasswordModel.LoginPasswordStrengthColor = passwordStrengthColor;
                    updatePasswordModel.LoginPasswordStrengthMessage = passwordStrengthMessage;
                    if (passwordStrength < 4)
                    {
                        modelStateDictionary.AddModelError("LoginPassword", "Your password is not strong enough");
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("", "Your request is either invalid or expired");
                    modelStateDictionary.AddModelError("", "Please use the correct URL to update your password");
                    modelStateDictionary.AddModelError("", "You can try resetting the password one more time");
                    modelStateDictionary.AddModelError("", "Should the problem continue to persist");
                    modelStateDictionary.AddModelError("", "Please contact our support personnel");
                }
                if (modelStateDictionary.IsValid)
                {
                    string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE LoginUser SET LoginPassword = @LoginPassword, PasswordExpiryDate = @PasswordExpiryDate, ResetPasswordCompletedDateTime = @ResetPasswordCompletedDateTime WHERE LocationNameDesc = @LocationNameDesc AND LoginUserId = @LoginUserId", sqlConnection);
                    sqlCommand.Parameters.Add("@LoginPassword", SqlDbType.NVarChar, 1024);
                    sqlCommand.Parameters.Add("@PasswordExpiryDate", SqlDbType.VarChar, 50);
                    sqlCommand.Parameters.Add("@ResetPasswordCompletedDateTime", SqlDbType.VarChar, 50);
                    sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
                    sqlCommand.Parameters.Add("@LoginUserId", SqlDbType.BigInt);
                    sqlCommand.Parameters["@LoginPassword"].Value = EncryptDecrypt.EncryptDataMd5(updatePasswordModel.LoginPassword, privateKey);
                    sqlCommand.Parameters["@PasswordExpiryDate"].Value = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm:ss");
                    sqlCommand.Parameters["@ResetPasswordCompletedDateTime"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                    sqlCommand.Parameters["@LoginUserId"].Value = loginUserId;
                    sqlCommand.ExecuteNonQuery();
                    Dictionary<string, string> emailTemplateKeywordValues = UpdatePasswordKeywordValues(locationNameDesc, locationNameDesc1, updatePasswordModel.EmailAddress, execUniqueId);
                    SendEmail(locationNameDesc, locationNameDesc1, emailTemplateKeywordValues, privateKey, null, execUniqueId);
                    //Add code to send email
                }
                else
                {
                    MergeModelStateErrorMessages(modelStateDictionary);
                    GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
                    updatePasswordModel.CaptchaNumber0 = httpSessionStateBase["CaptchaNumber0"].ToString();
                    updatePasswordModel.CaptchaNumber1 = httpSessionStateBase["CaptchaNumber1"].ToString();
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
                    ;
                }
            }
        }
        public void BuildKioskItemModels(string locationNameDesc, string locationNameDesc1, KioskCheckoutModel kioskCheckoutModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(HttpContext.Current.Application["DatabaseConnectionString"].ToString());
                sqlConnection.Open();
                kioskCheckoutModel.KioskItemModels = BuildKioskItemModels(locationNameDesc, locationNameDesc1, kioskCheckoutModel.GeneralDonationDescription, kioskCheckoutModel.GeneralDonationAmount, kioskCheckoutModel.KioskOrderItemIds, kioskCheckoutModel.KioskOrderedOrderQtys, sqlConnection, out int orderItemCount, out float orderItemAmount, clientId, ipAddress, execUniqueId, loggedInUserId);
                kioskCheckoutModel.OrderItemAmount = orderItemAmount;
                kioskCheckoutModel.OrderItemCount = orderItemCount;
                kioskCheckoutModel.PaymentModel.SponsorshipAmount = orderItemAmount;
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
                    sqlConnection.Close();
                }
                catch
                {
                }
            }
        }
        public string GetUserTypeNameId(string locationNameDesc, string locationNameDesc1, HttpSessionStateBase httpSessionStateBase, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string userTypeNameId;
            string privateKey = HttpContext.Current.Application["PrivateKey"].ToString();
            SessionObjectModel sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject" + locationNameDesc1];
            try
            {
                userTypeNameId = EncryptDecrypt.DecryptDataMd5(sessionObjectModel.UserTypeNameId, privateKey);
            }
            catch
            {
                userTypeNameId = "";
            }
            return userTypeNameId;
        }
        private bool ValidateUpdatePassword(string locationNameDesc, string locationNameDesc1, string resetPasswordQueryString, SqlConnection sqlConnection, out string resetPasswordKey, out string loginUserId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                bool success;
                int indexOf1, indexOf2;
                string personId;
                indexOf1 = resetPasswordQueryString.IndexOf("_", 0);
                personId = resetPasswordQueryString.Substring(0, indexOf1);
                indexOf2 = resetPasswordQueryString.IndexOf("_", indexOf1 + 1);
                loginUserId = resetPasswordQueryString.Substring(indexOf1 + 1, indexOf2 - indexOf1 - 1);
                SqlCommand sqlCommand = new SqlCommand("SELECT LoginUser.* FROM LoginUser INNER JOIN Person ON LoginUser.LocationNameDesc = Person.LocationNameDesc AND LoginUser.PersonId = Person.PersonId WHERE LoginUser.LocationNameDesc = '" + locationNameDesc + "' AND Person.PersonId = " + personId + " AND LoginUser.ResetPasswordQueryString = '" + resetPasswordQueryString + "'", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    resetPasswordKey = sqlDataReader["ResetPasswordKey"].ToString();
                    if (sqlDataReader["LoginUserId"].ToString() == loginUserId && sqlDataReader["PersonId"].ToString() == personId)
                    {
                        if (
                            sqlDataReader["ResetPasswordCompletedDateTime"].ToString() == "" &&
                            long.Parse(DateTime.Parse(sqlDataReader["ResetPasswordExpiryDateTime"].ToString()).ToString("yyyyMMddHHmmss")) >
                            long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"))
                           )
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        success = false;
                    }
                }
                else
                {
                    resetPasswordKey = "";
                    success = false;
                }
                return success;
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
                    sqlDataReader.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        private List<KioskItemModel> BuildKioskItemModels(string locationNameDesc, string locationNameDesc1, string generalDonationDescription, string generalDonationAmount, string kioskOrderItemIds, string kioskOrderOrderQtys, SqlConnection sqlConnection, out int orderItemCount, out float orderItemAmount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                List<KioskItemModel> kioskItemModels = new List<KioskItemModel>();
                KioskItemModel kioskItemModel;
                orderItemCount = 0;
                orderItemAmount = 0;
                if (!string.IsNullOrWhiteSpace(generalDonationDescription) && !string.IsNullOrWhiteSpace(generalDonationAmount) && float.TryParse(generalDonationAmount, out float tempAmount))
                {
                    kioskItemModels.Add
                    (
                        kioskItemModel = new KioskItemModel
                        {
                            ItemDesc = generalDonationDescription,
                            //ItemDesc1 = generalDonationDescription,
                            //ItemDesc2 = "", //generalDonationDescription,
                            ItemRate = float.Parse(generalDonationAmount),
                            OrderAmount = float.Parse(generalDonationAmount),
                            OrderQty = 1,
                            KioskItemId = 0,
                            KioskGroupId = 0,
                            KioskGroupModel = new KioskGroupModel
                            {
                                KioskGroupDesc = "General",
                                KioskGroupId = 0,
                            },
                        }
                    );
                    orderItemAmount += kioskItemModel.OrderAmount.Value;
                    orderItemCount++;
                }
                if (!string.IsNullOrWhiteSpace(kioskOrderItemIds))
                {
                    SqlCommand sqlCommand = BuildSqlCommandKioskCheckoutSelect(sqlConnection);// new SqlCommand("SELECT * FROM KioskGroup INNER JOIN KioskItem ON KioskGroup.KioskGroupId = KioskItem.KioskGroupId WHERE KioskItem.KioskItemId IN(" + kioskOrderItemIds + ")", sqlConnection);
                    sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                    sqlCommand.CommandText = sqlCommand.CommandText.Replace("@KioskItemIds", kioskOrderItemIds);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    List<string> kioskOrderItemIdList = kioskOrderItemIds.Split(',').ToList();
                    List<string> kioskOrderOrderQtyList = kioskOrderOrderQtys.Split(',').ToList();
                    long orderQty;
                    while (sqlDataReader.Read())
                    {
                        orderQty = long.Parse(kioskOrderOrderQtyList[kioskOrderItemIdList.FindIndex(x => x == sqlDataReader["KioskItemId"].ToString())]);
                        kioskItemModels.Add
                        (
                            kioskItemModel = new KioskItemModel
                            {
                                ItemDesc = sqlDataReader["ItemDesc"].ToString(),
                                //ItemDesc1 = sqlDataReader["ItemDesc1"].ToString(),
                                //ItemDesc2 = sqlDataReader["ItemDesc2"].ToString(),
                                ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
                                OrderAmount = float.Parse(sqlDataReader["ItemRate"].ToString()) * orderQty,
                                OrderQty = orderQty,
                                KioskItemId = long.Parse(sqlDataReader["KioskItemId"].ToString()),
                                KioskGroupId = long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                                KioskGroupModel = new KioskGroupModel
                                {
                                    KioskGroupDesc = sqlDataReader["KioskGroupDesc"].ToString(),
                                    KioskGroupId = long.Parse(sqlDataReader["KioskGroupId"].ToString()),
                                },
                            }
                        );
                        orderItemAmount += kioskItemModel.OrderAmount.Value;
                        orderItemCount++;
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return kioskItemModels;
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
                }
            }
        }
        private Dictionary<string, string> KioskOrderReceiptEmailKeywordValues(string locationNameDesc, string locationNameDesc1, long sponsorshipId, KioskCheckoutModel kioskCheckoutModel, string registerStatus, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string templatesDirectoryName, emailSubject, emailBody;
            templatesDirectoryName = HttpContext.Current.Application["TemplatesDirectoryUrl"].ToString();
            templatesDirectoryName = HttpContext.Current.Server.MapPath(templatesDirectoryName);
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + @"\KioskOrderReceiptEmailTemplateSubject.txt");
            emailSubject = streamReader.ReadToEnd() + sponsorshipId;
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + @"\KioskOrderReceiptEmailTemplateBody.html");
            emailBody = streamReader.ReadToEnd();
            streamReader.Close();
            string creditCardNumber, kioskOrderItems = "", kioskOrderItemsTemp, kioskOrderItemsTemplate;
            streamReader = new StreamReader(templatesDirectoryName + @"\KioskOrderItemTemplate.html");
            kioskOrderItemsTemplate = streamReader.ReadToEnd();
            streamReader.Close();
            int seqNum = 0;
            for (int i = 0; i < kioskCheckoutModel.KioskItemModels.Count; i++)
            {
                seqNum++;
                kioskOrderItemsTemp = kioskOrderItemsTemplate;
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##SeqNum##@@", seqNum.ToString());
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##KioskItemId##@@", kioskCheckoutModel.KioskItemModels[i].KioskItemId.ToString());
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##KioskGroupDesc##@@", kioskCheckoutModel.KioskItemModels[i].KioskGroupModel.KioskGroupDesc);
                //kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##KioskItemDesc##@@", kioskCheckoutModel.KioskItemModels[i].ItemDesc1 + " " + kioskCheckoutModel.KioskItemModels[i].ItemDesc2);
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##KioskItemDesc##@@", kioskCheckoutModel.KioskItemModels[i].ItemDesc);
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##OrderQty##@@", kioskCheckoutModel.KioskItemModels[i].OrderQty.ToString());
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##ItemRate##@@", kioskCheckoutModel.KioskItemModels[i].ItemRate.Value.ToString("c"));
                kioskOrderItemsTemp = kioskOrderItemsTemp.Replace("@@##OrderAmount##@@", kioskCheckoutModel.KioskItemModels[i].OrderAmount.Value.ToString("c"));
                kioskOrderItems += kioskOrderItemsTemp;
            }
            try
            {
                creditCardNumber = kioskCheckoutModel.PaymentModel.CreditCardNumber.Substring(kioskCheckoutModel.PaymentModel.CreditCardNumber.Length - 4);
            }
            catch
            {
                creditCardNumber = "";
            }
            emailBody = emailBody.Replace("@@##SponsorshipId##@@", sponsorshipId.ToString());
            emailBody = emailBody.Replace("@@##EmailAddress##@@", kioskCheckoutModel.PaymentModel.EmailAddress);
            emailBody = emailBody.Replace("@@##OrderAmount##@@", kioskCheckoutModel.OrderItemAmount.ToString("c"));
            emailBody = emailBody.Replace("@@##EmailAddress##@@", kioskCheckoutModel.PaymentModel.EmailAddress);
            emailBody = emailBody.Replace("@@##CreditCardNumber##@@", creditCardNumber);
            emailBody = emailBody.Replace("@@##CreditCardProcessMessage##@@", kioskCheckoutModel.PaymentModel.CreditCardProcessStatus ? "SUCCESS" : kioskCheckoutModel.PaymentModel.CreditCardProcessMessage);
            emailBody = emailBody.Replace("@@##UniqeRefId##@@", kioskCheckoutModel.PaymentModel.UniqeRefId);
            try
            {
                emailBody = emailBody.Replace("@@##KioskOrderComments##@@", kioskCheckoutModel.PaymentModel.Comments.Replace("\n", "<br />"));
            }
            catch
            {
                emailBody = emailBody.Replace("@@##KioskOrderComments##@@", "");
            }
            emailBody = emailBody.Replace("@@##KioskOrderItems##@@", kioskOrderItems);
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
                { "ToEmailAddress", kioskCheckoutModel.PaymentModel.EmailAddress },
                { "EmailBccEmailAddresss", bccEmailAddresss },
            };
            kioskCheckoutModel.SponsorshipReceipt = emailBody;
            return sponsorshipEmailTemplateKeywordValues;
        }
        private ShoppingCart CreateShoppingCart(string locationNameDesc, string locationNameDesc1, KioskCheckoutModel kioskCheckoutModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ShoppingCart shoppingCart = new ShoppingCart
            {
                EmailAddress = kioskCheckoutModel.PaymentModel.EmailAddress,
                FirstName = kioskCheckoutModel.PaymentModel.FirstName,
                LastName = kioskCheckoutModel.PaymentModel.LastName,
                PrimaryTelephoneNum = kioskCheckoutModel.PaymentModel.PrimaryTelephoneNum,
                ShoppingCartAmount = kioskCheckoutModel.OrderItemAmount,
                ShoppingCartComments = kioskCheckoutModel.PaymentModel.Comments,
                ShoppingCartItems = CreateShoppingCartItems(locationNameDesc, locationNameDesc1, kioskCheckoutModel, clientId, ipAddress, execUniqueId, loggedInUserId),
            };
            return shoppingCart;
        }
        private List<ShoppingCartItem> CreateShoppingCartItems(string locationNameDesc, string locationNameDesc1, KioskCheckoutModel kioskCheckoutModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
            short seqNum = 0;
            for (int i = 0; i < kioskCheckoutModel.KioskItemModels.Count; i++)
            {
                shoppingCartItems.Add
                (
                    new ShoppingCartItem
                    {
                        ItemDesc = kioskCheckoutModel.KioskItemModels[i].ItemDesc,
                        ItemId = kioskCheckoutModel.KioskItemModels[i].KioskItemId.Value,
                        ItemRate = kioskCheckoutModel.KioskItemModels[i].ItemRate,
                        OrderAmount = kioskCheckoutModel.KioskItemModels[i].OrderAmount,
                        OrderComments = "",
                        OrderQty = (int)kioskCheckoutModel.KioskItemModels[i].OrderQty.Value,
                        RateTypeNameDesc = "CHARGE",
                        SeqNum = seqNum++,
                    }
                );
            }
            return shoppingCartItems;
        }
        private CreditCardDataBO CreateCreditCardData(string locationNameDesc, string locationNameDesc1, KioskCheckoutModel kioskCheckoutModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            CreditCardDataBO creditCardDataBO = new CreditCardDataBO
            {
                CreditCardAmount = kioskCheckoutModel.OrderItemAmount.ToString(),
                CreditCardCVV = kioskCheckoutModel.PaymentModel.CreditCardSecCode,
                CreditCardExpiryMM = kioskCheckoutModel.PaymentModel.CreditCardExpiryMM,
                CreditCardExpiryYYYY = kioskCheckoutModel.PaymentModel.CreditCardExpiryYYYY,//.Substring(sponsorshipModel.PaymentModel.CreditCardExpiryYYYY.Length - 2),
                CreditCardHolderName = kioskCheckoutModel.PaymentModel.NameOnCreditCard,
                CreditCardNumber = kioskCheckoutModel.PaymentModel.CreditCardNumber,
                Currency = "USD",
            };
            return creditCardDataBO;
        }
    }
}
