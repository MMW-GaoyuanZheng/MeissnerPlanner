// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\EnumType.cs.tt


namespace Microsoft.Graph
{
    using Newtonsoft.Json;

    /// <summary>
    /// The enum InstallState.
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    public enum InstallState
    {
    
        /// <summary>
        /// not Applicable
        /// </summary>
        NotApplicable = 0,
	
        /// <summary>
        /// installed
        /// </summary>
        Installed = 1,
	
        /// <summary>
        /// failed
        /// </summary>
        Failed = 2,
	
        /// <summary>
        /// not Installed
        /// </summary>
        NotInstalled = 3,
	
        /// <summary>
        /// uninstall Failed
        /// </summary>
        UninstallFailed = 4,
	
        /// <summary>
        /// unknown
        /// </summary>
        Unknown = 5,
	
    }
}
