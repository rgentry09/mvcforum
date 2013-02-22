﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Web
{
    /// <summary>
    /// An abstract factory that can be used to create new instances of <see cref="T:MvcSiteMapProvider.Web.MvcHttpContext"/>
    /// at runtime.
    /// </summary>
    public class HttpContextFactory
        : IHttpContextFactory
    {
        #region IHttpContextFactory Members

        public virtual HttpContextBase Create()
        {
            // TODO: Change this to HttpContextWrapper to make it generic to pass around and add
            // methods here to wrap in specified classes when needed.
            //return new MvcHttpContext(context);

            return new MvcHttpContext(HttpContext.Current);
        }

        public virtual RequestContext CreateRequestContext(RouteData routeData)
        {
            var httpContext = this.Create();
            //return new RequestContext(httpContext, routeData);

            if (httpContext.Handler is MvcHandler)
                return ((MvcHandler)httpContext.Handler).RequestContext;
            else
                return new RequestContext(httpContext, routeData);
        }

        public virtual RequestContext CreateRequestContext()
        {
            var httpContext = this.Create();
            if (httpContext.Handler is MvcHandler)
                return ((MvcHandler)httpContext.Handler).RequestContext;
            else
                return new RequestContext(httpContext, new RouteData());
        }

        public virtual ControllerContext CreateControllerContext(RequestContext requestContext, ControllerBase controller)
        {
            return new ControllerContext(requestContext, controller);
        }

        public virtual IRequestCache GetRequestCache()
        {
            return new RequestCache(this);
        }

        public virtual IUrlHelper CreateUrlHelper(RequestContext requestContext)
        {
            return new UrlHelperAdaptor(requestContext);
        }

        public virtual IUrlHelper CreateUrlHelper()
        {
            var requestContext = this.CreateRequestContext();
            return new UrlHelperAdaptor(requestContext);
        }

        #endregion
    }
}
