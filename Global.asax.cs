using MvcSiteMapProvider.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace frontendplay
{
  // Note: For instructions on enabling IIS7 classic mode, 
  // visit http://go.microsoft.com/fwlink/?LinkId=301868
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      MvcHandler.DisableMvcResponseHeader = true;
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

      //MiniProfilerEF.Initialize();

      AreaRegistration.RegisterAllAreas();
      XmlSiteMapController.RegisterRoutes(RouteTable.Routes);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
    }

    //protected void Application_BeginRequest()
    //{
    //  if (Request.IsLocal)
    //  {
    //    MiniProfiler.Start();
    //  }
    //}

    //protected void Application_EndRequest()
    //{
    //  MiniProfiler.Stop();
    //}
  }
}
