using frontendplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using frontendplay.Utilities;
using System.Configuration;
using System.Data;

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


    // gets a single blog post
    public bool AddComment(int postId, CommentModel comment)
    {
      try
      {
        BlogPostModel blogpostmodel = Retrieve(postId);

        if (blogpostmodel == null)
        {
          throw new DataException();
        }

        blogpostmodel.Comments.Add(comment);
        db.SaveChanges();

        return true;
      }
      catch(DataException)
      {
        return false;
      }
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


    // get last updated blog post datetime
    public DateTime LastUpdate()
    {
      return
        db.BlogPostModels
          .OrderByDescending(m => m.PublishDate)
          .FirstOrDefault<BlogPostModel>().PublishDate;
    }


    // finds all posts and convert to syndication items
    public List<SyndicationItem> Feed(UrlHelper urlHelper)
    {
      IEnumerable<BlogPostModel> posts =
        db.BlogPostModels
          .ToList()
          .OrderByDescending(m => m.PublishDate);

      List<SyndicationItem> feed = new List<SyndicationItem>();

      foreach(var post in posts)
      {
        var itemUrl = urlHelper.Action("Post", "Blog", new { title = UrlEncoder.ToFriendlyUrl(post.Title), id = post.ID }, "http");

        SyndicationItem item = new SyndicationItem(post.Title, SyndicationContent.CreateHtmlContent(post.HtmlText.ToString()), new Uri(itemUrl), itemUrl, post.PublishDate)
        {
          PublishDate = post.PublishDate,
          Summary = SyndicationContent.CreatePlaintextContent(post.ShortText),
          BaseUri = new Uri(urlHelper.Action("Index", "Blog", new {}, "http")),
        };

        item.Links.Add(new SyndicationLink(new Uri(itemUrl)));
        item.Authors.Add(new SyndicationPerson(post.Author));

        item.Contributors.Add(new SyndicationPerson(
          ConfigurationManager.AppSettings["adminEmail"], post.Author, urlHelper.Action("About", "Blog", new { }, "http")
        ));

        feed.Add(item);
      }

      return feed;
    }


    public void Dispose()
    {
      db.Dispose();
    }
  }
}