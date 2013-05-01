using frontendplay.Models;
using frontendplay.Repositories;
using frontendplay.Utilities;
using frontendplay.ViewModels;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace frontendplay.Controllers
{
  public class BlogController : Controller
  {
    BlogPostRepository repository = new BlogPostRepository();

    protected int entriesPerPage = 5;

     
    // GET: /page/{page}
    [OutputCache(Duration = 60 * 10, VaryByParam = "page")]
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
    [OutputCache(Duration = 60 * 10, VaryByParam = "id")]
    public ActionResult Post(string name, int id)
    {
      BlogPostModel blogpostmodel = repository.Retrieve(id);

      if (blogpostmodel == null)
      {
        return HttpNotFound();
      }

      return View(blogpostmodel);
    }


    // GET: /comment/{id}
    public PartialViewResult Comment(int id)
    { 
      return PartialView(new CommentModel());
    } 


    // POST: /comment/{id}
    [ValidateAntiForgeryToken]
    [HttpPost]
    public JsonResult Comment(CommentModel commentModel, int id)
    {
      // honeypot
      if (!String.IsNullOrEmpty(Request.Form["Body"]))
      {
        return Json(new CommentJsonViewModel()
        {
          success = false,
          output = this.PartialViewToString("_Honeypot", new {})
        });
      }

      // model is fine, go and save
      if (ModelState.IsValid)
      {
        bool success = repository.AddComment(id, commentModel);

        if (success)
        {
          return Json(new CommentJsonViewModel()
          {
            success = true,
            newComment = this.PartialViewToString("_Comment", commentModel),
            output = this.PartialViewToString("CommentSuccess", commentModel)
          });
        }

        // at this time something went wrong while saving
        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
      }

      // validation error
      return Json(new CommentJsonViewModel()
      {
        success = false,
        output = this.PartialViewToString("Comment", commentModel)
      });
    }


    // GET: /about
    [OutputCache(Duration = 60 * 60)]
    public ActionResult About()
    {
      return View(new DefaultViewModel());
    }


    // GET: /archive
    [OutputCache(Duration = 60 * 10)]
    public ActionResult Archive()
    {
      return View(repository.Archive());
    }


    // GET: /feed
    [OutputCache(Duration = 60 * 10)]
    public ActionResult Feed()
    {
      var feed = new SyndicationFeed()
      {
        Language = "en-US",
        Items = repository.Feed(Url),
        Title = SyndicationContent.CreatePlaintextContent("frontendplay"),
        Description = SyndicationContent.CreatePlaintextContent("frontendplay - stories about css, javascript, ASP.NET and PCs"),
        Copyright = SyndicationContent.CreatePlaintextContent("Copyright (C) " + DateTime.Now.Year + " by " + ConfigurationManager.AppSettings["appCreator"]),
        BaseUri = new Uri(Url.Action("Index", "Blog", new {}, "http")),
        LastUpdatedTime = repository.LastUpdate()
      };

      feed.Authors.Add(new SyndicationPerson(ConfigurationManager.AppSettings["adminName"]));

      return new FeedResult(new Rss20FeedFormatter(feed));

    }
  }
}
