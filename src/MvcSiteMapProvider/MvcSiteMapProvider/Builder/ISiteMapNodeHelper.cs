﻿using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml;

namespace MvcSiteMapProvider.Builder
{
    /// <summary>
    /// Contract for a set of services for creating SiteMap nodes, including dynamic nodes.
    /// </summary>
    public interface ISiteMapNodeHelper
    {
        /// <summary>
        /// Generates a key based on the properties that are passed. The combination of values should be unique.
        /// </summary>
        /// <param name="parentKey">The key of the intended parent node.</param>
        /// <param name="key">The key (if any) of the current node.</param>
        /// <param name="url">The URL of the current node.</param>
        /// <param name="title">The title of the current node.</param>
        /// <param name="area">The area (if any) name of the current node.</param>
        /// <param name="controller">The controller name of the current node.</param>
        /// <param name="action">The action method name of the current node.</param>
        /// <param name="httpMethod">The HTTP method of the current node.</param>
        /// <param name="clickable">Whether or not the node should be clickable on the UI.</param>
        /// <returns>A key that applies to the current node.</returns>
        string CreateNodeKey(string parentKey, string key, string url, string title, string area, string controller, string action, string httpMethod, bool clickable);

        /// <summary>
        /// Creates an instance of <see cref="T:MvcSiteMapProvider.Builder.ISiteMapNodeToParentRelation"/>.
        /// </summary>
        /// <param name="key">The key for the node. This cannot be changed.</param>
        /// <param name="parentKey">The key of the intended parent node.</param>
        /// <param name="node">The node to map to the parentKey.</param>
        /// <param name="sourceName">A string description of the provider. This value is shown in exception messages related to the node configuration.</param>
        /// <returns>An <see cref="T:MvcSiteMapProvider.Builder.ISiteMapNodeToParentRelation"/> instance.</returns>
        ISiteMapNodeToParentRelation CreateNode(string key, string parentKey, string sourceName);

        /// <summary>
        /// Creates an instance of <see cref="T:MvcSiteMapProvider.Builder.ISiteMapNodeToParentRelation"/>.
        /// </summary>
        /// <param name="key">The key for the node. This cannot be changed.</param>
        /// <param name="parentKey">The key of the intended parent node.</param>
        /// <param name="node">The node to map to the parentKey.</param>
        /// <param name="sourceName">A string description of the provider. This value is shown in exception messages related to the node configuration.</param>
        /// <param name="implicitResourceKey">The implicit resource key for localization of the node's properties.</param>
        /// <returns>An <see cref="T:MvcSiteMapProvider.Builder.ISiteMapNodeToParentRelation"/> instance.</returns>
        ISiteMapNodeToParentRelation CreateNode(string key, string parentKey, string sourceName, string implicitResourceKey);

        /// <summary>
        /// Processes the dynamic nodes and builds them into a list of <see cref="T:MvcSiteMapNodeProvider.Builder.ISiteMapNodeToParentRelation"/> instances
        /// and maps their parent node.
        /// </summary>
        /// <param name="node">The node that includes a configured <see cref="T:MvcSiteMapProvider.IDynamicNodeProvider"/>.</param>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerable`MvcSiteMapNodeProvider.Builder.ISiteMapNodeToParentRelation`"/> instance.</returns>
        IEnumerable<ISiteMapNodeToParentRelation> CreateDynamicNodes(ISiteMapNodeToParentRelation node);

        /// <summary>
        /// Processes the dynamic nodes and builds them into a list of <see cref="T:MvcSiteMapNodeProvider.Builder.ISiteMapNodeToParentRelation"/> instances
        /// and maps their parent node.
        /// </summary>
        /// <param name="node">The node that includes a configured <see cref="T:MvcSiteMapProvider.IDynamicNodeProvider"/>.</param>
        /// <param name="defaultParentKey">The default parent key to use if the dynamic node doesn't specify one.</param>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerable`MvcSiteMapNodeProvider.Builder.ISiteMapNodeToParentRelation`"/> instance.</returns>
        IEnumerable<ISiteMapNodeToParentRelation> CreateDynamicNodes(ISiteMapNodeToParentRelation node, string defaultParentKey);

        /// <summary>
        /// A service that can be used to check whether an attribute name is reserved and whether it should be
        /// added to the RouteValues and/or Attributes dictionaries.
        /// </summary>
        ISiteMapXmlReservedAttributeNameProvider ReservedAttributeNames { get; }


        /// <summary>
        /// The SiteMapCacheKey for the current SiteMap instance.
        /// </summary>
        string SiteMapCacheKey { get; }
    }
}
