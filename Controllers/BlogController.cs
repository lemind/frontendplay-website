using frontendplay.Models;
using frontendplay.Repositories;
using frontendplay.Utilities;
using frontendplay.ViewModels;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace frontendplay.Controllers
{
  public class BlogController : Controller
  {
    BlogPostRepository repository = new BlogPostRepository();

    public static int entriesPerPage = int.Parse(ConfigurationManager.AppSettings["entriesPerPage"]);

     
    // GET: /page/{page}
    [OutputCache(Duration = 4 * 60 * 60, VaryByParam = "page")]
    public ActionResult Index(int page = 1)
    {
      OrderedDictionary pages = repository.Pages(entriesPerPage, page);

      // invalid page
      if(!(bool)pages["valid"])
      {
        return Redirect("/");
      }

      PostsViewModel model = new PostsViewModel()
      {
        list = repository.List(entriesPerPage, page),
        page = page,
        prev = pages["prev"] != null ? Url.Action("Index", new { page = pages["prev"] }) : null,
        next = pages["next"] != null ? Url.Action("Index", new { page = pages["next"] }) : null
      };

      // for meta tags
      ViewBag.RelPrev = model.prev;
      ViewBag.RelNext = model.next;

      return View(model);
    }


    // GET: /{post}-{id}
    [OutputCache(Duration = 4 * 60 * 60, VaryByParam = "id")]
    public ActionResult Post(string name, int id)
    {
      BlogPostModel blogpostmodel = repository.Retrieve(id);

      if (blogpostmodel == null)
      {
        return HttpNotFound();
      }

      return View(blogpostmodel);
    }


    // GET: /about
    [OutputCache(Duration = 24 * 60 * 60)]
    public ActionResult About()
    {
      return View(new DefaultViewModel());
    }


    // GET: /archive
    [OutputCache(Duration = 24 * 60 * 60)]
    public ActionResult Archive()
    {
      return View(repository.Archive());
    }


    // GET: /feed
    [OutputCache(Duration = 2 * 60 * 60)]
    public ActionResult Feed()
    {
      var feed = new SyndicationFeed()
      {
        Language = "en-US",
        Items = repository.Feed(Url),
        Title = SyndicationContent.CreatePlaintextContent("frontendplay"),
        Description = SyndicationContent.CreatePlaintextContent("frontendplay - stories about css, javascript, ASP.NET and PCs"),
        Copyright = SyndicationContent.CreatePlaintextContent("Copyright (C) " + DateTime.Now.Year + " by " + ConfigurationManager.AppSettings["appCreator"]),
        BaseUri = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + "/"),
        LastUpdatedTime = repository.LastUpdate()
      };

      feed.Authors.Add(new SyndicationPerson(ConfigurationManager.AppSettings["adminName"]));

      return new FeedResult(new Rss20FeedFormatter(feed));

    }
  }
}
