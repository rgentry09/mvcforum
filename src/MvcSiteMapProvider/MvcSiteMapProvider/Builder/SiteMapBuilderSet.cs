﻿using System;
using System.Web.Caching;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Builder
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SiteMapBuilderSet
        : ISiteMapBuilderSet
    {
        public SiteMapBuilderSet(
            string name,
            ISiteMapBuilder siteMapBuilder,
            ICacheDependencyFactory cacheDependencyFactory
            )
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (siteMapBuilder == null)
                throw new ArgumentNullException("siteMapBuilder");
            if (cacheDependencyFactory == null)
                throw new ArgumentNullException("cacheDependencyFactory");

            this.name = name;
            this.siteMapBuilder = siteMapBuilder;
            this.cacheDependencyFactory = cacheDependencyFactory;
        }

        protected readonly string name;
        protected readonly ISiteMapBuilder siteMapBuilder;
        protected readonly ICacheDependencyFactory cacheDependencyFactory;


        #region ISiteMapBuilderSet<CacheDependency> Members

        public string Name
        {
            get { return this.name; }
        }

        public ISiteMapBuilder Builder
        {
            get { return this.siteMapBuilder; }
        }

        public ICacheDependency CreateCacheDependency()
        {
            return this.cacheDependencyFactory.Create();
        }

        public bool AppliesTo(string builderSetName)
        {
            return this.name.Equals(builderSetName);
        }

        #endregion
    }
}
