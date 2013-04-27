using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace frontendplay.Utilities
{
  public class UrlEncoder
  {
    public static string ToFriendlyUrl(string urlToEncode)
    {
      urlToEncode = (urlToEncode ?? "").Trim().ToLower();

      StringBuilder url = new StringBuilder();

      foreach (char ch in urlToEncode)
      {
        switch (ch)
        {
          case ' ':
            url.Append('-');
            break;
          case '&':
            url.Append("and");
            break;
          case '\'':
            break;
          default:
            if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z'))
            {
              url.Append(ch);
            }
            else
            {
              url.Append('-');
            }
            break;
        }
      }

      return url.ToString();
    } 
  }
}