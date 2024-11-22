using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SVCCTempleWeb.ClassCode
{
    public static class Bootstrap
    {
        private static string todaysDate_;
        private static Dictionary<string, Dictionary<string, string>> todaysInfoLocations_;
        public static void Initialize(HttpApplicationState httpApplicationState)
        {
            Utilities.Initialize();
            string execUniqueId = "SVCCTempleWeb_Bootstrap";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.Initialize();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                httpApplicationState[ConfigurationManager.ConnectionStrings[i].Name] = ConfigurationManager.ConnectionStrings[i].ConnectionString;
            }
            for (int i = 0; i < ConfigurationManager.AppSettings.Keys.Count; i++)
            {
                httpApplicationState[ConfigurationManager.AppSettings.Keys[i]] = ConfigurationManager.AppSettings[i];
                Utilities.SetApplicationValue(ConfigurationManager.AppSettings.Keys[i], ConfigurationManager.AppSettings[i]);
            }
            httpApplicationState["PrivateKey"] = "ELPME9TCCVS";
            Utilities.SetApplicationValue("PrivateKey", "ELPME9TCCVS");
            BuildTodaysInfo(httpApplicationState, execUniqueId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
        public static Dictionary<string, string> GetTodaysInfo(string locationNameDesc, HttpApplicationState httpApplicationState, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Dictionary<string, Dictionary<string, string>> todaysInfoLocations;
            if (todaysDate_ != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                todaysInfoLocations = BuildTodaysInfo(httpApplicationState, execUniqueId);
            }
            else
            {
                todaysInfoLocations = todaysInfoLocations_;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return todaysInfoLocations[locationNameDesc];
        }
        private static Dictionary<string, Dictionary<string, string>> BuildTodaysInfo(HttpApplicationState httpApplicationState, string execUniqueId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            todaysDate_ = DateTime.Now.ToString("yyyy-MM-dd");
            Dictionary<string, Dictionary<string, string>> todaysInfoLocations = new Dictionary<string, Dictionary<string, string>>();
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            Dictionary<string, string> todaysInfos;
            todaysInfos = svccTempleBL.GetTodaysInfo("SACRAMENTO", todaysDate_, execUniqueId);
            todaysInfoLocations["SACRAMENTO"] = todaysInfos;
            todaysInfos = svccTempleBL.GetTodaysInfo("FREMONT", todaysDate_, execUniqueId);
            todaysInfoLocations["FREMONT"] = todaysInfos;
            httpApplicationState["TodaysInfoLocations"] = todaysInfoLocations;
            //Utilities.SetApplicationValue("TodaysInfoLocations", todaysInfoLocationss);
            todaysInfoLocations_ = todaysInfoLocations;
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            DeleteExceptionLog(execUniqueId);
            return todaysInfoLocations;
        }
        private static void DeleteExceptionLog(string execUniqueId)
        {
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            svccTempleBL.DeleteExceptionLog(execUniqueId);
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: ArchLib.ExceptionLog Truncated");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
    }
}
