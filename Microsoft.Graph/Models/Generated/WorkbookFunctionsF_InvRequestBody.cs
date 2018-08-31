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
    /// The type WorkbookFunctionsF_InvRequestBody.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class WorkbookFunctionsF_InvRequestBody
    {
    
        /// <summary>
        /// Gets or sets Probability.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "probability", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken Probability { get; set; }
    
        /// <summary>
        /// Gets or sets DegFreedom1.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "degFreedom1", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken DegFreedom1 { get; set; }
    
        /// <summary>
        /// Gets or sets DegFreedom2.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "degFreedom2", Required = Newtonsoft.Json.Required.Default)]
        public Newtonsoft.Json.Linq.JToken DegFreedom2 { get; set; }
    
    }
}
