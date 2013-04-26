using System.Web;
using System.Web.Optimization;

namespace frontendplay
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/headjavascript").Include(

      ));

      bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
        "~/Content/Scripts/app.js"
      ));

      bundles.Add(new StyleBundle("~/bundles/stylesheet").Include(
        "~/Content/Sass/app.css"
      ));
    }
  }
}