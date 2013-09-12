using frontendplay.Models;
using frontendplay.Utilities;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace frontendplay.Repositories
{
  public class BlogPostDynamicNodeProvider : DynamicNodeProviderBase
  {
    StoreContext db = new StoreContext();


    public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
    {
      var nodeList = new List<DynamicNode>();

      foreach (var item in db.BlogPostModels.ToList())
      {
        var title = UrlEncoder.ToFriendlyUrl(item.Title);
        DynamicNode node = new DynamicNode()
        {
          Title = title
        };

        node.RouteValues.Add("id", item.ID);
        node.RouteValues.Add("title", title);

        nodeList.Add(node);
      }

      return nodeList;
    }


    //public override CacheDescription GetCacheDescription()
    //{
    //  return new CacheDescription("BlogPostDynamicNodeProvider") { SlidingExpiration = TimeSpan.FromMinutes(10) };
    //} 
  }
}