﻿using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Core.SiteMap.Builder
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IReflectionSiteMapBuilderFactory
    {
        ISiteMapBuilder Create(
            IEnumerable<String> includeAssemblies,
            IEnumerable<String> excludeAssemblies);
    }
}
