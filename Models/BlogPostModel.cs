using System;
using System.ComponentModel.DataAnnotations;
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

    public string ShortText { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate
    {
      get { return _date; }
      set { _date = value; }
    }
  }
}