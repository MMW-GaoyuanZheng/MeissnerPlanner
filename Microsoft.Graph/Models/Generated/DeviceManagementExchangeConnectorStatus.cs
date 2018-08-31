// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\EnumType.cs.tt


namespace Microsoft.Graph
{
    using Newtonsoft.Json;

    /// <summary>
    /// The enum DeviceManagementExchangeConnectorStatus.
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    public enum DeviceManagementExchangeConnectorStatus
    {
    
        /// <summary>
        /// none
        /// </summary>
        None = 0,
	
        /// <summary>
        /// connection Pending
        /// </summary>
        ConnectionPending = 1,
	
        /// <summary>
        /// connected
        /// </summary>
        Connected = 2,
	
        /// <summary>
        /// disconnected
        /// </summary>
        Disconnected = 3,
	
    }
}
