// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\IEntityCollectionWithReferencesRequestBuilder.cs.tt
namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The interface IDetectedAppManagedDevicesCollectionWithReferencesRequestBuilder.
    /// </summary>
    public partial interface IDetectedAppManagedDevicesCollectionWithReferencesRequestBuilder
    {
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        IDetectedAppManagedDevicesCollectionWithReferencesRequest Request();

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        IDetectedAppManagedDevicesCollectionWithReferencesRequest Request(IEnumerable<Option> options);

        /// <summary>
        /// Gets an <see cref="IManagedDeviceWithReferenceRequestBuilder"/> for the specified ManagedDevice.
        /// </summary>
        /// <param name="id">The ID for the ManagedDevice.</param>
        /// <returns>The <see cref="IManagedDeviceWithReferenceRequestBuilder"/>.</returns>
        IManagedDeviceWithReferenceRequestBuilder this[string id] { get; }
        
        /// <summary>
        /// Gets an <see cref="IDetectedAppManagedDevicesCollectionReferencesRequestBuilder"/> for the references in the collection.
        /// </summary>
        /// <returns>The <see cref="IDetectedAppManagedDevicesCollectionReferencesRequestBuilder"/>.</returns>
        IDetectedAppManagedDevicesCollectionReferencesRequestBuilder References { get; }

    }
}
