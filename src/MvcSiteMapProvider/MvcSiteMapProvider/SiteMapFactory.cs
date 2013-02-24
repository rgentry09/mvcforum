﻿using System;
using System.Web.Routing;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Security;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Collections;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider
{
    /// <summary>
    /// An abstract factory that can be used to create new instances of <see cref="T:MvcSiteMapProvider.RequestCacheableSiteMap"/>
    /// at runtime.
    /// </summary>
    public class SiteMapFactory
        : ISiteMapFactory
    {
        public SiteMapFactory(
            IAclModule aclModule,
            IMvcContextFactory mvcContextFactory,
            ISiteMapNodeCollectionFactory siteMapNodeCollectionFactory,
            IGenericDictionaryFactory genericDictionaryFactory,
            IUrlPath urlPath,
            RouteCollection routes,
            IRequestCache requestCache,
            IControllerTypeResolverFactory controllerTypeResolverFactory,
            IActionMethodParameterResolverFactory actionMethodParameterResolverFactory,
            IMvcResolverFactory mvcResolverFactory
            )
        {
            if (aclModule == null)
                throw new ArgumentNullException("aclModule");
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");
            if (siteMapNodeCollectionFactory == null)
                throw new ArgumentNullException("siteMapNodeCollectionFactory");
            if (genericDictionaryFactory == null)
                throw new ArgumentNullException("genericDictionaryFactory");
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (requestCache == null)
                throw new ArgumentNullException("requestCache");
            if (controllerTypeResolverFactory == null)
                throw new ArgumentNullException("controllerTypeResolverFactory");
            if (actionMethodParameterResolverFactory == null)
                throw new ArgumentNullException("actionMethodParameterResolverFactory");
            if (mvcResolverFactory == null)
                throw new ArgumentNullException("mvcResolverFactory");

            this.aclModule = aclModule;
            this.mvcContextFactory = mvcContextFactory;
            this.siteMapNodeCollectionFactory = siteMapNodeCollectionFactory;
            this.genericDictionaryFactory = genericDictionaryFactory;
            this.urlPath = urlPath;
            this.routes = routes;
            this.requestCache = requestCache;
            this.controllerTypeResolverFactory = controllerTypeResolverFactory;
            this.actionMethodParameterResolverFactory = actionMethodParameterResolverFactory;
            this.mvcResolverFactory = mvcResolverFactory;
        }

        protected readonly IAclModule aclModule;
        protected readonly IMvcContextFactory mvcContextFactory;
        protected readonly ISiteMapNodeCollectionFactory siteMapNodeCollectionFactory;
        protected readonly IGenericDictionaryFactory genericDictionaryFactory;
        protected readonly IUrlPath urlPath;
        protected readonly RouteCollection routes;
        protected readonly IRequestCache requestCache;
        protected readonly IControllerTypeResolverFactory controllerTypeResolverFactory;
        protected readonly IActionMethodParameterResolverFactory actionMethodParameterResolverFactory;
        protected readonly IMvcResolverFactory mvcResolverFactory;

        #region ISiteMapFactory Members

        public virtual ISiteMap Create(ISiteMapBuilder siteMapBuilder)
        {
            // IMPORTANT: We need to ensure there is one instance of controllerTypeResolver and 
            // one instance of ActionMethodParameterResolver per SiteMap instance.
            var controllerTypeResolver = this.controllerTypeResolverFactory.Create(routes);
            var actionMethodParameterResolver = actionMethodParameterResolverFactory.Create();

            var mvcResolver = mvcResolverFactory.Create(controllerTypeResolver, actionMethodParameterResolver);

            return new RequestCacheableSiteMap(
                siteMapBuilder,
                mvcResolver,
                mvcContextFactory,
                aclModule,
                siteMapNodeCollectionFactory,
                genericDictionaryFactory,
                urlPath,
                routes,
                requestCache);
        }

        #endregion
    }
}
