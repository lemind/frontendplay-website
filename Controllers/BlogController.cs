using frontendplay.Models;
using frontendplay.Repositories;
using frontendplay.ViewModels;
using System.Web.Mvc;

namespace frontendplay.Controllers
{
  public class BlogController : Controller
  {
    BlogPostRepository repository = new BlogPostRepository();


    // GET: /
    public ActionResult Index()
    {
      PostsViewModel model = new PostsViewModel();
      model.list = repository.List();

      return View(model);
    }


    // GET: /{post}-{id}
    public ActionResult Post(int id = 0)
    {
      BlogPostModel blogpostmodel = repository.Retrieve(id);

      if (blogpostmodel == null)
      {
        return HttpNotFound();
      }
      return View(blogpostmodel);
    }


    // GET: /about
    public ActionResult About()
    {
      return View();
    }


    // GET: /archive
    public ActionResult Archive()
    {
      return View();
    }
  }
}
