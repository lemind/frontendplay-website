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


    // groups all posts by year
    public IEnumerable<IGrouping<int, BlogPostModel>> Archive()
    {
      IEnumerable<IGrouping<int, BlogPostModel>> posts =
        db.BlogPostModels
          .ToList()
          .OrderByDescending(m => m.PublishDate)
          .GroupBy(m => m.PublishDate.Year);

      return posts;
    }


    public void Dispose()
    {
      db.Dispose();
    }
  }
}