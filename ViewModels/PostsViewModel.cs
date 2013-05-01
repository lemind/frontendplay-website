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

    public string previousPosts
    {
      get
      {
        return "/page/" + (page + 1).ToString();
      }
    }

    public string nextPosts
    {
      get
      {
        return "/page/" + (page - 1).ToString();
      }
    }
  }
}