﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Routing;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Web.UrlResolver
{
    /// <summary>
    /// Default SiteMapNode URL resolver.
    /// </summary>
    public class SiteMapNodeUrlResolver
        : SiteMapNodeUrlResolverBase
    {
        public SiteMapNodeUrlResolver(
            IMvcContextFactory mvcContextFactory,
            IUrlPath urlPath
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");

            this.mvcContextFactory = mvcContextFactory;
            this.urlPath = urlPath;
        }

        protected readonly IMvcContextFactory mvcContextFactory;
        protected readonly IUrlPath urlPath;

        #region ISiteMapNodeUrlResolver Members

        /// <summary>
        /// Resolves the URL.
        /// </summary>
        /// <param name="node">The MVC site map node.</param>
        /// <param name="area">The area.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The resolved URL.</returns>
        public override string ResolveUrl(ISiteMapNode node, string area, string controller, string action, IDictionary<string, object> routeValues)
        {
            if (!string.IsNullOrEmpty(node.UnresolvedUrl))
            {
                return this.ResolveVirtualPath(node);
            }
            return this.ResolveRouteUrl(node, area, controller, action, routeValues);
        }

        #endregion

        protected virtual string ResolveVirtualPath(ISiteMapNode node)
        {
            var url = node.UnresolvedUrl;
            if (!urlPath.IsAbsoluteUrl(url))
            {
                return urlPath.MakeVirtualPathAppAbsolute(urlPath.Combine(urlPath.AppDomainAppVirtualPath, url));
            }
            return url;
        }

        protected virtual string ResolveRouteUrl(ISiteMapNode node, string area, string controller, string action, IDictionary<string, object> routeValues)
        {
            string result = string.Empty;
            // Create a TextWriter with null stream as a backing stream 
            // which doesn't consume resources
            using (var nullWriter = new StreamWriter(Stream.Null))
            {
                var requestContext = this.CreateRequestContext(node, true, nullWriter);
                result = this.ResolveRouteUrl(node, area, controller, action, routeValues, requestContext);
            }

            if (string.IsNullOrEmpty(result))
            {
                // fixes #115 - UrlResolver should not throw exception.
                return urlPath.MakeVirtualPathAppAbsolute(urlPath.Combine(urlPath.AppDomainAppVirtualPath, "~/")) + Guid.NewGuid().ToString();
            }

            return result;
        }

        protected virtual string ResolveRouteUrl(ISiteMapNode node, string area, string controller, string action, IDictionary<string, object> routeValues, RequestContext requestContext)
        {
            string result = string.Empty;
            var urlHelper = this.mvcContextFactory.CreateUrlHelper(requestContext);
            var routeValueDictionary = new RouteValueDictionary(routeValues);

            if (!string.IsNullOrEmpty(node.Route))
            {
                routeValueDictionary.Remove("route");
                result = urlHelper.RouteUrl(node.Route, routeValueDictionary);
            }
            else
            {
                result = urlHelper.Action(action, controller, routeValueDictionary);
            }

            return result;
        }

        protected virtual HttpContextBase CreateHttpContext(ISiteMapNode node, TextWriter writer)
        {
            var currentHttpContext = this.mvcContextFactory.CreateHttpContext();

            // Create a URI with the home page and no query string values.
            var uri = new Uri(currentHttpContext.Request.Url, "/");
            return this.mvcContextFactory.CreateHttpContext(node, uri, writer);
        }

        protected virtual RequestContext CreateRequestContext(ISiteMapNode node, bool includeAmbientRequestValues, TextWriter writer)
        {
            if (!includeAmbientRequestValues)
            {
                var httpContext = this.CreateHttpContext(node, writer);
                return this.mvcContextFactory.CreateRequestContext(httpContext);
            }
            else
            {
                return this.mvcContextFactory.CreateRequestContext();
            }
        }
    }
}
