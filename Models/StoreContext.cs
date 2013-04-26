using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace frontendplay.Models
{
  public class StoreContext : DbContext
  {
    public StoreContext()
      : base("DefaultConnection")
    {

    }

    public DbSet<BlogPostModel> BlogPostModels { get; set; }

    public DbSet<UserProfile> UserProfiles { get; set; }
  }
}