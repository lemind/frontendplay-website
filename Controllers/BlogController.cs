using frontendplay.Models;
using frontendplay.Repositories;
using frontendplay.Utilities;
using frontendplay.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace frontendplay.Controllers
{
  public class BlogController : Controller
  {
    BlogPostRepository repository = new BlogPostRepository();

     
    // GET: /page/{page}
    public ActionResult Index(int page = 1)
    { 
      PostsViewModel model = new PostsViewModel()
      {
        list = repository.List(1, page),
        page = page
      }; 

      return View(model);
    }


    // GET: /{post}-{id}
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
    //[ValidateAntiForgeryToken]
    [HttpPost]
    public JsonResult Comment(CommentModel commentModel, int id)
    {
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

        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
      }

      return Json(new CommentJsonViewModel()
      {
        success = false,
        output = this.PartialViewToString("Comment", commentModel)
      });
    }


    // GET: /about
    public ActionResult About()
    {
      return View(new DefaultViewModel());
    }


    // GET: /archive
    public ActionResult Archive()
    {
      return View(repository.Archive());
    }


    // GET: /feed
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
