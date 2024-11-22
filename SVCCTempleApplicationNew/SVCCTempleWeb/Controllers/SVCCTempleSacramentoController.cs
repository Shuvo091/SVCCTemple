using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using Newtonsoft.Json;
using SVCCTempleWeb.ClassCode;
using SVCCTempleWeb.Enumerations;
using SVCCTempleWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.Controllers
{
    [OutputCache(Duration = 0)]
    public class SVCCTempleSacramentoController : Controller
    {
        private const string LOCATION_NAME_DESC = "SACRAMENTO";
        private const string LOCATION_NAME_DESC1 = "Sacramento";
        private string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        // GET: SVCCTempleSacramento
        public ActionResult Index1()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Index";
            string importantIdsList = "5001, 5002, 5005, 5006, 5007, 5008, 5009";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            List<ImportantDatesModel> importantDatesModelsList = svccTempleBL.GetImportantDates2(LOCATION_NAME_DESC, 12, importantIdsList, null, execUniqueId);//5001, 5002, 5005, 5006, 5007, 5008, 5009
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(importantDatesModelsList);
        }

        public ActionResult Index()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Index";
            string importantIdsList = "5001, 5002, 5005, 5006, 5007, 5008, 5009";
            //string startDate = "2021-10-01", finishDate = "2021-10-31";
            //string emailEventsHtml = GenerateEmailEventsHtml(LOCATION_NAME_DESC, startDate, finishDate, importantIdsList);
            //Response.Write("<div  id='emailEventsTemplate'>" + emailEventsHtml + "</div><br style='clear: both;' />");
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string startDate = DateTime.Now.ToString("yyyy-MM-dd"), finishDate = DateTime.Now.AddDays(8).ToString("yyyy-MM-dd");
            IndexModel indexModel = svccTempleBL.BuildIndexModel(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, importantIdsList, 12, startDate, finishDate, execUniqueId);
            //List<ImportantDatesModel> importantDatesModelsList = svccTempleBL.GetImportantDates2(LOCATION_NAME_DESC, 12, importantIdsList, execUniqueId);//5001, 5002, 5005, 5006, 5007, 5008, 5009
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(indexModel);
        }

        #region
        [AllowAnonymous]
        [HttpGet]
        //[Route("SVCCTempleSacramento/IndexGenerateTempleEventData/{fromDate:long}/{toDate:long}/{deploy:bool}")]
        public ActionResult IndexGenerateTempleEventData(string fromDate, string toDate, string deploy)
        {
            //http://localhost:21455/SVCCTempleSacramento/IndexGenerateTemplEventData?fromDate=2023-05-05&toDate=2023-05-31
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "IndexGenerateTemplEventData";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string startDate = DateTime.Parse(fromDate).ToString("yyyy-MM-dd"), finishDate = DateTime.Parse(toDate).ToString("yyyy-MM-dd");
            svccTempleBL.GenerateTempleEventDataSacramento(LOCATION_NAME_DESC, startDate, finishDate, bool.Parse(deploy), execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }
        #endregion

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AboutUs()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "AboutUs";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Booking()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Booking";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Booking(BookingModel bookingModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Booking";
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            ActionResult actionResult;
            ModelState.Clear();
            TryValidateModel(bookingModel);
            sVCCTempleBL.Booking(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, ref bookingModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (ModelState.IsValid)
            {
                actionResult = PartialView("_BookingSuccess", bookingModel);
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                actionResult = PartialView("_BookingData", bookingModel);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Calendar(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Calendar";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string importantDatesIds = "5001, 5002, 5003, 5004, 5005, 5006, 5051, 5052, 5053, 5054, 5055, 5056, 5057, 5058";
            CalendarModel calendarModel = svccTempleBL.GenerateCalendarData(LOCATION_NAME_DESC, id, importantDatesIds, execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(calendarModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ContactUs()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "ContactUs";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            ContactUsModel contactUsModel = svccTempleBL.ContactUs(LOCATION_NAME_DESC, execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(contactUsModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ContactUs(ContactUsModel contactUsModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "ContactUs";
            JsonResult jsonResult;
            WebUtilities webUtilities = new WebUtilities();
            contactUsModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel();
            ResponseModel responseModel;
            try
            {
                ModelState.Clear();
                if (TryValidateModel(contactUsModel))
                {
                    SVCCTempleBL svccTempleBL = new SVCCTempleBL();
                    svccTempleBL.ContactUs(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, contactUsModel, execUniqueId);
                    responseModel = new ResponseModel
                    {
                        ResponseMessagesHtml = ViewToHtmlString(this, "_ResponseMessages", contactUsModel.ResponseObjectModel),
                        ResponseMessagesData = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("ContactUsTypeId", ""),
                            new KeyValuePair<string, string>("FirstName", ""),
                            new KeyValuePair<string, string>("LastName", ""),
                            new KeyValuePair<string, string>("EmailAddress", ""),
                            new KeyValuePair<string, string>("TelephoneNumber", ""),
                            new KeyValuePair<string, string>("Comments", ""),
                        },
                    };
                    jsonResult = Json(responseModel, JsonRequestBehavior.AllowGet);
                }
                else //Model Errors
                {
                    contactUsModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                    ValidationSummaryModel validationSummaryModel = new ValidationSummaryModel
                    {
                        ResponseMessagesError = webUtilities.CopyModelErrorsToReponseMessagesError(ModelState),
                        ValidationSummaryMessage = "Please fix errors to continue",
                    };
                    validationSummaryModel.ValidationSummaryPropertiesHtml = ViewToHtmlString(this, "_ValidationSummary", validationSummaryModel);
                    jsonResult = Json(validationSummaryModel, JsonRequestBehavior.AllowGet);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Model Errors");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Error", exception);
                contactUsModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                ModelState.AddModelError("", "System error occurred");
                ModelState.AddModelError("", "Please contact support personnel");
                ValidationSummaryModel validationSummaryModel = new ValidationSummaryModel
                {
                    ResponseMessagesError = webUtilities.CopyModelErrorsToReponseMessagesError(ModelState),
                    ValidationSummaryMessage = "Please fix errors to continue",
                };
                validationSummaryModel.ValidationSummaryPropertiesHtml = ViewToHtmlString(this, "_ValidationSummary", validationSummaryModel);
                jsonResult = Json(validationSummaryModel, JsonRequestBehavior.AllowGet);
            }
            if (contactUsModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return jsonResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult Dashboard()
        {
            ViewData["ActionName"] = "Dashboard";
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public FileResult DownloadSponsorShipReciept(string id)
        {
            var downloadFullFileName = Server.MapPath("~/Documents/TempDownload/") + @"\" + id;
            return File(downloadFullFileName, "application/force-download", id);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Forbidden()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ImportantDates()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "ImportantDates";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            List<ImportantDatesModel> importantDatesModelsList = svccTempleBL.GetImportantDates2(LOCATION_NAME_DESC, 12, "5001, 5002, 5003, 5004, 5005, 5006, 5007, 5008, 5009", null, execUniqueId);//5001, 5002, 5005, 5006, 5007, 5008, 5009
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(importantDatesModelsList);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult TempleInfoDates()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "TempleInfoDates";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string startDate = DateTime.Now.ToString("yyyy-MM-dd"), finishDate = DateTime.Now.AddDays(17).ToString("yyyy-MM-dd");
            TempleInfoDatesModel templeInfoDatesModel = svccTempleBL.GetTempleInfoDates(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, startDate, finishDate, execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View("DailyInfoSankalpamInfo", templeInfoDatesModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult KioskCheckout()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                string kioskOrderItemIds = Request["kioskOrderedItemIds"];
                string kioskOrderOrderQtys = Request["kioskOrderOrderQtys"];
                string generalDonationDescription, generalDonationAmount;
                generalDonationDescription = Request["GeneralDonationDescription"];
                generalDonationAmount = Request["GeneralDonationAmount"];
                KioskCheckoutModel kioskCheckoutModel = sVCCTempleBL.KioskCheckout(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, generalDonationDescription, generalDonationAmount, kioskOrderItemIds, kioskOrderOrderQtys, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("KioskCheckout", kioskCheckoutModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = sVCCTempleBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult KioskOrder()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                List<KioskGroupModel> kioskGroupModels = sVCCTempleBL.KioskOrder(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("KioskOrder", kioskGroupModels);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = sVCCTempleBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult KioskPayment(KioskCheckoutModel kioskCheckoutModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            PartialViewResult partialViewResult;
            try
            {
                ModelState.Clear();
                TryValidateModel(kioskCheckoutModel.PaymentModel, "PaymentModel");
                if (!string.IsNullOrWhiteSpace(kioskCheckoutModel.PaymentModel.PrimaryTelephoneNum))
                {
                    if (long.TryParse(kioskCheckoutModel.PaymentModel.PrimaryTelephoneNum, out long tempNumber))
                    {
                        if (tempNumber.ToString().Length == 10)
                        {

                        }
                        else
                        {
                            ModelState.AddModelError("PaymentModel.PrimaryTelephoneNum", "Please enter valid 10 digit number");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("PaymentModel.PrimaryTelephoneNum", "Please enter valid 10 digit number");
                    }
                }
                else
                {
                }
                if (ModelState.IsValid)
                {
                    sVCCTempleBL.KioskPayment(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, ref kioskCheckoutModel, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        partialViewResult = PartialView("_KioskReceipt", kioskCheckoutModel.SponsorshipReceipt);
                        Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        partialViewResult = PartialView("_KioskCheckout", kioskCheckoutModel);
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    sVCCTempleBL.BuildKioskItemModels(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, kioskCheckoutModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    partialViewResult = PartialView("_KioskCheckout", kioskCheckoutModel);
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = sVCCTempleBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                partialViewResult = PartialView("_Error", responseObjectModel);
            }
            return partialViewResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginUserProf()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "LoginUserProf";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginUserProf(LoginUserProfModel loginUserProfModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(loginUserProfModel);
                SessionObjectModel sessionObjectModel = sVCCTempleBL.LoginUserProf(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, ref loginUserProfModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Session["SessionObjectSacramento"] = sessionObjectModel;
                if (ModelState.IsValid)
                {
                    Session.Timeout = int.Parse(ConfigurationManager.AppSettings["AccessTokenExpiryMinutes"]);
                    var identity = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim(ClaimTypes.Name, sessionObjectModel.FirstName + " " + sessionObjectModel.LastName),
                            new Claim(ClaimTypes.Email, sessionObjectModel.LoginNameId1),
                            //new Claim(ClaimTypes.Country, "India"),
                        },
                        "ApplicationCookie"
                    );
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    //return RedirectToAction("Dashboard", "SVCCTempleSacramento");
                    return Json(new { ReturnUrl = Url.Action("Dashboard", "SVCCTempleSacramento") });
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Process completed after valid model");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                sVCCTempleBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                loginUserProfModel.CaptchaAnswerLogin = null;
                loginUserProfModel.CaptchaNumberLogin0 = Session["CaptchaNumberLogin0"].ToString();
                loginUserProfModel.CaptchaNumberLogin1 = Session["CaptchaNumberLogin1"].ToString();
                sVCCTempleBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = PartialView("_LoginUserProfData", loginUserProfModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObjectSacramento"] = null;
            Session.Abandon();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return RedirectToAction("Index", "SVCCTempleSacramento");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult PicGallery()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "PicGallery";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult PoojaItems()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "PoojaItems";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterUserLoginUser(string id)
        {
            ViewData["ActionName"] = "RegisterUserLoginUser";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                RegisterUserLoginUserModel registerUserLoginUserModel = sVCCTempleBL.RegisterUserLoginUser(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("RegisterUserLoginUser", registerUserLoginUserModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = sVCCTempleBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Register User / Login / Reset Password / GET");
                sVCCTempleBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUserProf(RegisterUserProfModel registerUserProfModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                registerUserProfModel.AnswerToCaptcha = "abc";
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(registerUserProfModel);
                sVCCTempleBL.RegisterUserProf(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, ref registerUserProfModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Process completed after valid model");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                sVCCTempleBL.GenerateCaptchaQuesion(Session, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                registerUserProfModel.CaptchaAnswerRegister = null;
                registerUserProfModel.CaptchaNumberRegister0 = Session["CaptchaNumberRegister0"].ToString();
                registerUserProfModel.CaptchaNumberRegister1 = Session["CaptchaNumberRegister1"].ToString();
                sVCCTempleBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = PartialView("_RegisterUserProfData", registerUserProfModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            ViewData["ActionName"] = "ResetPassword";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                //ResetPasswordModel resetPasswordModel = sVCCTempleBL.ResetPassword(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                }
                else
                {
                }
                actionResult = View("ResetPassword", null);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = sVCCTempleBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Reset Password / GET");
                sVCCTempleBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                Session["CaptchaNumberResetPassword0"] = null;
                Session["CaptchaNumberResetPassword1"] = null;
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(resetPasswordModel);
                sVCCTempleBL.ResetPassword(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, ref resetPasswordModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Process completed after valid model");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                sVCCTempleBL.GenerateCaptchaQuesion(Session, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1");
                resetPasswordModel.CaptchaAnswerResetPassword = null;
                resetPasswordModel.CaptchaNumberResetPassword0 = Session["CaptchaNumberResetPassword0"].ToString();
                resetPasswordModel.CaptchaNumberResetPassword1 = Session["CaptchaNumberResetPassword1"].ToString();
                sVCCTempleBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = PartialView("_ResetPasswordData", resetPasswordModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SankalpamDateTime(string id, string fromDate, string toDate, string sankalpamStatus, List<string> sponsorshipIds)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "SankalpamDateTime";
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            string userTypeNameId = sVCCTempleBL.GetUserTypeNameId(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (userTypeNameId == "10" || userTypeNameId == "20" || userTypeNameId == "30")
            {
                ;
            }
            else
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return RedirectToAction("Dashboard");
            }
            if (id == null || id == "-1")
            {
                id = "";
            }
            if ((fromDate == null && fromDate == "") || (toDate == null || toDate == ""))
            {
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
                fromDate = DateTime.Now.AddDays(-28).ToString("yyyy-MM-dd");
            }
            List<SponsorListDataModel> sponsorListDataModels = sVCCTempleBL.SankalpamDateTime(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, id, fromDate, toDate, sankalpamStatus, sponsorshipIds, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            PartialViewResult partialViewResult;
            if (sponsorListDataModels != null)
            {
                partialViewResult = PartialView("_SponsorListData", sponsorListDataModels);
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 500;
                partialViewResult = null;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00091000 :: Exit");
            return partialViewResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SankalpamInfo()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "SankalpamInfo";
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            DateTime sankalpamDate = DateTime.Now;
            SankalpamInfoModel sankalpamInfoModel = new SankalpamInfoModel
            {
                StartDate = sankalpamDate,
                FinishDate = sankalpamDate.AddDays(4),
                SankalpamInfoDetailModels = new List<SankalpamInfoDetailModel>(),
            };
            for (int i = 0; i < 5; i++)
            {
                SankalpamInfoDetailModel sankalpamInfoDetailModel = new SankalpamInfoDetailModel
                {
                    SankalpamDate = sankalpamDate.AddDays(i),
                };
                sankalpamInfoDetailModel.CalendarDatas = sVCCTempleBL.GenerateCalendarData(LOCATION_NAME_DESC, sankalpamInfoDetailModel.SankalpamDate, sankalpamInfoDetailModel.SankalpamDate, execUniqueId);
                sankalpamInfoModel.SankalpamInfoDetailModels.Add(sankalpamInfoDetailModel);
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(sankalpamInfoModel);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Snippets(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Snippets";
            long snippetsId;
            try
            {
                snippetsId = long.Parse(id);
            }
            catch
            {
                snippetsId = 1;
            }
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            SnippetsModel snippetsModel = sVCCTempleBL.Snippets(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Server.MapPath("~/Snippets/"), snippetsId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(snippetsModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SnippetsData(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Snippets";
            long snippetsId;
            try
            {
                snippetsId = long.Parse(id);
            }
            catch
            {
                snippetsId = 1;
            }
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            SnippetsModel snippetsModel = sVCCTempleBL.Snippets(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Server.MapPath("~/Snippets/"), snippetsId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View("_Snippets", snippetsModel);
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult SponsorList()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string userTypeNameId = svccTempleBL.GetUserTypeNameId(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (userTypeNameId == "10" || userTypeNameId == "20" || userTypeNameId == "30")
            {
                ;
            }
            else
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return RedirectToAction("Dashboard");
            }
            ViewData["ActionName"] = "SponsorList";
            SponsorListModel sponsorListModel = svccTempleBL.SponsorList(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, execUniqueId);
            PartialViewResult partialViewResult = PartialView("_SponsorList", sponsorListModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00091000 :: Exit");
            return partialViewResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult SponsorListData(string id, string fromDate, string toDate, string sankalpamStatus)
        {
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            if (id == null || id == "-1")
            {
                id = "";
            }
            if ((fromDate == null && fromDate == "") || (toDate == null || toDate == ""))
            {
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
                fromDate = DateTime.Now.AddDays(-28).ToString("yyyy-MM-dd");
            }
            string userTypeNameId = svccTempleBL.GetUserTypeNameId(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (userTypeNameId == "10" || userTypeNameId == "20" || userTypeNameId == "30")
            {
                ;
            }
            else
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return RedirectToAction("Dashboard");
            }
            List<SponsorListDataModel> sponsorListDataModels = svccTempleBL.SponsorListData(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, id, fromDate, toDate, sankalpamStatus);
            PartialViewResult partialViewResult = PartialView("_SponsorListData", sponsorListDataModels);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00091000 :: Exit");
            return partialViewResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult SponsorListDownload(string id, string fromDate, string toDate)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string userTypeNameId = svccTempleBL.GetUserTypeNameId(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (userTypeNameId == "10" || userTypeNameId == "20" || userTypeNameId == "30")
            {
                ;
            }
            else
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return RedirectToAction("Dashboard");
            }
            if (id == null)
            {
                id = "";
            }
            if ((fromDate == null && fromDate == "") || (toDate == null || toDate == ""))
            {
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
                fromDate = DateTime.Now.AddDays(-28).ToString("yyyy-MM-dd");
            }
            byte[] fileBytes = svccTempleBL.SponsorListDataDownload(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, id, fromDate, toDate, null);
            var downloadFileName = "SVCC_" + LOCATION_NAME_DESC1 + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            FileContentResult fileContentResult = File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, downloadFileName);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00091000 :: Exit");
            return fileContentResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Sponsorship()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Sponsorship";
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            SponsorshipModel sponsorshipModel = sVCCTempleBL.Sponsorship(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(sponsorshipModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public PartialViewResult Sponsorship(SponsorshipModel sponsorshipModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Sponsorship";
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            WebUtilities webUtilities = new WebUtilities();
            sponsorshipModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel();
            PartialViewResult partialViewResult;
            try
            {
                ModelState.Clear();
                if (TryValidateModel(sponsorshipModel.PaymentModel, "PaymentModel"))
                {
                    if (sVCCTempleBL.ValidateShoppingCart(sponsorshipModel, execUniqueId))
                    {
                        sVCCTempleBL.Sponsorship(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, sponsorshipModel, execUniqueId);
                        if (sponsorshipModel.ResponseObjectModel.IsSuccessStatusCode)
                        {
                            sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Success;
                        }
                        else
                        {
                            sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                        }
                    }
                    else
                    {
                        sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                    }
                }
                else
                {
                    sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Error", exception);
                sponsorshipModel.ResponseObjectModel.ResponseTypeId = ResponseTypeEnum.Error;
            }
            sponsorshipModel.ResponseObjectModel.ListStyleType = "decimal";
            sponsorshipModel.ResponseObjectModel.TextAlign = "left";
            if (sponsorshipModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ModelState.AddModelError("", "Please fix errors to continue");
                partialViewResult = PartialView("_ResponseMessages", sponsorshipModel.ResponseObjectModel);
            }
            else
            {
                TempData["DownloadFileName"] = sponsorshipModel.DownloadFileName;
                Response.StatusCode = (int)HttpStatusCode.OK;
                partialViewResult = PartialView("_SponsorshipReceipt", sponsorshipModel.SponsorshipReceipt);
            }
            return partialViewResult;

        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult SponsorshipReport()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string userTypeNameId = svccTempleBL.GetUserTypeNameId(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (userTypeNameId == "10" || userTypeNameId == "20")
            {
                ;
            }
            else
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return RedirectToAction("Dashboard");
            }
            ViewData["ActionName"] = "SponsorReport";
            SponsorshipReportModel sponsorshipReportModel = svccTempleBL.SponsorshipReport(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, execUniqueId);
            PartialViewResult partialViewResult = PartialView("_SponsorshipReport", sponsorshipReportModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00091000 :: Exit");
            return partialViewResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult SponsorshipReportData(string fromDate, string toDate, string emailAddress)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            if ((fromDate == null && fromDate == "") || (toDate == null || toDate == ""))
            {
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
                fromDate = DateTime.Now.AddDays(-28).ToString("yyyy-MM-dd");
            }
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string userTypeNameId = svccTempleBL.GetUserTypeNameId(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, Session, clientId, ipAddress, execUniqueId, loggedInUserId);
            if (userTypeNameId == "10" || userTypeNameId == "20")
            {
                ;
            }
            else
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return RedirectToAction("Dashboard");
            }
            List<SponsorshipReportDataModel> sponsorshipReportDataModels = svccTempleBL.SponsorshipRepotData(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, fromDate, toDate, emailAddress);
            PartialViewResult partialViewResult = PartialView("_SponsorshipReportData", sponsorshipReportDataModels);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00091000 :: Exit");
            return partialViewResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult TempleServices()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "TempleServices";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Unsubscribe()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Unsubscribe";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult UpcomingEvents()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "UpcomingEvents";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult UpdatePassword(string id)
        {
            ViewData["ActionName"] = "UpdatePassword";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                UpdatePasswordModel updatePasswordModel = sVCCTempleBL.UpdatePassword(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                }
                else
                {
                }
                actionResult = View("UpdatePassword", updatePasswordModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = sVCCTempleBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Update Password / GET");
                sVCCTempleBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                Session["CaptchaNumber0"] = null;
                Session["CaptchaNumber1"] = null;
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordModel updatePasswordModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(updatePasswordModel);
                sVCCTempleBL.UpdatePassword(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, ref updatePasswordModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    actionResult = PartialView("_UpdatePasswordSuccess", updatePasswordModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    actionResult = PartialView("_UpdatePasswordData", updatePasswordModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Process completed after valid model");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                sVCCTempleBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                updatePasswordModel.CaptchaAnswer = null;
                updatePasswordModel.CaptchaNumber0 = Session["CaptchaNumber0"].ToString();
                updatePasswordModel.CaptchaNumber1 = Session["CaptchaNumber1"].ToString();
                sVCCTempleBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                actionResult = PartialView("_UpdatePasswordData", updatePasswordModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult UserProfile()
        {
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "UserProfile";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            UserProfileModel userProfileModel = new UserProfileModel(); //svccTempleBL.SponsorshipReport(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, execUniqueId);
            return PartialView("_UserProfile", userProfileModel);
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult SankalpamData()
        {
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "SankalpamData";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            SankalpamDataModel sankalpamDataModel = new SankalpamDataModel(); //svccTempleBL.SponsorshipReport(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, execUniqueId);
            return PartialView("_SankalpamData", sankalpamDataModel);
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult DonationReport()
        {
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "DonationReport";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            DonationReportModel donationReportModel = new DonationReportModel(); //svccTempleBL.SponsorshipReport(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, execUniqueId);
            return PartialView("_DonationReport", donationReportModel);
        }

        #region JsonResult
        [HttpGet]
        public JsonResult APICalendarInfo(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string importantDatesIds = "5001, 5002, 5003, 5004, 5005, 5006, 5051, 5052, 5053, 5054, 5055, 5056, 5057, 5058";
            CalendarModel calendarModel = svccTempleBL.GenerateCalendarData(LOCATION_NAME_DESC, id, importantDatesIds, execUniqueId);
            JsonResult jsonResult = Json(new { HtmlString = ViewToHtmlString(this, "_APICalendarInfo", calendarModel), JsonObject = JsonConvert.SerializeObject(calendarModel) }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");

            return jsonResult;
        }

        [HttpGet]
        public JsonResult APIDailyInfo(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            List<GeneralInfo> generalInfos;
            if (id == null)
            {
                id = DateTime.Now.ToString("yyyy-MM-dd");
            }
            generalInfos = null;// svccTempleBL.GetDailyInfo(LOCATION_NAME_DESC, id, execUniqueId);
            JsonResult jsonResult = Json(new { HtmlString = ViewToHtmlString(this, "_APIDailyInfo", generalInfos), JsonObject = JsonConvert.SerializeObject(generalInfos) }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");

            return jsonResult;
        }

        [HttpGet]
        public JsonResult APISankalpamInfo(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "SankalpamInfo";
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            DateTime sankalpamDate = DateTime.Now;
            SankalpamInfoModel sankalpamInfoModel = new SankalpamInfoModel
            {
                StartDate = sankalpamDate,
                FinishDate = sankalpamDate.AddDays(4),
                SankalpamInfoDetailModels = new List<SankalpamInfoDetailModel>(),
            };
            for (int i = 0; i < 5; i++)
            {
                SankalpamInfoDetailModel sankalpamInfoDetailModel = new SankalpamInfoDetailModel
                {
                    SankalpamDate = sankalpamDate.AddDays(i),
                };
                sankalpamInfoDetailModel.CalendarDatas = sVCCTempleBL.GenerateCalendarData(LOCATION_NAME_DESC, sankalpamInfoDetailModel.SankalpamDate, sankalpamInfoDetailModel.SankalpamDate, execUniqueId);
                sankalpamInfoModel.SankalpamInfoDetailModels.Add(sankalpamInfoDetailModel);
            }
            JsonResult jsonResult = Json(new { HtmlString = ViewToHtmlString(this, "_SankalpamInfo", sankalpamInfoModel), JsonObject = JsonConvert.SerializeObject(sankalpamInfoModel) }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");

            return jsonResult;
        }

        [HttpGet]
        public ActionResult APIJsonResult(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }
        #endregion

        #region Private
        private string GenerateCaptchaQuestion()
        {
            Random random = new Random();
            string randomNumber1 = random.Next(1, 11).ToString();
            string randomNumber2 = random.Next(1, 11).ToString();
            Session["CaptchaQuestionNumber1"] = randomNumber1;
            Session["CaptchaQuestionNumber2"] = randomNumber2;
            return "Sum of " + randomNumber1 + " and " + randomNumber2 + "?";
        }

        private void GenerateCaptchaQuestion(RegisterUserProfModel registerUserProfModel)
        {
            Random random = new Random();
            string randomNumber1 = random.Next(1, 11).ToString();
            string randomNumber2 = random.Next(1, 11).ToString();
            Session["CaptchaQuestionNumber1"] = randomNumber1;
            Session["CaptchaQuestionNumber2"] = randomNumber2;
            registerUserProfModel.CaptchaQuestion = "Sum of " + randomNumber1 + " and " + randomNumber2 + "?";
            registerUserProfModel.AnswerToCaptcha = null;
        }

        private string GenerateRegularEventsHtml(string startDate, string finishDate)
        {
            string templatesDirectoryName, regularEventsHtmlTemplate;
            string regularEventsHtml = "", regularEventsHtmlTemp, eventDateTime;
            string sqlStmt = "SELECT ImportantDatesDate.EventDate, CASE WHEN ISNULL(ImportantDatesDate.EventTime, '') = '' THEN ImportantDates.StartTime ELSE ImportantDatesDate.EventTime END AS EventTime, ImportantDates.EventName1, ImportantDates.EventName2, ImportantDates.EventDesc1, ImportantDates.EventDesc2, ImportantDates.EventDesc3, ImportantDates.FileName1, ImportantDates.ImageName1 FROM ImportantDates INNER JOIN ImportantDatesDate ON ImportantDates.ImportantDatesId = ImportantDatesDate.ImportantDatesId WHERE ImportantDatesDate.EventDate BETWEEN '" + startDate + "' AND '" + finishDate + "' AND ImportantDates.ImportantDatesId IN(1, 2, 3, 4, 5, 6) ORDER BY ImportantDatesDate.EventDate, ImportantDatesDate.EventTime";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection("DATA SOURCE=sql2k801.discountasp.net; UID=SQL2008_507990_svcc_user; PWD=ganesha09; INITIAL CATALOG=SQL2008_507990_svcc;");
            sqlConnection.Open();
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sqlStmt, sqlConnection);
            System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            templatesDirectoryName = Utilities.GetApplicationValue("TemplatesDirectoryUrl");
            templatesDirectoryName = HttpContext.Server.MapPath(templatesDirectoryName);
            System.IO.StreamReader streamReader = new System.IO.StreamReader(templatesDirectoryName + @"\RecurringEventsTemplate.html");
            regularEventsHtmlTemplate = streamReader.ReadToEnd();
            streamReader.Close();
            while (sqlDataReader.Read())
            {
                regularEventsHtmlTemp = regularEventsHtmlTemplate;
                eventDateTime = DateTime.Parse(sqlDataReader["EventDate"].ToString() + " " + sqlDataReader["EventTime"].ToString()).ToString("ddd - MMM - M/d - hh:mm tt");
                regularEventsHtmlTemp = regularEventsHtmlTemp.Replace("@@##ImageName1##@@", sqlDataReader["ImageName1"].ToString());
                regularEventsHtmlTemp = regularEventsHtmlTemp.Replace("@@##EventDateTime##@@", eventDateTime);
                regularEventsHtmlTemp = regularEventsHtmlTemp.Replace("@@##EventName1##@@", sqlDataReader["EventName1"].ToString().Replace("\r\n", " "));
                regularEventsHtmlTemp = regularEventsHtmlTemp.Replace("@@##EventName2##@@", sqlDataReader["EventName2"].ToString().Replace("\r\n", " "));
                regularEventsHtmlTemp = regularEventsHtmlTemp.Replace("@@##EventDesc1##@@", sqlDataReader["EventDesc1"].ToString().Replace("\r\n", " "));
                regularEventsHtmlTemp = regularEventsHtmlTemp.Replace("@@##SponsorshipGroupId##@@", sqlDataReader["EventDesc3"].ToString());

                regularEventsHtml += regularEventsHtmlTemp;
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return regularEventsHtml;
        }

        private string GenerateEmailEventsHtml(string locationNameDesc, string startDate, string finishDate, string importantIdsList)
        {
            string baseURL, sacramentoBaseURL;
            string eventDay, eventDate, eventTime, emailEventsHtml, emailEventsTr, infoData, readMoreHtml;
            string eventName, eventDesc, eventText;
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection("DATA SOURCE=sql2k801.discountasp.net; UID=SQL2008_507990_svcc_user; PWD=ganesha09; INITIAL CATALOG=SQL2008_507990_svcc;");
            sqlConnection.Open();
            System.Data.SqlClient.SqlCommand sqlCommand = BuildSqlStmtEmailEvents(locationNameDesc, startDate, finishDate, importantIdsList, sqlConnection);
            System.Data.SqlClient.SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            emailEventsHtml = "";
            emailEventsHtml += "<table style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; font-family: Arial; font-size: 12px; font-weight: bold; padding: 5px;\">" + Environment.NewLine;
            emailEventsTr = "";
            emailEventsTr += "<tr>" + Environment.NewLine;
            emailEventsTr += "    <th style=\"background-color: #d3d3d3; border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: center; vertical-align: top;\">Date</th>" + Environment.NewLine;
            emailEventsTr += "    <th style=\"background-color: #d3d3d3; border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: center; vertical-align: top;\">Time</th>" + Environment.NewLine;
            emailEventsTr += "    <th style=\"background-color: #d3d3d3; border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\">Event</th>" + Environment.NewLine;
            emailEventsTr += "    <th style=\"background-color: #d3d3d3; border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\">Description</th>" + Environment.NewLine;
            emailEventsTr += "    <th style=\"background-color: #d3d3d3; border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\"></th>" + Environment.NewLine;
            emailEventsTr += "    <th style=\"background-color: #d3d3d3; border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\"></th>" + Environment.NewLine;
            emailEventsTr += "</tr>" + Environment.NewLine;
            emailEventsHtml += emailEventsTr;
            baseURL = "https://www.svcctemple.org/"; //System.Web.HttpContext.Current.Application["BaseURL"].ToString();
            sacramentoBaseURL = "https://www.svcctemple.org/SVCCTempleSacramento/"; //System.Web.HttpContext.Current.Application["SacramentoBaseURL"].ToString();
            while (sqlDataReader.Read())
            {
                emailEventsTr = "";
                infoData = "";
                if (sqlDataReader["FileName1"].ToString() != "")
                {
                    readMoreHtml = "<a href=\"" + baseURL + "Files/" + sqlDataReader["FileName1"].ToString() + "\" target=\"_blank\">Click here to read about...</a>";
                    infoData += readMoreHtml;
                }
                if (sqlDataReader["SponsorshipGroupId"].ToString() != "-1")
                {
                    if (infoData != "")
                    {
                        infoData += "<br />";
                    }
                    readMoreHtml = "<a href=\"" + sacramentoBaseURL + "Sponsorship/" + sqlDataReader["SponsorshipGroupId"].ToString() + "\" style=\"color: #ff0000;\" target=\"_blank\">Click here to enter Sankalpam</a>";
                    infoData += readMoreHtml;
                }
                if (sqlDataReader["EventTime"].ToString() == "")
                {
                    eventTime = DateTime.Parse(sqlDataReader["StartTime"].ToString()).ToString("hh:mm tt");
                }
                else
                {
                    eventTime = DateTime.Parse(sqlDataReader["EventTime"].ToString()).ToString("hh:mm tt");
                }
                eventTime = "<span style=\"color: #000000;\">" + eventTime + "</span>";
                eventDay = "<span style=\"color: #a54000;\">" + DateTime.Parse(sqlDataReader["EventDate"].ToString()).ToString("ddd") + "</span>";
                eventDate = "<span style=\"color: #0000ff;\">" + DateTime.Parse(sqlDataReader["EventDate"].ToString()).ToString("MMM-dd") + "</span>";
                eventName = "<span style=\"color: #ff0000;\">" + sqlDataReader["EventName1"].ToString() + "</span>";
                if (sqlDataReader["EventName1"].ToString() != "")
                {
                    eventName += "<br />";
                    eventName += "<span style=\"color: #0000ff;\">" + sqlDataReader["EventName2"].ToString() + "</span>";
                }
                eventDesc = "<span style=\"color: #0000ff;\">" + sqlDataReader["EventDesc1"].ToString() + "</span>";
                if (sqlDataReader["EventName1"].ToString() != "")
                {
                    eventDesc += "<br />";
                    eventDesc += "<span style=\"color: #ff0000;\">" + sqlDataReader["EventDesc2"].ToString() + "</span>";
                }
                eventText = "<span style=\"color: #ff0000;\">" + sqlDataReader["EventText1"].ToString() + "</span>";
                if (sqlDataReader["EventText1"].ToString() != "")
                {
                    eventText += "<br />";
                    eventText += "<span style=\"color: #0000ff;\">" + sqlDataReader["EventText2"].ToString() + "</span>";
                }
                emailEventsTr += "<tr>" + Environment.NewLine;
                emailEventsTr += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: center; vertical-align: top;\">" + eventDay + "<br />" + eventDate + "</td>" + Environment.NewLine;
                emailEventsTr += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: center; vertical-align: top;\">" + eventTime + "</td>" + Environment.NewLine;
                emailEventsTr += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\">" + eventName + "</td>" + Environment.NewLine;
                emailEventsTr += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\">" + eventDesc + "</td>" + Environment.NewLine;
                emailEventsTr += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\">" + eventText + "</td>" + Environment.NewLine;
                emailEventsTr += "    <td style=\"border-collapse: collapse; border-color: #000000; border-style: solid; border-width: 1px; color: #000000; font-size: 15px; padding: 5px; text-align: left; vertical-align: top;\">" + infoData + "</td>" + Environment.NewLine;
                emailEventsTr += "</tr>" + Environment.NewLine;
                emailEventsHtml += emailEventsTr;
            }
            emailEventsHtml += "</table>" + Environment.NewLine;
            sqlDataReader.Close();
            sqlConnection.Close();
            return emailEventsHtml;
        }
        private System.Data.SqlClient.SqlCommand BuildSqlStmtEmailEvents(string locationNameDesc, string startDate, string finishDate, string importantIdsList, System.Data.SqlClient.SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               ImportantDatesDate.EventDate" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventTime" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText1" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText2" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText3" + Environment.NewLine;
            sqlStmt += "              ,MonthList_ImportantDates.*" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               *" + Environment.NewLine;
            sqlStmt += "          FROM MonthList" + Environment.NewLine;
            sqlStmt += "    CROSS JOIN ImportantDates" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList.BeginDate BETWEEN '" + startDate + "' AND '" + finishDate + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.ImportantDatesId IN(" + importantIdsList + ")" + Environment.NewLine;
            sqlStmt += "              ) MonthList_ImportantDates" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN ImportantDatesDate" + Environment.NewLine;
            sqlStmt += "            ON MonthList_ImportantDates.ImportantDatesId = ImportantDatesDate.ImportantDatesId" + Environment.NewLine;
            sqlStmt += "           AND ImportantDatesDate.EventDate BETWEEN MonthList_ImportantDates.BeginDate AND MonthList_ImportantDates.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY ImportantDatesDate.EventDate" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventTime" + Environment.NewLine;
            sqlStmt += "              ,MonthList_ImportantDates.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_ImportantDates.SeqNum" + Environment.NewLine;

            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sqlStmt, sqlConnection);
            return sqlCommand;
        }

        private string ViewToHtmlString(Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
            }
            controller.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}
//https://www.c-sharpcorner.com/UploadFile/f4f047/generating-pdf-file-using-C-Sharp/
//Refer above to download the original approach what I have used
/*
//prevent using cache mvc
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class NoCacheAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
        filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        filterContext.HttpContext.Response.Cache.SetNoStore();

        base.OnResultExecuting(filterContext);
    }
}
 */
