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
    /// The type WorkbookFunctionsRandBetweenRequestBody.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class WorkbookFunctionsRandBetweenRequestBody
    {
    
        /// <summary>
        /// Gets or sets Bottom.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "bottom", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken Bottom { get; set; }
    
        /// <summary>
        /// Gets or sets Top.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "top", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken Top { get; set; }
    
    }
}
