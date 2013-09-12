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

      routes.MapRoute(
          name: "Error",
          url: "error/{action}",
          defaults: new { controller = "Error", action = "NotFound" }
      );

      routes.MapRoute(
          name: "Post",
          url: "story/{id}/{title}",
          defaults: new { controller = "Blog", action = "Post", title = string.Empty, id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "Pagination",
          url: "page/{page}",
          defaults: new { controller = "Blog", action = "Index", page = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "Default",
          url: "{action}/{id}",
          defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
      );
    }
  }
}
