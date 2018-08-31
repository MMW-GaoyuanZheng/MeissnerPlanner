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
    /// The type Location.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [JsonConverter(typeof(DerivedTypeConverter))]
    public partial class Location
    {
    
        /// <summary>
        /// Gets or sets displayName.
        /// The name associated with the location.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "displayName", Required = Newtonsoft.Json.Required.Default)]
        public string DisplayName { get; set; }
    
        /// <summary>
        /// Gets or sets locationEmailAddress.
        /// Optional email address of the location.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "locationEmailAddress", Required = Newtonsoft.Json.Required.Default)]
        public string LocationEmailAddress { get; set; }
    
        /// <summary>
        /// Gets or sets address.
        /// The street address of the location.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "address", Required = Newtonsoft.Json.Required.Default)]
        public PhysicalAddress Address { get; set; }
    
        /// <summary>
        /// Gets or sets coordinates.
        /// The geographic coordinates and elevation of the location.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "coordinates", Required = Newtonsoft.Json.Required.Default)]
        public OutlookGeoCoordinates Coordinates { get; set; }
    
        /// <summary>
        /// Gets or sets locationUri.
        /// Optional URI representing the location.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "locationUri", Required = Newtonsoft.Json.Required.Default)]
        public string LocationUri { get; set; }
    
        /// <summary>
        /// Gets or sets locationType.
        /// The type of location. Possible values are: default, conferenceRoom, homeAddress, businessAddress,geoCoordinates, streetAddress, hotel, restaurant, localBusiness, postalAddress. Read-only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "locationType", Required = Newtonsoft.Json.Required.Default)]
        public LocationType? LocationType { get; set; }
    
        /// <summary>
        /// Gets or sets uniqueId.
        /// For internal use only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "uniqueId", Required = Newtonsoft.Json.Required.Default)]
        public string UniqueId { get; set; }
    
        /// <summary>
        /// Gets or sets uniqueIdType.
        /// For internal use only.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "uniqueIdType", Required = Newtonsoft.Json.Required.Default)]
        public LocationUniqueIdType? UniqueIdType { get; set; }
    
        /// <summary>
        /// Gets or sets additional data.
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public IDictionary<string, object> AdditionalData { get; set; }
    
    }
}
