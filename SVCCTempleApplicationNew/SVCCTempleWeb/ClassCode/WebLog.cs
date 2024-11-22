using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Xml;

namespace SVCCTempleWeb.ClassCode
{
    public class WebLog
    {
        public void InsertUserLog(HttpContext httpContextInstance, SqlConnection sqlConnection)
        {
            string hostName, ipAddress, siteName, userHostAddress, localAddress;
            string sqlStmt;
            string absoluteUri, absolutePath, hostAddress, userAgentClient, userAgentServer, browserCapabilities;
            string ipInnerXml1, ipCountryCode, ipCountryName, ipRegionCode, ipRegionName, ipCity, ipZipCode, ipTimeZone, ipLatitude, ipLongitude, ipMetroCode;
            string ipInnerXml2, ipHost, ipISP, ipOrg, ipQueries, ipRegionName2, ipCountryCode2, ipLatitude2, ipLongitude2;
            SqlCommand sqlCommand;

            GetIPAddress(out hostName, out ipAddress, out siteName, out userHostAddress, out localAddress);
            absoluteUri = httpContextInstance.Request.Url.AbsoluteUri;
            absolutePath = httpContextInstance.Request.Url.AbsolutePath;
            hostAddress = httpContextInstance.Request.Url.Host;
            userAgentClient = "";
            userAgentServer = httpContextInstance.Request.UserAgent;
            browserCapabilities = GetBrowserCapabilities(httpContextInstance);
            GetIPLocationInfo(userHostAddress, out ipInnerXml1, out ipCountryCode, out ipCountryName, out ipRegionCode, out ipRegionName, out ipCity, out ipZipCode, out ipTimeZone, out ipLatitude, out ipLongitude, out ipMetroCode, out ipInnerXml2, out ipHost, out ipISP, out ipOrg, out ipQueries, out ipRegionName2, out ipCountryCode2, out ipLatitude2, out ipLongitude2);

            sqlStmt = "INSERT UserLog(AbsoluteUri, AbsolutePath, HostAddress,IPAddress, HostName, SiteName, UserHostAddress, LocalAddress, UserAgentClient, UserAgentServer, BrowserCapabilities, IPInnerXml1, IPCountryCode, IPCountryName, IPRegionCode, IPRegionName, IPCity, IPZipCode, IPTimeZone, IPLatitude, IPLongitude, IPMetroCode, IPInnerXml2, IPHost, IPISP, IPOrg, IPRegionName2, IPCountryCode2, IPLatitude2, IPLongitude2, IPQueries) SELECT @AbsoluteUri, @AbsolutePath, @HostAddress, @IPAddress, @HostName, @SiteName, @UserHostAddress, @LocalAddress, @UserAgentClient, @UserAgentServer, @BrowserCapabilities, @IPInnerXml1, @IPCountryCode, @IPCountryName, @IPRegionCode, @IPRegionName, @IPCity, @IPZipCode, @IPTimeZone, @IPLatitude, @IPLongitude, @IPMetroCode, @IPInnerXml2, @IPHost, @IPISP, @IPOrg, @IPRegionName2, @IPCountryCode2, @IPLatitude2, @IPLongitude2, @IPQueries WHERE NOT EXISTS(SELECT 1 FROM ExcludeIP WHERE ExcludeIPAddress = @UserHostAddress)";

            sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@AbsoluteUri", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AbsoluteUri", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AbsolutePath", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@HostAddress", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@HostName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@SiteName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserHostAddress", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LocalAddress", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserAgentClient", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserAgentServer", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@BrowserCapabilities", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPInnerXml1", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPCountryCode", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPCountryName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPRegionCode", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPRegionName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPCity", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPZipCode", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPTimeZone", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPLatitude", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPLongitude", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPMetroCode", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPInnerXml2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPHost", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPISP", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPOrg", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPRegionName2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPCountryCode2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPLatitude2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPLongitude2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@IPQueries", SqlDbType.VarChar);

