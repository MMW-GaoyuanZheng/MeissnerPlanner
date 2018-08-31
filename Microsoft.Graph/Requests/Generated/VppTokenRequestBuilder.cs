// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\EntityRequestBuilder.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The type VppTokenRequestBuilder.
    /// </summary>
    public partial class VppTokenRequestBuilder : EntityRequestBuilder, IVppTokenRequestBuilder
    {

        /// <summary>
        /// Constructs a new VppTokenRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public VppTokenRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public new IVppTokenRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public new IVppTokenRequest Request(IEnumerable<Option> options)
        {
            return new VppTokenRequest(this.RequestUrl, this.Client, options);
        }
    
        /// <summary>
        /// Gets the request builder for VppTokenSyncLicenses.
        /// </summary>
        /// <returns>The <see cref="IVppTokenSyncLicensesRequestBuilder"/>.</returns>
        public IVppTokenSyncLicensesRequestBuilder SyncLicenses()
        {
            return new VppTokenSyncLicensesRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph.syncLicenses"),
                this.Client);
        }
    
    }
}
