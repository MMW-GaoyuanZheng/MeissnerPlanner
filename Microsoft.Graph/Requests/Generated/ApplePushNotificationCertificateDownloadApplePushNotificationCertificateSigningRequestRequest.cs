// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\MethodRequest.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;

    /// <summary>
    /// The type ApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest.
    /// </summary>
    public partial class ApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest : BaseRequest, IApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest
    {
        /// <summary>
        /// Constructs a new ApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest.
        /// </summary>
        public ApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest(
            string requestUrl,
            IBaseClient client,
            IEnumerable<Option> options)
            : base(requestUrl, client, options)
        {
        }

        /// <summary>
        /// Issues the GET request.
        /// </summary>
        public System.Threading.Tasks.Task<string> GetAsync()
        {
            return this.GetAsync(CancellationToken.None);
        }

        /// <summary>
        /// Issues the GET request.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await for async call.</returns>
        public System.Threading.Tasks.Task<string> GetAsync(
            CancellationToken cancellationToken)
        {
            this.Method = "GET";
            return this.SendAsync<string>(null, cancellationToken);
        }


        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        public IApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest Expand(string value)
        {
            this.QueryOptions.Add(new QueryOption("$expand", value));
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        public IApplePushNotificationCertificateDownloadApplePushNotificationCertificateSigningRequestRequest Select(string value)
        {
            this.QueryOptions.Add(new QueryOption("$select", value));
            return this;
        }
    }
}
