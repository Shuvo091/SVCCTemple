using SVCCTempleWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SVCCTempleWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrap.Initialize(Application);
        }
        protected void Application_BeginRequest()
        {
            if (Context.Request.Url.ToString().ToUpper().IndexOf("FREMONT.SVCCTEMPLE.ORG") > -1)
            {
                Response.Redirect("https://www.svcctemple.org/SVCCTempleFremont");
            }
            else
            {
                if (Context.Request.Url.ToString().ToUpper().IndexOf("SACRAMENTO.SVCCTEMPLE.ORG") > -1)
                {
                    Response.Redirect("https://www.svcctemple.org/SVCCTempleSacramento");
                }
                else
                {
                    if (!Context.Request.IsSecureConnection && !Context.Request.Url.ToString().StartsWith("http://localhost:")) //To avoid switching to https when local testing
                    {
                        //Only insert an "s" to the "http:", and avoid replacing wrongly http: in the url parameters
                        Response.Redirect(Context.Request.Url.ToString().Insert(4, "s"));
                    }
                }
            }
        }
    }
}
