﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSiteMapProvider
{
    /// <summary>
    /// DynamicNode class
    /// </summary>
    public class DynamicNode
    {
        protected ChangeFrequency changeFrequency = ChangeFrequency.Undefined;
        protected UpdatePriority updatePriority = UpdatePriority.Undefined;

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        public virtual string Route { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets or sets the parent key (optional).
        /// </summary>
        /// <value>The parent key.</value>
        public virtual string ParentKey { get; set; }

        /// <summary>
        /// Gets or sets the Url (optional).
        /// </summary>
        /// <value>The area.</value>
        public virtual string Url { get; set; }

        /// <summary>
        /// Gets or sets the area (optional).
        /// </summary>
        /// <value>The area.</value>
        public virtual string Area { get; set; }

        /// <summary>
        /// Gets or sets the controller (optional).
        /// </summary>
        /// <value>The controller.</value>
        public virtual string Controller { get; set; }

        /// <summary>
        /// Gets or sets the action (optional).
        /// </summary>
        /// <value>The action.</value>
        public virtual string Action { get; set; }

        /// <summary>
        /// Gets or sets the title (optional).
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the description (optional).
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the target frame (optional).
        /// </summary>
        /// <value>The target frame.</value>
        public virtual string TargetFrame { get; set; }

        /// <summary>
        /// Gets or sets the image URL (optional).
        /// </summary>
        /// <value>The image URL.</value>
        public virtual string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the route values.
        /// </summary>
        /// <value>The route values.</value>
        public virtual IDictionary<string, object> RouteValues { get; set; }

        /// <summary>
        /// Gets or sets the attributes (optional).
        /// </summary>
        /// <value>The attributes.</value>
        public virtual IDictionary<string, object> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the preserved route parameter names (= values that will be used from the current request route).
        /// </summary>
        /// <value>The attributes.</value>
        public virtual IList<string> PreservedRouteParameters { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public virtual IList<string> Roles { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        public virtual DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the change frequency.
        /// </summary>
        /// <value>The change frequency.</value>
        public virtual ChangeFrequency ChangeFrequency 
        { 
            get { return changeFrequency; } 
            set { changeFrequency = value; } 
        }

        /// <summary>
        /// Gets or sets the update priority.
        /// </summary>
        /// <value>The update priority.</value>
        public virtual UpdatePriority UpdatePriority 
        {
            get { return updatePriority; }
            set { updatePriority = value; }
        }

        /// <summary>
        /// A value indicating to cache the resolved URL. If false, the URL will be 
        /// resolved every time it is accessed.
        /// </summary>
        public virtual bool? CacheResolvedUrl { get; set; }


        /// <summary>
        /// Gets or sets the canonical URL.
        /// </summary>
        /// <remarks>May not be used in conjuntion with CanonicalKey. Only 1 canonical value is allowed.</remarks>
        public virtual string CanonicalUrl { get; set; }

        /// <summary>
        /// Gets or sets the canonical key. The key is used to reference another ISiteMapNode to get the canonical URL.
        /// </summary>
        /// <remarks>May not be used in conjuntion with CanonicalUrl. Only 1 canonical value is allowed.</remarks>
        public virtual string CanonicalKey { get; set; }

        /// <summary>
        /// Gets or sets the robots meta values.
        /// </summary>
        /// <value>The robots meta values.</value>
        public virtual IList<string> MetaRobotsValues { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the node relative to its sibling nodes (the nodes that have the same parent).
        /// </summary>
        /// <value>The sort order.</value>
        public virtual int? Order { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method (such as GET, POST, or HEAD).
        /// </summary>
        /// <value>
        /// The HTTP method.
        /// </value>
        public virtual string HttpMethod { get; set; }

        /// <summary>
        /// Gets or sets the visibility provider.
        /// </summary>
        /// <value>
        /// The visibility provider.
        /// </value>
        public virtual string VisibilityProvider { get; set; }

        /// <summary>
        /// Gets or sets the URL resolver.
        /// </summary>
        /// <value>
        /// The URL resolver.
        /// </value>
        public virtual string UrlResolver { get; set; }

        /// <summary>
        /// Gets or sets whether the node is clickable or just a grouping node.
        /// </summary>
        /// <value>
        /// Is clickable.
        /// </value>
        public virtual bool? Clickable { get; set; }

        /// <summary>
        /// Copies the values for matching properties on an <see cref="T:MvcSiteMapNodeProvider.ISiteMapNode"/> instance, but
        /// doesn't overwrite any values that are not set in this <see cref="T:MvcSiteMapNodeProvider.DynamicNode"/> instance.
        /// </summary>
        /// <param name="node">The site map node to copy the values into.</param>
        public virtual void SafeCopyTo(ISiteMapNode node)
        {
            if (!String.IsNullOrEmpty(this.Route))
                node.Route = this.Route;
            if (!String.IsNullOrEmpty(this.Url))
                node.Url = this.Url;
            if (!String.IsNullOrEmpty(this.Area))
                node.Area = this.Area;
            if (!String.IsNullOrEmpty(this.Controller))
                node.Controller = this.Controller;
            if (!String.IsNullOrEmpty(this.Action))
                node.Action = this.Action;
            if (!String.IsNullOrEmpty(this.Title))
                node.Title = this.Title;
            if (!String.IsNullOrEmpty(this.Description))
                node.Description = this.Description;
            if (!String.IsNullOrEmpty(this.TargetFrame))
                node.TargetFrame = this.TargetFrame;
            if (!String.IsNullOrEmpty(this.ImageUrl))
                node.ImageUrl = this.ImageUrl;
            foreach (var kvp in this.RouteValues)
            {
                node.RouteValues[kvp.Key] = kvp.Value;
            }
            foreach (var kvp in this.Attributes)
            {
                node.Attributes[kvp.Key] = kvp.Value;
            }
            if (this.PreservedRouteParameters.Any())
            {
                foreach (var p in this.PreservedRouteParameters)
                {
                    if (!node.PreservedRouteParameters.Contains(p))
                    {
                        node.PreservedRouteParameters.Add(p);
                    }
                }
            }
            if (this.Roles.Any())
            {
                foreach (var role in this.Roles)
                {
                    if (!node.Roles.Contains(role))
                    {
                        node.Roles.Add(role);
                    }
                }
            }
            if (this.LastModifiedDate != null && this.LastModifiedDate.HasValue)
                node.LastModifiedDate = this.LastModifiedDate.Value;
            if (this.ChangeFrequency != ChangeFrequency.Undefined)
                node.ChangeFrequency = this.ChangeFrequency;
            if (this.UpdatePriority != UpdatePriority.Undefined)
                node.UpdatePriority = this.UpdatePriority;
            if (this.CacheResolvedUrl != null)
                node.CacheResolvedUrl = (bool)this.CacheResolvedUrl;
            if (!String.IsNullOrEmpty(this.CanonicalKey))
                node.CanonicalKey = this.CanonicalKey;
            if (!String.IsNullOrEmpty(this.CanonicalUrl))
                node.CanonicalUrl = this.CanonicalUrl;
            if (this.MetaRobotsValues.Any())
            {
                foreach (var value in this.MetaRobotsValues)
                {
                    if (!node.MetaRobotsValues.Contains(value))
                    {
                        node.MetaRobotsValues.Add(value);
                    }
                }
            }
            if (this.Order != null)
                node.Order = (int)this.Order;
            if (!String.IsNullOrEmpty(this.HttpMethod))
                node.HttpMethod = this.HttpMethod;
            if (!String.IsNullOrEmpty(this.VisibilityProvider))
                node.VisibilityProvider = this.VisibilityProvider;
            if (!String.IsNullOrEmpty(this.UrlResolver))
                node.UrlResolver = this.UrlResolver;
            if (this.Clickable != null)
                node.Clickable = (bool)this.Clickable;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNode"/> class.
        /// </summary>
        public DynamicNode()
        {
            RouteValues = new Dictionary<string, object>();
            Attributes = new Dictionary<string, object>();
            PreservedRouteParameters = new List<string>();
            Roles = new List<string>();
            MetaRobotsValues = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNode"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="title">The title.</param>
        public DynamicNode(string key, string title)
            : this(key, null, title, "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNode"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parentKey">The parent key.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        public DynamicNode(string key, string parentKey, string title, string description)
            : this()
        {
            Key = key;
            ParentKey = parentKey;
            Title = title;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNode"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parentKey">The parent key.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="controller">The controller (optional).</param>
        /// <param name="action">The action (optional).</param>
        public DynamicNode(string key, string parentKey, string title, string description, string controller, string action)
            : this(key, parentKey, title, description, null, controller, action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNode"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parentKey">The parent key.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="action">The action (optional).</param>
        public DynamicNode(string key, string parentKey, string title, string description, string action)
            : this(key, parentKey, title, description, null, null, action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNode"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parentKey">The parent key.</param>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="area">The area (optional).</param>
        /// <param name="controller">The controller (optional).</param>
        /// <param name="action">The action (optional).</param>
        public DynamicNode(string key, string parentKey, string title, string description, string area, string controller, string action)
            : this(key, parentKey, title, description)
        {
            Area = area;
            Controller = controller;
            Action = action;
        }
    }
}
