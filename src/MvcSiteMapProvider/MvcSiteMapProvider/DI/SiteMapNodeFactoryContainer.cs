﻿using System;
using System.Linq;
using System.Web.Hosting;
using System.Collections.Generic;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.UrlResolver;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Collections.Specialized;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Xml;

namespace MvcSiteMapProvider.DI
{
    /// <summary>
    /// A specialized dependency injection container for resolving a <see cref="T:MvcSiteMapProvider.SiteMapNodeFactory"/> instance.
    /// </summary>
    public class SiteMapNodeFactoryContainer
    {
        public SiteMapNodeFactoryContainer(
            ConfigurationSettings settings,
            IMvcContextFactory mvcContextFactory,
            IUrlPath urlPath)
        {
            this.absoluteFileName = HostingEnvironment.MapPath(settings.SiteMapFileName);
            this.settings = settings;
            this.mvcContextFactory = mvcContextFactory;
            this.requestCache = this.mvcContextFactory.GetRequestCache();
            this.urlPath = urlPath;
            this.assemblyProvider = new AttributeAssemblyProvider(settings.IncludeAssembliesForScan, settings.ExcludeAssembliesForScan);
            this.mvcSiteMapNodeAttributeProvider = new MvcSiteMapNodeAttributeDefinitionProvider();
            this.dynamicNodeProviders = this.ResolveDynamicNodeProviders();
            this.siteMapNodeUrlResolvers = this.ResolveSiteMapNodeUrlResolvers();
            this.siteMapNodeVisibilityProviders = this.ResolveSiteMapNodeVisibilityProviders(settings.DefaultSiteMapNodeVisibiltyProvider);
        }

        private readonly string absoluteFileName;
        private readonly ConfigurationSettings settings;
        private readonly IMvcContextFactory mvcContextFactory;
        private readonly IRequestCache requestCache;
        private readonly IUrlPath urlPath;
        private readonly IDynamicNodeProvider[] dynamicNodeProviders;
        private readonly ISiteMapNodeUrlResolver[] siteMapNodeUrlResolvers;
        private readonly ISiteMapNodeVisibilityProvider[] siteMapNodeVisibilityProviders;

        private readonly XmlDistinctAttributeAggregator xmlAggergator 
            = new XmlDistinctAttributeAggregator(new SiteMapXmlNameProvider());
        private readonly IAttributeAssemblyProvider assemblyProvider;
        private readonly IMvcSiteMapNodeAttributeDefinitionProvider mvcSiteMapNodeAttributeProvider;

        public ISiteMapNodeFactory ResolveSiteMapNodeFactory()
        {
            return new SiteMapNodeFactory(
                this.ResolveSiteMapNodeChildStateFactory(),
                this.ResolveLocalizationServiceFactory(),
                this.ResolveSiteMapNodePluginProvider(),
                this.urlPath,
                this.mvcContextFactory); 
        }

        private ISiteMapNodeChildStateFactory ResolveSiteMapNodeChildStateFactory()
        {
            return new SiteMapNodeChildStateFactory(
                new AttributeDictionaryFactory(this.requestCache),
                new RouteValueDictionaryFactory(this.requestCache));
        }

        private ILocalizationServiceFactory ResolveLocalizationServiceFactory()
        {
            return new LocalizationServiceFactory(
                new ExplicitResourceKeyParser(),
                new StringLocalizer(this.mvcContextFactory));
        }

        private ISiteMapNodePluginProvider ResolveSiteMapNodePluginProvider()
        {
            return new SiteMapNodePluginProvider(
                new DynamicNodeProviderStrategy(this.dynamicNodeProviders),
                new SiteMapNodeUrlResolverStrategy(this.siteMapNodeUrlResolvers),
                new SiteMapNodeVisibilityProviderStrategy(this.siteMapNodeVisibilityProviders, settings.DefaultSiteMapNodeVisibiltyProvider));
        }

        private IDynamicNodeProvider[] ResolveDynamicNodeProviders()
        {
            var instantiator = new PluginInstantiator<IDynamicNodeProvider>();
            var xmlSource = new FileXmlSource(this.absoluteFileName);
            var typeNames = xmlAggergator.GetAttributeValues(xmlSource, "dynamicNodeProvider");

            var attributeTypeNames = this.GetMvcSiteMapNodeAttributeDynamicNodeProviderNames();
            foreach (var typeName in attributeTypeNames)
            {
                if (!typeNames.Contains(typeName))
                {
                    typeNames.Add(typeName);
                }
            }

            var providers = instantiator.GetInstances(typeNames);
            return providers.ToArray();
        }

