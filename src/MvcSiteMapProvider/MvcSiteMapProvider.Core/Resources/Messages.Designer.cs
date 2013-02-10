﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcSiteMapProvider.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MvcSiteMapProvider.Core.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current ACL module does not provide functionality for regular SiteMapNode objects..
        /// </summary>
        internal static string AclModuleDoesNotSupportRegularSiteMapNodes {
            get {
                return ResourceManager.GetString("AclModuleDoesNotSupportRegularSiteMapNodes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ambiguous controller. Found multiple controller types for {0}Controller. Consider narrowing the places to search by adding you controller namespaces to ControllerBuilder.Current.DefaultNamespaces..
        /// </summary>
        internal static string AmbiguousControllerFoundMultipleControllers {
            get {
                return ResourceManager.GetString("AmbiguousControllerFoundMultipleControllers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot enumerate a threadsafe dictionary. Instead, enumerate the keys or values collection..
        /// </summary>
        internal static string CannotEnumerateThreadSafeDictionary {
            get {
                return ResourceManager.GetString("CannotEnumerateThreadSafeDictionary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot use a leading .. to exit above the top directory..
        /// </summary>
        internal static string CannotExitUpTopDirectory {
            get {
                return ResourceManager.GetString("CannotExitUpTopDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Collection is read-only..
        /// </summary>
        internal static string CollectionReadOnly {
            get {
                return ResourceManager.GetString("CollectionReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not resolve URL for sitemap node {0} which represents action {1} in controller {2}. Ensure that the route {3} for this sitemap node can be resolved and that its default values allow resolving the URL for the current sitemap node..
        /// </summary>
        internal static string CouldNotResolve {
            get {
                return ResourceManager.GetString("CouldNotResolve", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection..
        /// </summary>
        internal static string InvalidOffsetLength {
            get {
                return ResourceManager.GetString("InvalidOffsetLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An invalid element was found in the sitemap..
        /// </summary>
        internal static string InvalidSiteMapElement {
            get {
                return ResourceManager.GetString("InvalidSiteMapElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not a valid virtual path..
        /// </summary>
        internal static string InvalidVirtualPath {
            get {
                return ResourceManager.GetString("InvalidVirtualPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple nodes with the same key &apos;{0}&apos; were found. XmlSiteMapProvider requires that sitemap nodes have unique keys..
        /// </summary>
        internal static string MultipleNodesWithIdenticalKey {
            get {
                return ResourceManager.GetString("MultipleNodesWithIdenticalKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple nodes with the same URL &apos;{0}&apos; were found. XmlSiteMapProvider requires that sitemap nodes have unique URLs..
        /// </summary>
        internal static string MultipleNodesWithIdenticalUrl {
            get {
                return ResourceManager.GetString("MultipleNodesWithIdenticalUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Index is less than zero..
        /// </summary>
        internal static string NeedNonNegativeNumber {
            get {
                return ResourceManager.GetString("NeedNonNegativeNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple root nodes defined. Are you missing a ParentKey definition on the {0} controller, {1}?.
        /// </summary>
        internal static string NoParentKeyDefined {
            get {
                return ResourceManager.GetString("NoParentKeyDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No ISiteMapNodeUrlResolver is provided for the current node..
        /// </summary>
        internal static string NoUrlResolverProvided {
            get {
                return ResourceManager.GetString("NoUrlResolverProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is a physical path, but a virtual path was expected..
        /// </summary>
        internal static string PhysicalPathNotAllowed {
            get {
                return ResourceManager.GetString("PhysicalPathNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Relative URL is not allowed..
        /// </summary>
        internal static string RelativeUrlNotAllowed {
            get {
                return ResourceManager.GetString("RelativeUrlNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource object with classname &apos;{0}&apos; and key &apos;{1}&apos; was not found..
        /// </summary>
        internal static string ResourceNotFoundWithClassAndKey {
            get {
                return ResourceManager.GetString("ResourceNotFoundWithClassAndKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Security trimming cannot be disabled again after it is enabled..
        /// </summary>
        internal static string SecurityTrimmingCannotBeDisabled {
            get {
                return ResourceManager.GetString("SecurityTrimmingCannotBeDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Site map file {0} could not be found. Verify that the path provided is correct..
        /// </summary>
        internal static string SiteMapFileNotFound {
            get {
                return ResourceManager.GetString("SiteMapFileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Root node defined in SiteMap is null, root node cannot be null..
        /// </summary>
        internal static string SiteMapInvalidRootNode {
            get {
                return ResourceManager.GetString("SiteMapInvalidRootNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The sitemap loader may only be set in the App_Initialize event of Global.asax and must not be set again..
        /// </summary>
        internal static string SiteMapLoaderAlreadySet {
            get {
                return ResourceManager.GetString("SiteMapLoaderAlreadySet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid value of type {0} passed in, value must be of type SiteMapNode..
        /// </summary>
        internal static string SiteMapNodeCollectionInvalidType {
            get {
                return ResourceManager.GetString("SiteMapNodeCollectionInvalidType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no default builder set configured..
        /// </summary>
        internal static string SiteMapNoDefaultBuilderSetConfigured {
            get {
                return ResourceManager.GetString("SiteMapNoDefaultBuilderSetConfigured", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SiteMapNode is readonly, property {0} cannot be modified..
        /// </summary>
        internal static string SiteMapNodeReadOnly {
            get {
                return ResourceManager.GetString("SiteMapNodeReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SiteMap is readonly, nodes cannot be modified..
        /// </summary>
        internal static string SiteMapReadOnly {
            get {
                return ResourceManager.GetString("SiteMapReadOnly", resourceCulture);
            }
        }
    }
}
