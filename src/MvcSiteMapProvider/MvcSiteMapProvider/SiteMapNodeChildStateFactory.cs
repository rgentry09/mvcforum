﻿using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Collections;

namespace MvcSiteMapProvider
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteMapNodeChildStateFactory
        : ISiteMapNodeChildStateFactory
    {
        public SiteMapNodeChildStateFactory(
            IExplicitResourceKeyParser explicitResourceKeyParser,
            IStringLocalizer stringLocalizer,
            IAttributeCollectionFactory attributeCollectionFactory,
            IRouteValueCollectionFactory routeValueCollectionFactory
            )
        {
            if (explicitResourceKeyParser == null)
                throw new ArgumentNullException("explicitResourceKeyParser");
            if (stringLocalizer == null)
                throw new ArgumentNullException("stringLocalizer");
            if (attributeCollectionFactory == null)
                throw new ArgumentNullException("attributeCollectionFactory");
            if (routeValueCollectionFactory == null)
                throw new ArgumentNullException("routeValueCollectionFactory");

            this.explicitResourceKeyParser = explicitResourceKeyParser;
            this.stringLocalizer = stringLocalizer;
            this.attributeCollectionFactory = attributeCollectionFactory;
            this.routeValueCollectionFactory = routeValueCollectionFactory;
        }

        protected readonly IExplicitResourceKeyParser explicitResourceKeyParser;
        protected readonly IStringLocalizer stringLocalizer;
        protected readonly IAttributeCollectionFactory attributeCollectionFactory;
        protected readonly IRouteValueCollectionFactory routeValueCollectionFactory;

        #region ISiteMapNodeChildStateFactory Members

        public ILocalizationService CreateLocalizationService(string implicitResourceKey)
        {
            return new LocalizationService(implicitResourceKey, explicitResourceKeyParser, stringLocalizer);
        }

        public IAttributeCollection CreateAttributeCollection(ISiteMap siteMap, ILocalizationService localizationService)
        {
            return attributeCollectionFactory.Create(siteMap, localizationService);
        }

        public IRouteValueCollection CreateRouteValueCollection(ISiteMap siteMap)
        {
            return routeValueCollectionFactory.Create(siteMap);
        }

        public IPreservedRouteParameterCollection CreatePreservedRouteParameterCollection(ISiteMap siteMap)
        {
            return new PreservedRouteParameterCollection(siteMap);
        }

        public IRoleCollection CreateRoleCollection(ISiteMap siteMap)
        {
            return new RoleCollection(siteMap);
        }

        #endregion
    }
}
