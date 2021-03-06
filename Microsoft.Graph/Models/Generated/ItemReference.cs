// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\ComplexType.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// The type ItemReference.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [JsonConverter(typeof(DerivedTypeConverter))]
    public partial class ItemReference
    {
    
        /// <summary>
        /// Gets or sets driveId.
        /// Unique identifier of the drive instance that contains the item. Read-only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "driveId", Required = Newtonsoft.Json.Required.Default)]
        public string DriveId { get; set; }
    
        /// <summary>
        /// Gets or sets driveType.
        /// Identifies the type of drive. See [drive][] resource for values.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "driveType", Required = Newtonsoft.Json.Required.Default)]
        public string DriveType { get; set; }
    
        /// <summary>
        /// Gets or sets id.
        /// Unique identifier of the item in the drive. Read-only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Newtonsoft.Json.Required.Default)]
        public string Id { get; set; }
    
        /// <summary>
        /// Gets or sets name.
        /// The name of the item being referenced. Read-only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Newtonsoft.Json.Required.Default)]
        public string Name { get; set; }
    
        /// <summary>
        /// Gets or sets path.
        /// Path that can be used to navigate to the item. Read-only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "path", Required = Newtonsoft.Json.Required.Default)]
        public string Path { get; set; }
    
        /// <summary>
        /// Gets or sets shareId.
        /// A unique identifier for a shared resource that can be accessed via the [Shares][] API.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "shareId", Required = Newtonsoft.Json.Required.Default)]
        public string ShareId { get; set; }
    
        /// <summary>
        /// Gets or sets sharepointIds.
        /// Returns identifiers useful for SharePoint REST compatibility. Read-only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sharepointIds", Required = Newtonsoft.Json.Required.Default)]
        public SharepointIds SharepointIds { get; set; }
    
        /// <summary>
        /// Gets or sets additional data.
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public IDictionary<string, object> AdditionalData { get; set; }
    
    }
}
