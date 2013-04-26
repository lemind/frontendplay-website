using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace frontendplay.Controllers
{
  public class HomeController : Controller
  {
    // GET home page
    public ActionResult Index()
    {
      return View();
    }


    // GET about page
    public ActionResult About()
    {
      return View();
    }


    // GET archive page
    public ActionResult Archive()
    {
      return View();
    }
  }
}
