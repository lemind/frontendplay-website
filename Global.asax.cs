﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace frontendplay
{
  // Note: For instructions on enabling IIS7 classic mode, 
  // visit http://go.microsoft.com/fwlink/?LinkId=301868
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();

      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      MvcHandler.DisableMvcResponseHeader = true;
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    }
  }
}
