// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\EntityCollectionRequestBuilder.cs.tt
namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type AndroidManagedAppProtectionAppsCollectionRequestBuilder.
    /// </summary>
    public partial class AndroidManagedAppProtectionAppsCollectionRequestBuilder : BaseRequestBuilder, IAndroidManagedAppProtectionAppsCollectionRequestBuilder
    {
        /// <summary>
        /// Constructs a new AndroidManagedAppProtectionAppsCollectionRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public AndroidManagedAppProtectionAppsCollectionRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public IAndroidManagedAppProtectionAppsCollectionRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public IAndroidManagedAppProtectionAppsCollectionRequest Request(IEnumerable<Option> options)
        {
            return new AndroidManagedAppProtectionAppsCollectionRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="IManagedMobileAppRequestBuilder"/> for the specified AndroidManagedAppProtectionManagedMobileApp.
        /// </summary>
        /// <param name="id">The ID for the AndroidManagedAppProtectionManagedMobileApp.</param>
        /// <returns>The <see cref="IManagedMobileAppRequestBuilder"/>.</returns>
        public IManagedMobileAppRequestBuilder this[string id]
        {
            get
            {
                return new ManagedMobileAppRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        
    }
}
