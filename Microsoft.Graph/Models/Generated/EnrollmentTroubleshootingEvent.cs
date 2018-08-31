// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\EntityType.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// The type Enrollment Troubleshooting Event.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class EnrollmentTroubleshootingEvent : DeviceManagementTroubleshootingEvent
    {
    
        /// <summary>
        /// Gets or sets managed device identifier.
        /// Device identifier created or collected by Intune.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "managedDeviceIdentifier", Required = Newtonsoft.Json.Required.Default)]
        public string ManagedDeviceIdentifier { get; set; }
    
        /// <summary>
        /// Gets or sets operating system.
        /// Operating System.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "operatingSystem", Required = Newtonsoft.Json.Required.Default)]
        public string OperatingSystem { get; set; }
    
        /// <summary>
        /// Gets or sets os version.
        /// OS Version.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "osVersion", Required = Newtonsoft.Json.Required.Default)]
        public string OsVersion { get; set; }
    
        /// <summary>
        /// Gets or sets user id.
        /// Identifier for the user that tried to enroll the device.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userId", Required = Newtonsoft.Json.Required.Default)]
        public string UserId { get; set; }
    
        /// <summary>
        /// Gets or sets device id.
        /// Azure AD device identifier.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "deviceId", Required = Newtonsoft.Json.Required.Default)]
        public string DeviceId { get; set; }
    
        /// <summary>
        /// Gets or sets enrollment type.
        /// Type of the enrollment. Possible values are: unknown, userEnrollment, deviceEnrollmentManager, appleBulkWithUser, appleBulkWithoutUser, windowsAzureADJoin, windowsBulkUserless, windowsAutoEnrollment, windowsBulkAzureDomainJoin, windowsCoManagement.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enrollmentType", Required = Newtonsoft.Json.Required.Default)]
        public DeviceEnrollmentType? EnrollmentType { get; set; }
    
        /// <summary>
        /// Gets or sets failure category.
        /// Highlevel failure category. Possible values are: unknown, authentication, authorization, accountValidation, userValidation, deviceNotSupported, inMaintenance, badRequest, featureNotSupported, enrollmentRestrictionsEnforced, clientDisconnected.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "failureCategory", Required = Newtonsoft.Json.Required.Default)]
        public DeviceEnrollmentFailureReason? FailureCategory { get; set; }
    
        /// <summary>
        /// Gets or sets failure reason.
        /// Detailed failure reason.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "failureReason", Required = Newtonsoft.Json.Required.Default)]
        public string FailureReason { get; set; }
    
    }
}

