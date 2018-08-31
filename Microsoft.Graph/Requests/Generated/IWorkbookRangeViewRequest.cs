// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\IEntityRequest.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The interface IWorkbookRangeViewRequest.
    /// </summary>
    public partial interface IWorkbookRangeViewRequest : IBaseRequest
    {
        /// <summary>
        /// Creates the specified WorkbookRangeView using PUT.
        /// </summary>
        /// <param name="workbookRangeViewToCreate">The WorkbookRangeView to create.</param>
        /// <returns>The created WorkbookRangeView.</returns>
        System.Threading.Tasks.Task<WorkbookRangeView> CreateAsync(WorkbookRangeView workbookRangeViewToCreate);        /// <summary>
        /// Creates the specified WorkbookRangeView using PUT.
        /// </summary>
        /// <param name="workbookRangeViewToCreate">The WorkbookRangeView to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created WorkbookRangeView.</returns>
        System.Threading.Tasks.Task<WorkbookRangeView> CreateAsync(WorkbookRangeView workbookRangeViewToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified WorkbookRangeView.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified WorkbookRangeView.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified WorkbookRangeView.
        /// </summary>
        /// <returns>The WorkbookRangeView.</returns>
        System.Threading.Tasks.Task<WorkbookRangeView> GetAsync();

        /// <summary>
        /// Gets the specified WorkbookRangeView.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The WorkbookRangeView.</returns>
        System.Threading.Tasks.Task<WorkbookRangeView> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified WorkbookRangeView using PATCH.
        /// </summary>
        /// <param name="workbookRangeViewToUpdate">The WorkbookRangeView to update.</param>
        /// <returns>The updated WorkbookRangeView.</returns>
        System.Threading.Tasks.Task<WorkbookRangeView> UpdateAsync(WorkbookRangeView workbookRangeViewToUpdate);

        /// <summary>
        /// Updates the specified WorkbookRangeView using PATCH.
        /// </summary>
        /// <param name="workbookRangeViewToUpdate">The WorkbookRangeView to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The updated WorkbookRangeView.</returns>
        System.Threading.Tasks.Task<WorkbookRangeView> UpdateAsync(WorkbookRangeView workbookRangeViewToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IWorkbookRangeViewRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IWorkbookRangeViewRequest Expand(Expression<Func<WorkbookRangeView, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IWorkbookRangeViewRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IWorkbookRangeViewRequest Select(Expression<Func<WorkbookRangeView, object>> selectExpression);

    }
}
