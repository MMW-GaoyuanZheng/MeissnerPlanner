// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\IEntityCollectionPage.cs.tt

namespace Microsoft.Graph
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// The interface ICalendarSingleValueExtendedPropertiesCollectionPage.
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<CalendarSingleValueExtendedPropertiesCollectionPage>))]
    public interface ICalendarSingleValueExtendedPropertiesCollectionPage : ICollectionPage<SingleValueLegacyExtendedProperty>
    {
        /// <summary>
        /// Gets the next page <see cref="ICalendarSingleValueExtendedPropertiesCollectionRequest"/> instance.
        /// </summary>
        ICalendarSingleValueExtendedPropertiesCollectionRequest NextPageRequest { get; }

        /// <summary>
        /// Initializes the NextPageRequest property.
        /// </summary>
        void InitializeNextPageRequest(IBaseClient client, string nextPageLinkString);
    }
}