            sqlCommand.Parameters["@AbsoluteUri"].Value = absoluteUri;
            sqlCommand.Parameters["@AbsoluteUri"].Value = absoluteUri;
            sqlCommand.Parameters["@AbsolutePath"].Value = absolutePath;
            sqlCommand.Parameters["@HostAddress"].Value = hostAddress;
            sqlCommand.Parameters["@IPAddress"].Value = ipAddress;
            sqlCommand.Parameters["@HostName"].Value = hostName;
            sqlCommand.Parameters["@SiteName"].Value = siteName;
            sqlCommand.Parameters["@UserHostAddress"].Value = userHostAddress;
            sqlCommand.Parameters["@LocalAddress"].Value = localAddress;
            sqlCommand.Parameters["@UserAgentClient"].Value = userAgentClient;
            sqlCommand.Parameters["@UserAgentServer"].Value = userAgentServer;
            sqlCommand.Parameters["@BrowserCapabilities"].Value = browserCapabilities;
            sqlCommand.Parameters["@IPInnerXml1"].Value = ipInnerXml1;
            sqlCommand.Parameters["@IPCountryCode"].Value = ipCountryCode;
            sqlCommand.Parameters["@IPCountryName"].Value = ipCountryName;
            sqlCommand.Parameters["@IPRegionCode"].Value = ipRegionCode;
            sqlCommand.Parameters["@IPRegionName"].Value = ipRegionName;
            sqlCommand.Parameters["@IPCity"].Value = ipCity;
            sqlCommand.Parameters["@IPZipCode"].Value = ipZipCode;
            sqlCommand.Parameters["@IPTimeZone"].Value = ipTimeZone;
            sqlCommand.Parameters["@IPLatitude"].Value = ipLatitude;
            sqlCommand.Parameters["@IPLongitude"].Value = ipLongitude;
            sqlCommand.Parameters["@IPMetroCode"].Value = ipMetroCode;
            sqlCommand.Parameters["@IPInnerXml2"].Value = ipInnerXml2;
            sqlCommand.Parameters["@IPHost"].Value = ipHost;
            sqlCommand.Parameters["@IPISP"].Value = ipISP;
            sqlCommand.Parameters["@IPOrg"].Value = ipOrg;
            sqlCommand.Parameters["@IPRegionName2"].Value = ipRegionName2;
            sqlCommand.Parameters["@IPCountryCode2"].Value = ipCountryCode2;
            sqlCommand.Parameters["@IPLatitude2"].Value = ipLatitude2;
            sqlCommand.Parameters["@IPLongitude2"].Value = ipLongitude2;
            sqlCommand.Parameters["@IPQueries"].Value = ipQueries;

