using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace frontendplay.ViewModels
{
  public class CommentJsonViewModel
  {
    public bool success { get; set; }

    public string newComment { get; set; }

    public string output { get; set; }
  }
}