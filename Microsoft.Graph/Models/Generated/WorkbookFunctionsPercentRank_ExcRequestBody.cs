// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\MethodRequestBody.cs.tt

namespace Microsoft.Graph
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// The type WorkbookFunctionsPercentRank_ExcRequestBody.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class WorkbookFunctionsPercentRank_ExcRequestBody
    {
    
        /// <summary>
        /// Gets or sets Array.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "array", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken Array { get; set; }
    
        /// <summary>
        /// Gets or sets X.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "x", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken X { get; set; }
    
        /// <summary>
        /// Gets or sets Significance.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "significance", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken Significance { get; set; }
    
    }
}
