using frontendplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace frontendplay.ViewModels
{
  public class PostsViewModel
  {
    public IEnumerable<BlogPostModel> list { get; set; }

    public string previousPosts
    {
      get
      {
        return "/previous";
      }
    }

    public string nextPosts
    {
      get
      {
        return "/next";
      }
    }
  }
}