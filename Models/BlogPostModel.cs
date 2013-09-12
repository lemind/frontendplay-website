using frontendplay.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace frontendplay.Models
{
  public class BlogPostModel
  {
    private DateTime _date = DateTime.Now;

    [Required]
    public int ID { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [AllowHtml]
    public string Text { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate
    {
      get { return _date; }
      set { _date = value; }
    }

    public MvcHtmlString HtmlText
    {
      get { return OutputUtilities.Markdown(Text); }
    }

    public MvcHtmlString ShortText
    {
      get 
      {
        var text = Regex.Replace(HtmlText.ToString(), @"<h.>[^</h.>]*</h.>", "");
        text = Regex.Replace(text, @"<[^>]*>", "").Trim();
        return OutputUtilities.Chop(text, int.Parse(ConfigurationManager.AppSettings["previewLength"])); 
      }
    }

    public string Year
    {
      get { return String.Format("{0:yyyy}", PublishDate); }
    }

    public string SMonth
    {
      get { return String.Format("{0:MM}", PublishDate); }
    }

    public string Month
    {
      get { return String.Format("{0:MMM}", PublishDate); }
    }

    public string Day
    {
      get { return PublishDate.Day.ToString(); }
    }

    public string MachineDate
    {
      get { return String.Format("{0:s}", PublishDate); }
    }

    public string FullDate
    {
      get { return String.Format("{0:D}", PublishDate); }
    }
  }
}