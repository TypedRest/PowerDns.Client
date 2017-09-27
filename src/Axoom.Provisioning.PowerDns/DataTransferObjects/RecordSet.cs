using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    /// <summary>
    /// Represents a set records.
    /// </summary>
    [PublicAPI]
    public class RecordSet
    {
        public RecordSet()
        {
        }

        public RecordSet(CanonicalName name, TimeSpan ttl, params Record[] records)
        {
            Name = name;
            Ttl = (uint) ttl.TotalSeconds;
            Records.AddRange(records);
        }
        
        /// <summary>
        /// Name for record set (e.g. “www.example.com.”)
        /// </summary>
        public CanonicalName Name { get; set; }
        
        /// <summary>
        /// Type of this record.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public RecordType Type { get; set; } = RecordType.A;
        
        /// <summary>
        /// DNS TTL of the records, in seconds. MUST NOT be included when <see cref="ChangeType"/> is set to "Delete".
        /// </summary>
        public uint Ttl { get; set; }
        
        /// <summary>
        /// MUST be added when updating the <see cref="RecordSet"/>. 
        /// With Delete, all existing <see cref="Record"/>s matching name and type will be deleted, including all comments. 
        /// With Replace: when records is present, all existing <see cref="Record"/>s matching name and type will be deleted, 
        /// and then new records given in records will be created. 
        /// If no records are left, any existing comments will be deleted as well. 
        /// When comments is present, all existing comments for the <see cref="Record"/>s matching name and type will be deleted, 
        /// and then new comments given in comments will be created.
        /// </summary>
        public ChangeType? ChangeType { get; set; }
        
        /// <summary>
        ///  All records in this <see cref="RecordSet"/>. 
        /// When updating Records, this is the list of new records (replacing the old ones). 
        /// Must be empty when <see cref="ChangeType"/> is set to "Delete". An empty list results in deletion of all records.
        /// </summary>
        public List<Record> Records { get; set; } = new List<Record>();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((RecordSet) obj);
        }

        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;

        private bool Equals(RecordSet other) => Equals(Name, other.Name);
    }
}