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
    /// The interface IThumbnailRequest.
    /// </summary>
    public partial interface IThumbnailRequest : IBaseRequest
    {
        /// <summary>
        /// Creates the specified Thumbnail using PUT.
        /// </summary>
        /// <param name="thumbnailToCreate">The Thumbnail to create.</param>
        /// <returns>The created Thumbnail.</returns>
        System.Threading.Tasks.Task<Thumbnail> CreateAsync(Thumbnail thumbnailToCreate);        /// <summary>
        /// Creates the specified Thumbnail using PUT.
        /// </summary>
        /// <param name="thumbnailToCreate">The Thumbnail to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created Thumbnail.</returns>
        System.Threading.Tasks.Task<Thumbnail> CreateAsync(Thumbnail thumbnailToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified Thumbnail.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified Thumbnail.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified Thumbnail.
        /// </summary>
        /// <returns>The Thumbnail.</returns>
        System.Threading.Tasks.Task<Thumbnail> GetAsync();

        /// <summary>
        /// Gets the specified Thumbnail.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The Thumbnail.</returns>
        System.Threading.Tasks.Task<Thumbnail> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified Thumbnail using PATCH.
        /// </summary>
        /// <param name="thumbnailToUpdate">The Thumbnail to update.</param>
        /// <returns>The updated Thumbnail.</returns>
        System.Threading.Tasks.Task<Thumbnail> UpdateAsync(Thumbnail thumbnailToUpdate);

        /// <summary>
        /// Updates the specified Thumbnail using PATCH.
        /// </summary>
        /// <param name="thumbnailToUpdate">The Thumbnail to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The updated Thumbnail.</returns>
        System.Threading.Tasks.Task<Thumbnail> UpdateAsync(Thumbnail thumbnailToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IThumbnailRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IThumbnailRequest Expand(Expression<Func<Thumbnail, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IThumbnailRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IThumbnailRequest Select(Expression<Func<Thumbnail, object>> selectExpression);

    }
}