            //sqlCommand.Parameters.Add("@AbsoluteUri", SqlDbType.VarChar);
            //sqlCommand.AddParameter("@AbsolutePath", DbType.String, ParameterDirection.Input, absolutePath, -1, 0, 0);
            //sqlCommand.AddParameter("@HostAddress", DbType.String, ParameterDirection.Input, hostAddress, -1, 0, 0);
            //sqlCommand.AddParameter("@IPAddress", DbType.String, ParameterDirection.Input, ipAddress, 100, 0, 0);
            //sqlCommand.AddParameter("@HostName", DbType.String, ParameterDirection.Input, hostName, 100, 0, 0);
            //sqlCommand.AddParameter("@SiteName", DbType.String, ParameterDirection.Input, siteName, 100, 0, 0);
            //sqlCommand.AddParameter("@UserHostAddress", DbType.String, ParameterDirection.Input, userHostAddress, 100, 0, 0);
            //sqlCommand.AddParameter("@LocalAddress", DbType.String, ParameterDirection.Input, localAddress, 100, 0, 0);
            //sqlCommand.AddParameter("@UserAgentClient", DbType.String, ParameterDirection.Input, userAgentClient, -1, 0, 0);
            //sqlCommand.AddParameter("@UserAgentServer", DbType.String, ParameterDirection.Input, userAgentServer, -1, 0, 0);
            //sqlCommand.AddParameter("@BrowserCapabilities", DbType.String, ParameterDirection.Input, browserCapabilities, -1, 0, 0);
            //sqlCommand.AddParameter("@IPInnerXml1", DbType.String, ParameterDirection.Input, ipInnerXml1, -1, 0, 0);
            //sqlCommand.AddParameter("@IPCountryCode", DbType.String, ParameterDirection.Input, ipCountryCode, -1, 0, 0);
            //sqlCommand.AddParameter("@IPCountryName", DbType.String, ParameterDirection.Input, ipCountryName, -1, 0, 0);
            //sqlCommand.AddParameter("@IPRegionCode", DbType.String, ParameterDirection.Input, ipRegionCode, -1, 0, 0);
            //sqlCommand.AddParameter("@IPRegionName", DbType.String, ParameterDirection.Input, ipRegionName, -1, 0, 0);
            //sqlCommand.AddParameter("@IPCity", DbType.String, ParameterDirection.Input, ipCity, -1, 0, 0);
            //sqlCommand.AddParameter("@IPZipCode", DbType.String, ParameterDirection.Input, ipZipCode, -1, 0, 0);
            //sqlCommand.AddParameter("@IPTimeZone", DbType.String, ParameterDirection.Input, ipTimeZone, -1, 0, 0);
            //sqlCommand.AddParameter("@IPLatitude", DbType.String, ParameterDirection.Input, ipLatitude, -1, 0, 0);
            //sqlCommand.AddParameter("@IPLongitude", DbType.String, ParameterDirection.Input, ipLongitude, -1, 0, 0);
            //sqlCommand.AddParameter("@IPMetroCode", DbType.String, ParameterDirection.Input, ipMetroCode, -1, 0, 0);
            //sqlCommand.AddParameter("@IPInnerXml2", DbType.String, ParameterDirection.Input, ipInnerXml2, -1, 0, 0);
            //sqlCommand.AddParameter("@IPHost", DbType.String, ParameterDirection.Input, ipHost, -1, 0, 0);
            //sqlCommand.AddParameter("@IPISP", DbType.String, ParameterDirection.Input, ipISP, -1, 0, 0);
            //sqlCommand.AddParameter("@IPOrg", DbType.String, ParameterDirection.Input, ipOrg, -1, 0, 0);
            //sqlCommand.AddParameter("@IPRegionName2", DbType.String, ParameterDirection.Input, ipRegionName2, -1, 0, 0);
            //sqlCommand.AddParameter("@IPCountryCode2", DbType.String, ParameterDirection.Input, ipCountryCode2, -1, 0, 0);
            //sqlCommand.AddParameter("@IPLatitude2", DbType.String, ParameterDirection.Input, ipLatitude2, -1, 0, 0);
            //sqlCommand.AddParameter("@IPLongitude2", DbType.String, ParameterDirection.Input, ipLongitude2, -1, 0, 0);
            //sqlCommand.AddParameter("@IPQueries", DbType.String, ParameterDirection.Input, ipQueries, -1, 0, 0);

            sqlCommand.ExecuteNonQuery();

            //if (sqlCommand.ErrorMessage == "")
            //{
            //    ;
            //}
            //else
            //{
            //    //For now leave it as is - need to record it in a different place
            //    httpContextInstance.Response.Write("Error Message for Insert User Log : " + sqlCommand.ErrorMessage + "<br /");
            //}
        }

        public void GetIPAddress(out string _hostName, out string _ipAddress, out string _siteName, out string _userHostAddress, out string _localAddress)
        {
            string hostName, ipAddress, siteName, userHostAddress, localAddress;
            IPAddress[] ipAddressList;
            GetHostIPAddress(out hostName, out ipAddress, out ipAddressList);
            siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
            for (int i = 0; i < ipAddressList.Length; i++)
            {
                //Response.Write(TabChar + i + " IP Address List =" + ipAddressList[i] + "<br />");
            }
            userHostAddress = HttpContext.Current.Request.UserHostAddress;
            localAddress = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];

