using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace frontendplay.Utilities
{
  public static class OutputUtilities
  {
    private static readonly Markdown MarkdownTransformer = new Markdown();
    private static string postfix = "…";


    // converts markdown to a MvcHtmlString
    public static MvcHtmlString Markdown(string text)
    {
      string html = MarkdownTransformer.Transform(text);

      return new MvcHtmlString(html);
    }

    public static MvcHtmlString Markdown(this MvcHtmlString text)
    {
      return Markdown(text.ToString());
    }



    // chops a MvcHtmlString by the given length
    public static MvcHtmlString Chop(string text, int chopLength = 100, bool isFile = false)
    {
      string rawText = isFile ? Path.GetFileNameWithoutExtension(text) : text;
      string extension = isFile ? Path.GetExtension(text) : "";

      if (text != null && rawText.Length > chopLength)
      {
        rawText = rawText.Substring(0, chopLength - postfix.Length) + postfix;
      }

      return new MvcHtmlString(isFile ? rawText + extension : rawText);
    }

    public static MvcHtmlString Chop(this MvcHtmlString text, int chopLength = 100, bool isFile = false)
    {
      return Chop(text.ToString(), chopLength, isFile);
    }



    // creates md5 hash of a string
    public static string GetMD5Hash(string input)
    {
      // step 1, calculate MD5 hash from input
      MD5 md5 = System.Security.Cryptography.MD5.Create();
      byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
      byte[] hash = md5.ComputeHash(inputBytes);

      // step 2, convert byte array to hex string
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < hash.Length; i++)
      {
        sb.Append(hash[i].ToString("X2"));
      }
      return sb.ToString();
    }


    public static string PartialViewToString(this Controller controller, string viewName, object model)
    {
      if (string.IsNullOrEmpty(viewName))
      {
        viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
      }

      controller.ViewData.Model = model;

      using (StringWriter stringWriter = new StringWriter())
      {
        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
        ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
        viewResult.View.Render(viewContext, stringWriter);
        return stringWriter.GetStringBuilder().ToString();
      }
    }
  }
}