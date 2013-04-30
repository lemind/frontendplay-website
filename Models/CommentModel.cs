using frontendplay.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace frontendplay.Models
{
  public class CommentModel
  {
    private DateTime _date = DateTime.Now;

    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Url)]
    public string Website { get; set; }

    [Required]
    [AllowHtml]
    public string Comment { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate
    {
      get { return _date; }
      set { _date = value; }
    }

    public string Image
    {
      get 
      { 
        var hash = OutputUtilities.GetMD5Hash(Email.Trim().ToLower()).ToLower();
        var def = HttpUtility.UrlEncode("http://localhost:50122/Content/Images/default-avatar.png");
        return "http://www.gravatar.com/avatar/" + hash + "?size=60&default=" + def; 
      }
    }

    public string MachineDate
    {
      get { return String.Format("{0:s}", CreatedDate); }
    }

    public string FullDate
    {
      get { return String.Format("{0:MMMM d, yyyy HH:mm}", CreatedDate); }
    }
  }
}