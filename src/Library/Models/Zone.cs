using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PowerDns.Client.Models
{
    /// <summary>
    /// Represents an authoritative DNS Zone.
    /// </summary>
    public class Zone : IEquatable<Zone>
    {
        public Zone()
        {}

        public Zone(CanonicalName name, params CanonicalName[] nameservers)
            : this(name, (IEnumerable<CanonicalName>)nameservers)
        {}

        public Zone(CanonicalName name, IEnumerable<CanonicalName> nameservers)
        {
            Name = name;
            Nameservers = new List<CanonicalName>(nameservers);
        }

        /// <summary>
        /// Opaque zone id (string), assigned by the server, should not be interpreted by the application. Guaranteed to be safe for embedding in URLs.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the zone (e.g. “example.com.”).
        /// </summary>
        [Key]
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

        public bool Equals(Zone other)
        {
            if (other == null) return false;
            return Id == other.Id
                && Name == other.Name
                && Type == other.Type
                && Url == other.Url
                && Serial == other.Serial
                && Kind == other.Kind;
        }

        public override bool Equals(object obj) => obj is Zone other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Serial.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Kind;
                return hashCode;
            }
        }
    }
}
