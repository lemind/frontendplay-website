using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using frontendplay.Models;

namespace frontendplay.Controllers
{
    public class BlogPostController : Controller
    {
      private StoreContext db = new StoreContext();



      // GET: /BlogPost/
      public ActionResult Index()
      {
          return View(db.BlogPostModels.ToList());
      }



      // GET: /BlogPost/Details/5
      public ActionResult Details(int id = 0)
      {
          BlogPostModel blogpostmodel = db.BlogPostModels.Find(id);
          if (blogpostmodel == null)
          {
              return HttpNotFound();
          }
          return View(blogpostmodel);
      }



      protected override void Dispose(bool disposing)
      {
          db.Dispose();
          base.Dispose(disposing);
      }
    }
}