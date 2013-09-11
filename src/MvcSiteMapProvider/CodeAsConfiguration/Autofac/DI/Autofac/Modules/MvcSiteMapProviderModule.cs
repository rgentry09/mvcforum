﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Web.Routing;
using System.Reflection;
using Autofac;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Visitor;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.Compilation;
using MvcSiteMapProvider.Web.Mvc.Filters;
using MvcSiteMapProvider.Web.UrlResolver;
using MvcSiteMapProvider.Security;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Xml;
using MvcSiteMapProvider.Globalization;

namespace DI.Autofac.Modules
{
    public class MvcSiteMapProviderModule
        : global::Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            bool securityTrimmingEnabled = false;
            bool enableLocalization = true;
            string absoluteFileName = HostingEnvironment.MapPath("~/Mvc.sitemap");
            TimeSpan absoluteCacheExpiration = TimeSpan.FromMinutes(5);
            string[] includeAssembliesForScan = new string[] { "$AssemblyName$" };

            var currentAssembly = this.GetType().Assembly;
            var siteMapProviderAssembly = typeof(SiteMaps).Assembly;
            var allAssemblies = new Assembly[] { currentAssembly, siteMapProviderAssembly };
            var excludeTypes = new Type[] { 
                typeof(SiteMapNodeVisibilityProviderStrategy),
                typeof(SiteMapXmlReservedAttributeNameProvider),
                typeof(SiteMapBuilderSetStrategy)
            };
            var multipleImplementationTypes = new Type[]  { 
                typeof(ISiteMapNodeUrlResolver), 
                typeof(ISiteMapNodeVisibilityProvider), 
                typeof(IDynamicNodeProvider) 
            };

            // Single implementations of interface with matching name (minus the "I").
            CommonConventions.RegisterDefaultConventions(
                (interfaceType, implementationType) => builder.RegisterType(implementationType).As(interfaceType).SingleInstance(),
                new Assembly[] { siteMapProviderAssembly },
                allAssemblies,
                excludeTypes,
                string.Empty);

            // Multiple implementations of strategy based extension points
            CommonConventions.RegisterAllImplementationsOfInterface(
                (interfaceType, implementationType) => builder.RegisterType(implementationType).As(interfaceType).SingleInstance(),
                multipleImplementationTypes,
                allAssemblies,
                excludeTypes,
                "^Composite");

            // Registration of internal controllers
            CommonConventions.RegisterAllImplementationsOfInterface(
                (interfaceType, implementationType) => builder.RegisterType(implementationType).As(interfaceType).AsSelf().InstancePerDependency(),
                new Type[] { typeof(IController) },
                new Assembly[] { siteMapProviderAssembly },
                new Type[0],
                string.Empty);

            // Visibility Providers
            builder.RegisterType<SiteMapNodeVisibilityProviderStrategy>()
                .As<ISiteMapNodeVisibilityProviderStrategy>()
                .WithParameter("defaultProviderName", string.Empty);

            // Pass in the global controllerBuilder reference
            builder.RegisterInstance(ControllerBuilder.Current)
                   .As<ControllerBuilder>();

            builder.RegisterType<BuildManagerAdaptor>()
                   .As<IBuildManager>();

            builder.RegisterType<ControllerBuilderAdaptor>()
                   .As<IControllerBuilder>();

            builder.RegisterType<ControllerTypeResolverFactory>()
                .As<IControllerTypeResolverFactory>()
                .WithParameter("areaNamespacesToIgnore", new string[0]);

            // Configure Security
            builder.RegisterType<AuthorizeAttributeAclModule>()
                .Named<IAclModule>("authorizeAttributeAclModule");
            builder.RegisterType<XmlRolesAclModule>()
                .Named<IAclModule>("xmlRolesAclModule");
            builder.RegisterType<CompositeAclModule>()
                .As<IAclModule>()
                .WithParameter(
                    (p, c) => p.Name == "aclModules",
                    (p, c) => new[]
                        {
                            c.ResolveNamed<IAclModule>("authorizeAttributeAclModule"),
                            c.ResolveNamed<IAclModule>("xmlRolesAclModule")
                        });

#if NET35
            builder.RegisterGeneric(typeof(AspNetCacheProvider<>))
                .As(typeof(ICacheProvider<>));

