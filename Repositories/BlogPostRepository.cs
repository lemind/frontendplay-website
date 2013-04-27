using frontendplay.Models;
using frontendplay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace frontendplay.Repositories
{
  public class BlogPostRepository
  {
    StoreContext db = new StoreContext();


    // finds all current posts
    public IEnumerable<BlogPostModel> List(uint count = 10)
    {
      IEnumerable<BlogPostModel> posts =
        db.BlogPostModels
          .ToList()
          .OrderByDescending(m => m.PublishDate)
          .Take((int)count);

      return posts;
    }


    // gets a single blog post
    public BlogPostModel Retrieve(int id)
    {
      return db.BlogPostModels.FirstOrDefault<BlogPostModel>(m => m.ID == id);
    }


    // groups all posts by month/year
    public IEnumerable<IGrouping<string, BlogPostModel>> Archive()
    {
      IEnumerable<IGrouping<string, BlogPostModel>> posts =
        db.BlogPostModels
          .ToList()
          .OrderByDescending(m => m.PublishDate)
          .GroupBy(m => String.Format("{0:y}", m.PublishDate));

      return posts;
    }


    public void Dispose()
    {
      db.Dispose();
    }
  }
}