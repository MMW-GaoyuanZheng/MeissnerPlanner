// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\EnumType.cs.tt


namespace Microsoft.Graph
{
    using Newtonsoft.Json;

    /// <summary>
    /// The enum RatingAppsType.
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    public enum RatingAppsType
    {
    
        /// <summary>
        /// all Allowed
        /// </summary>
        AllAllowed = 0,
	
        /// <summary>
        /// all Blocked
        /// </summary>
        AllBlocked = 1,
	
        /// <summary>
        /// ages Above4
        /// </summary>
        AgesAbove4 = 2,
	
        /// <summary>
        /// ages Above9
        /// </summary>
        AgesAbove9 = 3,
	
        /// <summary>
        /// ages Above12
        /// </summary>
        AgesAbove12 = 4,
	
        /// <summary>
        /// ages Above17
        /// </summary>
        AgesAbove17 = 5,
	
    }
}