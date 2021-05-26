namespace VET.Site.Util
{
    using cloudscribe.Web.Navigation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public static class NodeExtensions
    {
        public static string AdjustUrl(this TreeNode<NavigationNode> node, HttpContext context, IUrlHelper urlHelper)
        {
            string urlToUse = string.Empty;
            try
            {
                if ((!string.IsNullOrWhiteSpace(node.Value.Action)) && (!string.IsNullOrWhiteSpace(node.Value.Controller)))
                {
                    if (!string.IsNullOrWhiteSpace(node.Value.PreservedRouteParameters))
                    {
                        List<string> preservedParams = node.Value.PreservedRouteParameters.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                        var newRouteValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                        var routeValues = context.GetRouteData();
                        foreach (string p in preservedParams)
                        {
                            if (context.Request.Query.ContainsKey(p))
                            {
                                newRouteValues.Add(p, context.Request.Query[p]);
                            }

                            if (routeValues.Values.TryGetValue(p, out var value))
                            {
                                newRouteValues.Add(p, value);
                            }
                        }

                        if (!string.IsNullOrEmpty(node.Value.Area))
                        {
                            newRouteValues["area"] = node.Value.Area;
                        }

                        urlToUse = urlHelper.Action(node.Value.Action, node.Value.Controller, newRouteValues);

                    }
                    else
                    {
                        urlToUse = urlHelper.Action(node.Value.Action, node.Value.Controller, new { area = node.Value.Area });
                    }

                }
                else if (!string.IsNullOrWhiteSpace(node.Value.NamedRoute))
                {
                    urlToUse = urlHelper.RouteUrl(node.Value.NamedRoute);
                }
                else if (!string.IsNullOrWhiteSpace(node.Value.Page))
                {
                    urlToUse = urlHelper.Page(node.Value.Page, new { area = node.Value.Area });
                }

                string key = NavigationNodeAdjuster.KeyPrefix + node.Value.Key;

                if (context.Items[key] != null)
                {
                    NavigationNodeAdjuster adjuster = (NavigationNodeAdjuster)context.Items[key];
                }

                if (string.IsNullOrEmpty(urlToUse)) { return node.Value.Url; }
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }

            return urlToUse;
        }

    }
}
