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
    /// The interface IDriveItemRequest.
    /// </summary>
    public partial interface IDriveItemRequest : IBaseRequest
    {
        /// <summary>
        /// Creates the specified DriveItem using PUT.
        /// </summary>
        /// <param name="driveItemToCreate">The DriveItem to create.</param>
        /// <returns>The created DriveItem.</returns>
        System.Threading.Tasks.Task<DriveItem> CreateAsync(DriveItem driveItemToCreate);        /// <summary>
        /// Creates the specified DriveItem using PUT.
        /// </summary>
        /// <param name="driveItemToCreate">The DriveItem to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created DriveItem.</returns>
        System.Threading.Tasks.Task<DriveItem> CreateAsync(DriveItem driveItemToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified DriveItem.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified DriveItem.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified DriveItem.
        /// </summary>
        /// <returns>The DriveItem.</returns>
        System.Threading.Tasks.Task<DriveItem> GetAsync();

        /// <summary>
        /// Gets the specified DriveItem.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The DriveItem.</returns>
        System.Threading.Tasks.Task<DriveItem> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified DriveItem using PATCH.
        /// </summary>
        /// <param name="driveItemToUpdate">The DriveItem to update.</param>
        /// <returns>The updated DriveItem.</returns>
        System.Threading.Tasks.Task<DriveItem> UpdateAsync(DriveItem driveItemToUpdate);

        /// <summary>
        /// Updates the specified DriveItem using PATCH.
        /// </summary>
        /// <param name="driveItemToUpdate">The DriveItem to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The updated DriveItem.</returns>
        System.Threading.Tasks.Task<DriveItem> UpdateAsync(DriveItem driveItemToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IDriveItemRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IDriveItemRequest Expand(Expression<Func<DriveItem, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IDriveItemRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IDriveItemRequest Select(Expression<Func<DriveItem, object>> selectExpression);

    }
}
