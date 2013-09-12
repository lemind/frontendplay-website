using frontendplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using frontendplay.Utilities;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;

namespace frontendplay.Repositories
{
  public class BlogPostRepository
  {
    StoreContext db = new StoreContext();


    // finds all current posts
    public IEnumerable<BlogPostModel> List(int count = 10, int page = 1)
    {
      IEnumerable<BlogPostModel> posts =
        db.BlogPostModels
          .ToList()
          .OrderByDescending(m => m.PublishDate)
          .Skip((page - 1) * count) 
          .Take(count);

      return posts;
    }


    // gets a single blog post
    public BlogPostModel Retrieve(int id)
    {
      return db.BlogPostModels.FirstOrDefault<BlogPostModel>(m => m.ID == id);
    }


    // get page count
    public int PageCount(int count)
    {
      int countAll = db.BlogPostModels.ToList().Count();

      int countPages = (int)(countAll / count);
      if (countAll % count != 0)
      {
        countPages += 1;
      }

      return countPages;
    }


    // finds pagination links
    public OrderedDictionary Pages(int count, int page)
    {
      OrderedDictionary links = new OrderedDictionary();
      int countPages = PageCount(count);

      links["valid"] = true;

      // invalid page count
      if(page > countPages)
      {
        links["valid"] = false;
        return links;
      }

      // no links in this case
      if(countPages == 1)
      {
        links["next"] = null;
        links["prev"] = null;
      }
      // got links!
      else
      {
        links["next"] = page - 1;
        links["prev"] = page + 1;

        // edge cases
        if(page == 1)
        {
          links["next"] = null;
        }
        if(page == countPages)
        {
          links["prev"] = null;
        }
      }

      return links;
    }


    // groups all posts by month
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
          Summary = SyndicationContent.CreatePlaintextContent(post.ShortText.ToString()),
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