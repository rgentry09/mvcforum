﻿using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Collections;

namespace MvcSiteMapProvider
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISiteMapNodeChildStateFactory
    {
        ILocalizationService CreateLocalizationService(string implicitResourceKey);
        IAttributeCollection CreateAttributeCollection(ISiteMap siteMap, ILocalizationService localizationService);
        IRouteValueCollection CreateRouteValueCollection(ISiteMap siteMap);
        IPreservedRouteParameterCollection CreatePreservedRouteParameterCollection(ISiteMap siteMap);
        IRoleCollection CreateRoleCollection(ISiteMap siteMap);
    }
}
