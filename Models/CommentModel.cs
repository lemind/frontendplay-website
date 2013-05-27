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
    [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
    public string Name { get; set; }

    [Required]
    [StringLength(80, ErrorMessage = "Email cannot be longer than 80 characters.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Url)]
    [StringLength(100, ErrorMessage = "Website cannot be longer than 100 characters.")]
    public string Website { get; set; }

    [Required]
    [AllowHtml]
    [StringLength(2000, ErrorMessage = "Comment cannot be longer than 2000 characters.")]
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
        var def = HttpUtility.UrlEncode("http://www.frontendplay.com/Content/Images/default-avatar.png");
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