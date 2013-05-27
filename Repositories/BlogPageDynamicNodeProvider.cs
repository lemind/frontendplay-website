using frontendplay.Controllers;
using frontendplay.Models;
using frontendplay.Utilities;
using MvcSiteMapProvider.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace frontendplay.Repositories
{
  public class BlogPageDynamicNodeProvider : DynamicNodeProviderBase
  {
    BlogPostRepository repository = new BlogPostRepository();


    public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
    {
      var nodeList = new List<DynamicNode>();
      int countPages = repository.PageCount(BlogController.entriesPerPage);

      while(countPages > 1)
      {
        DynamicNode node = new DynamicNode()
        {
          Title = "page_" + countPages.ToString()
        };

        node.RouteValues.Add("page", countPages);

        nodeList.Add(node);

        countPages -= 1;
      }

      return nodeList;
    }


    public override CacheDescription GetCacheDescription()
    {
      return new CacheDescription("BlogPageDynamicNodeProvider") { SlidingExpiration = TimeSpan.FromMinutes(60 * 60 * 6) };
    } 
  }
}