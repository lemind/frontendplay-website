using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace frontendplay
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.IgnoreRoute("Account/{_}");

      routes.MapRoute(
          name: "Post",
          url: "story/{title}-{id}",
          defaults: new { controller = "Blog", action = "Post", title = string.Empty, id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "Default",
          url: "{action}/{id}",
          defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
      );
    }
  }
}