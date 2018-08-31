// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\MethodRequestBuilder.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The type WorkbookFunctionsDaysRequestBuilder.
    /// </summary>
    public partial class WorkbookFunctionsDaysRequestBuilder : BaseActionMethodRequestBuilder<IWorkbookFunctionsDaysRequest>, IWorkbookFunctionsDaysRequestBuilder
    {
        /// <summary>
        /// Constructs a new <see cref="WorkbookFunctionsDaysRequestBuilder"/>.
        /// </summary>
        /// <param name="requestUrl">The URL for the request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        /// <param name="endDate">A endDate parameter for the OData method call.</param>
        /// <param name="startDate">A startDate parameter for the OData method call.</param>
        public WorkbookFunctionsDaysRequestBuilder(
            string requestUrl,
            IBaseClient client,
            Newtonsoft.Json.Linq.JToken endDate,
            Newtonsoft.Json.Linq.JToken startDate)
            : base(requestUrl, client)
        {
            this.SetParameter("endDate", endDate, true);
            this.SetParameter("startDate", startDate, true);
        }

        /// <summary>
        /// A method used by the base class to construct a request class instance.
        /// </summary>
        /// <param name="functionUrl">The request URL to </param>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>An instance of a specific request class.</returns>
        protected override IWorkbookFunctionsDaysRequest CreateRequest(string functionUrl, IEnumerable<Option> options)
        {
            var request = new WorkbookFunctionsDaysRequest(functionUrl, this.Client, options);

            if (this.HasParameter("endDate"))
            {
                request.RequestBody.EndDate = this.GetParameter<Newtonsoft.Json.Linq.JToken>("endDate");
            }

            if (this.HasParameter("startDate"))
            {
                request.RequestBody.StartDate = this.GetParameter<Newtonsoft.Json.Linq.JToken>("startDate");
            }

            return request;
        }
    }
}
