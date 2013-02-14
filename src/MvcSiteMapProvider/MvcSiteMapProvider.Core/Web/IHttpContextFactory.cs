﻿using System;
using System.Web;
using System.Web.Routing;

namespace MvcSiteMapProvider.Core.Web
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IHttpContextFactory
    {
        HttpContextBase Create();
        RequestContext CreateRequestContext(RouteData routeData);
    }
}
