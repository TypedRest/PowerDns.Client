using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Axoom.Provisioning.PowerDns
{
    /// <summary>
    /// Connection configuration for <see cref="IPowerDns"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public class PowerDnsOptions
    {
        /// <summary>
        /// The top-level URI of the PowerDNS instance, not including the API version.
        /// </summary>
        public Uri Uri { get; set; }
 
        /// <summary>
        /// The API key used for authentication.
        /// </summary>
        public string ApiKey { get; set; }
    }
}