            builder.RegisterType<AspNetFileCacheDependency>()
                .Named<ICacheDependency>("cacheDependency1")
                .WithParameter("fileName", absoluteFileName);
#else
            builder.RegisterInstance(System.Runtime.Caching.MemoryCache.Default)
                   .As<System.Runtime.Caching.ObjectCache>();

            builder.RegisterGeneric(typeof(RuntimeCacheProvider<>))
                .As(typeof(ICacheProvider<>));

            builder.RegisterType<RuntimeFileCacheDependency>()
                .Named<ICacheDependency>("cacheDependency1")
                .WithParameter("fileName", absoluteFileName);
#endif
            builder.RegisterType<CacheDetails>()
                .Named<ICacheDetails>("cacheDetails1")
                .WithParameter("absoluteCacheExpiration", absoluteCacheExpiration)
                .WithParameter("slidingCacheExpiration", TimeSpan.MinValue)
                .WithParameter(
                    (p, c) => p.Name == "cacheDependency",
                    (p, c) => c.ResolveNamed<ICacheDependency>("cacheDependency1"));

            // Configure the visitors
            builder.RegisterType<UrlResolvingSiteMapNodeVisitor>()
                   .As<ISiteMapNodeVisitor>();

            // Prepare for our node providers
            builder.RegisterType<FileXmlSource>()
                .Named<IXmlSource>("xmlSource1")
                .WithParameter("fileName", absoluteFileName);
                
            builder.RegisterType<SiteMapXmlReservedAttributeNameProvider>()
                .As<ISiteMapXmlReservedAttributeNameProvider>()
                .WithParameter("attributesToIgnore", new string[0]);


            // Register the sitemap node providers
            builder.RegisterType<XmlSiteMapNodeProvider>()
                .Named<ISiteMapNodeProvider>("xmlSiteMapNodeProvider1")
                .WithParameter("includeRootNode", true)
                .WithParameter("useNestedDynamicNodeRecursion", false)
                .WithParameter(
                    (p, c) => p.Name == "xmlSource",
                    (p, c) => c.ResolveNamed<IXmlSource>("xmlSource1"));

            builder.RegisterType<ReflectionSiteMapNodeProvider>()
                .Named<ISiteMapNodeProvider>("reflectionSiteMapNodeProvider1")
                .WithParameter("includeAssemblies", includeAssembliesForScan)
                .WithParameter("excludeAssemblies", new string[0]);

            builder.RegisterType<CompositeSiteMapNodeProvider>()
                .Named<ISiteMapNodeProvider>("siteMapNodeProvider1")
                .WithParameter(
                    (p, c) => p.Name == "siteMapNodeProviders",
                    (p, c) => new[]
                        {
                            c.ResolveNamed<ISiteMapNodeProvider>("xmlSiteMapNodeProvider1"),
                            c.ResolveNamed<ISiteMapNodeProvider>("reflectionSiteMapNodeProvider1")
                        });

            // Register the sitemap builders
            builder.RegisterType<SiteMapBuilder>()
                .Named<ISiteMapBuilder>("siteMapBuilder1")
                .WithParameter(
                    (p, c) => p.Name == "siteMapNodeProvider",
                    (p, c) => c.ResolveNamed<ISiteMapNodeProvider>("siteMapNodeProvider1"));

            // Configure the builder sets
            builder.RegisterType<SiteMapBuilderSet>()
                   .Named<ISiteMapBuilderSet>("builderSet1")
                   .WithParameter("instanceName", "default")
                   .WithParameter("securityTrimmingEnabled", securityTrimmingEnabled)
                   .WithParameter("enableLocalization", enableLocalization)
                   .WithParameter(
                        (p, c) => p.Name == "siteMapBuilder",
                        (p, c) => c.ResolveNamed<ISiteMapBuilder>("siteMapBuilder1"))
                   .WithParameter(
                        (p, c) => p.Name == "cacheDetails",
                        (p, c) => c.ResolveNamed<ICacheDetails>("cacheDetails1"));

            builder.RegisterType<SiteMapBuilderSetStrategy>()
                .As<ISiteMapBuilderSetStrategy>()
                .WithParameter(
                    (p, c) => p.Name == "siteMapBuilderSets",
                    (p, c) => c.ResolveNamed<IEnumerable<ISiteMapBuilderSet>>("builderSet1"));
        }
    }
}

