using frontendplay.Models;
using frontendplay.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frontendplay.ViewModels
{
  public abstract class LayoutViewModel
  {
    BlogPostRepository repository = new BlogPostRepository();

    public IEnumerable<BlogPostModel> recentPosts
    {
      get
      {
        return repository.List(3);
      }
    }
  }
}