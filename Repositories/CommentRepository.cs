using frontendplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace frontendplay.Repositories
{
  public class CommentRepository
  {
    StoreContext db = new StoreContext();

    // finds all comments for a blog post
    public IEnumerable<CommentModel> List(int blogId)
    {
      return 
        db.BlogPostModels
          .FirstOrDefault<BlogPostModel>(m => m.ID == blogId)
          .Comments
          .OrderByDescending(m => m.CreatedDate);
    }
  }
}