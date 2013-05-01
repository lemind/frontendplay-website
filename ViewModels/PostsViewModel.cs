using frontendplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace frontendplay.ViewModels
{
  public class PostsViewModel : LayoutViewModel
  {
    public IEnumerable<BlogPostModel> list { get; set; }

    public int page { get; set; }

    public string next { get; set; }

    public string prev { get; set; }
  }
}