using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    /// <summary>
    /// Represents an authoritative DNS Zone.
    /// </summary>
    [PublicAPI]
    public class Zone
    {
        public Zone()
        {
        }

        public Zone(CanonicalName name, params CanonicalName[] nameServers)
            : this(name, (IEnumerable<CanonicalName>) nameServers)
        {
        }

        public Zone(CanonicalName name, IEnumerable<CanonicalName> nameServers)
        {
            Name = name;
            Nameservers = new List<CanonicalName>(nameServers);
        }

        /// <summary>
        /// Opaque zone id (string), assigned by the server, should not be interpreted by the application. Guaranteed to be safe for embedding in URLs.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the zone (e.g. “example.com.”).
        /// </summary>
        public CanonicalName Name { get; set; }

        /// <summary>
        /// Is always "Zone".
        /// </summary>
        public string Type { get; } = "Zone";

        /// <summary>
        /// API endpoint for this zone.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The SOA serial number.
        /// </summary>
        public int? Serial { get; set; }

        /// <summary>
        /// Simple list of nameserver names, including the trailing dot. Not required for slave zones.
        /// </summary>
        public List<CanonicalName> Nameservers { get; set; } = new List<CanonicalName>();

        /// <summary>
        /// Zone kind.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ZoneKind Kind { get; set; } = ZoneKind.Native;

        /// <summary>
        /// Resource records in this zone.
        /// </summary>
        [JsonProperty("rrsets")]
        public List<RecordSet> RecordSets { get; set; } = new List<RecordSet>();
    }
}