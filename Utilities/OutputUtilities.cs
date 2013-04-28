using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
  }
}