        private ISiteMapNodeUrlResolver[] ResolveSiteMapNodeUrlResolvers()
        {
            var instantiator = new PluginInstantiator<ISiteMapNodeUrlResolver>();
            var xmlSource = new FileXmlSource(this.absoluteFileName);
            var typeNames = xmlAggergator.GetAttributeValues(xmlSource, "urlResolver");

            var attributeTypeNames = this.GetMvcSiteMapNodeAttributeUrlResolverNames();
            foreach (var typeName in attributeTypeNames)
            {
                if (!typeNames.Contains(typeName))
                {
                    typeNames.Add(typeName);
                }
            }

            // Add the default provider if it is missing
            var defaultName = typeof(SiteMapNodeUrlResolver).ShortAssemblyQualifiedName();
            if (!typeNames.Contains(defaultName))
            {
                typeNames.Add(defaultName);
            }

            var providers = instantiator.GetInstances(typeNames, new object[] { this.mvcContextFactory, this.urlPath });
            return providers.ToArray();
        }

        private ISiteMapNodeVisibilityProvider[] ResolveSiteMapNodeVisibilityProviders(string defaultVisibilityProviderName)
        {
            var instantiator = new PluginInstantiator<ISiteMapNodeVisibilityProvider>();
            var xmlSource = new FileXmlSource(this.absoluteFileName);
            var typeNames = xmlAggergator.GetAttributeValues(xmlSource, "visibilityProvider");

            var attributeTypeNames = this.GetMvcSiteMapNodeAttributeVisibilityProviderNames();
            foreach (var typeName in attributeTypeNames)
            {
                if (!typeNames.Contains(typeName))
                {
                    typeNames.Add(typeName);
                }
            }

            // Fixes #196, default instance not created.
            if (!String.IsNullOrEmpty(defaultVisibilityProviderName) && !typeNames.Contains(defaultVisibilityProviderName))
            {
                typeNames.Add(defaultVisibilityProviderName);
            }
            var providers = instantiator.GetInstances(typeNames);
            return providers.ToArray();
        }


        private IEnumerable<string> GetMvcSiteMapNodeAttributeDynamicNodeProviderNames()
        {
            var result = new List<string>();
            if (this.settings.ScanAssembliesForSiteMapNodes)
            {
                var assemblies = assemblyProvider.GetAssemblies();
                var definitions = mvcSiteMapNodeAttributeProvider.GetMvcSiteMapNodeAttributeDefinitions(assemblies);
                result.AddRange(definitions
                    .Where(x => !String.IsNullOrEmpty(x.SiteMapNodeAttribute.DynamicNodeProvider))
                    .Select(x => x.SiteMapNodeAttribute.DynamicNodeProvider)
                    );
            }
            return result;
        }

        private IEnumerable<string> GetMvcSiteMapNodeAttributeUrlResolverNames()
        {
            var result = new List<string>();
            if (this.settings.ScanAssembliesForSiteMapNodes)
            {
                var assemblies = assemblyProvider.GetAssemblies();
                var definitions = mvcSiteMapNodeAttributeProvider.GetMvcSiteMapNodeAttributeDefinitions(assemblies);
                result.AddRange(definitions
                    .Where(x => !String.IsNullOrEmpty(x.SiteMapNodeAttribute.UrlResolver))
                    .Select(x => x.SiteMapNodeAttribute.UrlResolver)
                    );
            }
            return result;
        }

        private IEnumerable<string> GetMvcSiteMapNodeAttributeVisibilityProviderNames()
        {
            var result = new List<string>();
            if (this.settings.ScanAssembliesForSiteMapNodes)
            {
                var assemblies = assemblyProvider.GetAssemblies();
                var definitions = mvcSiteMapNodeAttributeProvider.GetMvcSiteMapNodeAttributeDefinitions(assemblies);
                result.AddRange(definitions
                    .Where(x => !String.IsNullOrEmpty(x.SiteMapNodeAttribute.VisibilityProvider))
                    .Select(x => x.SiteMapNodeAttribute.VisibilityProvider)
                    );
            }
            return result;
        }
    }
}