            _hostName = hostName;
            _ipAddress = ipAddress;
            _siteName = siteName;
            _userHostAddress = userHostAddress;
            _localAddress = localAddress;
        }

        public void GetHostIPAddress(out string _hostName, out string _ipAddress, out IPAddress[] _ipAddressList)
        {
            _hostName = System.Net.Dns.GetHostName();
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            _ipAddress = ipAddress.ToString();
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            _ipAddressList = ipHostInfo.AddressList;
        }

        private string GetBrowserCapabilities(HttpContext _httpContextInstance)
        {
            string browserCapabilities;
            HttpBrowserCapabilities browserCapabilitiesInstance;

            browserCapabilitiesInstance = _httpContextInstance.Request.Browser;

            browserCapabilities = "";

            browserCapabilities += "Type = " + browserCapabilitiesInstance.Type;
            browserCapabilities += ";Name = " + browserCapabilitiesInstance.Browser;
            browserCapabilities += ";Version = " + browserCapabilitiesInstance.Version;
            browserCapabilities += ";Major Version = " + browserCapabilitiesInstance.MajorVersion;
            browserCapabilities += ";Minor Version = " + browserCapabilitiesInstance.MinorVersion;
            browserCapabilities += ";Platform = " + browserCapabilitiesInstance.Platform;
            browserCapabilities += ";Is Beta = " + browserCapabilitiesInstance.Beta;
            browserCapabilities += ";Is Crawler = " + browserCapabilitiesInstance.Crawler;
            browserCapabilities += ";Is AOL = " + browserCapabilitiesInstance.AOL;
            browserCapabilities += ";Is Win16 = " + browserCapabilitiesInstance.Win16;
            browserCapabilities += ";Is Win32 = " + browserCapabilitiesInstance.Win32;
            browserCapabilities += ";Supports Frames = " + browserCapabilitiesInstance.Frames;
            browserCapabilities += ";Supports Tables = " + browserCapabilitiesInstance.Tables;
            browserCapabilities += ";Supports Cookies = " + browserCapabilitiesInstance.Cookies;
            browserCapabilities += ";Supports VB Script = " + browserCapabilitiesInstance.VBScript;
            browserCapabilities += ";Supports JavaScript Major = " + browserCapabilitiesInstance.EcmaScriptVersion.Major;
            browserCapabilities += ";Supports JavaScript MajorRevision = " + browserCapabilitiesInstance.EcmaScriptVersion.MajorRevision;
            browserCapabilities += ";Supports JavaScript Minor = " + browserCapabilitiesInstance.EcmaScriptVersion.Minor;
            browserCapabilities += ";Supports JavaScript MinorRevision = " + browserCapabilitiesInstance.EcmaScriptVersion.MinorRevision;
            browserCapabilities += ";Supports Java Applets = " + browserCapabilitiesInstance.JavaApplets;
            browserCapabilities += ";Supports ActiveX Controls = " + browserCapabilitiesInstance.ActiveXControls;
            browserCapabilities += ";CDF = " + browserCapabilitiesInstance.CDF;

            return browserCapabilities;
            //browserCapabilities += ";<br>" + Request.UserAgent;
        }

        private void GetIPLocationInfo(string _userHostAddress, out string _ipInnerXml1, out string _ipCountryCode, out string _ipCountryName, out string _ipRegionCode, out string _ipRegionName, out string _ipCity, out string _ipZipCode, out string _ipTimeZone, out string _ipLatitude, out string _ipLongitude, out string _ipMetroCode, out string _ipInnerXml2, out string _ipHost, out string _ipISP, out string _ipOrg, out string _ipQueries, out string _ipRegionName2, out string _ipCountryCode2, out string _ipLatitude2, out string _ipLongitude2)
        {
            _ipInnerXml1 = "";
            _ipCountryCode = "";
            _ipCountryName = "";
            _ipRegionCode = "";
            _ipRegionName = "";
            _ipCity = "";
            _ipZipCode = "";
            _ipTimeZone = "";
            _ipLatitude = "";
            _ipLongitude = "";
            _ipMetroCode = "";
            _ipInnerXml2 = "";
            _ipHost = "";
            _ipISP = "";
            _ipOrg = "";
            _ipQueries = "";
            _ipRegionName2 = "";
            _ipCountryCode2 = "";
            _ipLatitude2 = "";
            _ipLongitude2 = "";

            return;
            //XmlDocument ipInfoXmldoc1, ipInfoXmldoc2;
            //ipInfoXmldoc1 = new XmlDocument();
            //ipInfoXmldoc2 = new XmlDocument();
            //try
            //{
            //    ipInfoXmldoc1.Load("http://www.freegeoip.net/xml/" + _userHostAddress);
            //    _ipInnerXml1 = ipInfoXmldoc1.InnerXml;

            //    try
            //    {
            //        _ipCountryCode = ipInfoXmldoc1.GetElementsByTagName("CountryCode")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipCountryCode = "";
            //    }
            //    try
            //    {
            //        _ipCountryName = ipInfoXmldoc1.GetElementsByTagName("CountryName")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipCountryName = "";
            //    }
            //    try
            //    {
            //        _ipRegionCode = ipInfoXmldoc1.GetElementsByTagName("RegionCode")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipRegionCode = "";
            //    }
            //    try
            //    {
            //        _ipRegionName = ipInfoXmldoc1.GetElementsByTagName("RegionName")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipRegionName = "";
            //    }
            //    try
            //    {
            //        _ipCity = ipInfoXmldoc1.GetElementsByTagName("City")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipCity = "";
            //    }
            //    try
            //    {
            //        _ipZipCode = ipInfoXmldoc1.GetElementsByTagName("ZipCode")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipZipCode = "";
            //    }
            //    try
            //    {
            //        _ipTimeZone = ipInfoXmldoc1.GetElementsByTagName("TimeZone")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipTimeZone = "";
            //    }
            //    try
            //    {
            //        _ipLatitude = ipInfoXmldoc1.GetElementsByTagName("Latitude")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipLatitude = "";
            //    }
            //    try
            //    {
            //        _ipLongitude = ipInfoXmldoc1.GetElementsByTagName("Longitude")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipLongitude = "";
            //    }
            //    try
            //    {
            //        _ipMetroCode = ipInfoXmldoc1.GetElementsByTagName("MetroCode")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipMetroCode = "";
            //    }
            //}
            //catch
            //{
            //    _ipInnerXml1 = "";
            //    _ipCountryCode = "";
            //    _ipCountryName = "";
            //    _ipRegionCode = "";
            //    _ipRegionName = "";
            //    _ipCity = "";
            //    _ipZipCode = "";
            //    _ipTimeZone = "";
            //    _ipLatitude = "";
            //    _ipLongitude = "";
            //    _ipMetroCode = "";
            //}

            //try
            //{
            //    ipInfoXmldoc2.Load("http://xml.utrace.de/?query=" + _userHostAddress);
            //    _ipInnerXml2 = ipInfoXmldoc2.InnerXml;

            //    try
            //    {
            //        _ipHost = ipInfoXmldoc2.GetElementsByTagName("host")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipHost = "";
            //    }
            //    try
            //    {
            //        _ipISP = ipInfoXmldoc2.GetElementsByTagName("isp")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipISP = "";
            //    }
            //    try
            //    {
            //        _ipOrg = ipInfoXmldoc2.GetElementsByTagName("org")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipOrg = "";
            //    }
            //    try
            //    {
            //        _ipQueries = ipInfoXmldoc2.GetElementsByTagName("queries")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipQueries = "";
            //    }
            //    try
            //    {
            //        _ipRegionName2 = ipInfoXmldoc2.GetElementsByTagName("region")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipRegionName2 = "";
            //    }
            //    try
            //    {
            //        _ipCountryCode2 = ipInfoXmldoc2.GetElementsByTagName("countrycode")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipCountryCode2 = "";
            //    }
            //    try
            //    {
            //        _ipLatitude2 = ipInfoXmldoc2.GetElementsByTagName("latitude")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipLatitude2 = "";
            //    }
            //    try
            //    {
            //        _ipLongitude2 = ipInfoXmldoc2.GetElementsByTagName("longitude")[0].InnerText;
            //    }
            //    catch
            //    {
            //        _ipLongitude2 = "";
            //    }
            //}
            //catch
            //{
            //    _ipInnerXml2 = "";
            //    _ipHost = "";
            //    _ipISP = "";
            //    _ipOrg = "";
            //    _ipQueries = "";
            //    _ipRegionName2 = "";
            //    _ipCountryCode2 = "";
            //    _ipLatitude2 = "";
            //    _ipLongitude2 = "";
            //}
            //return;
        }
    }
}
