using System;
using System.Linq;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    public class CNameRecordSet : RecordSet
    {
        public CNameRecordSet(CanonicalName name, params CNameRecord[] records)
            : this(name, TimeSpan.FromHours(1), records)
        {
        }

        public CNameRecordSet(CanonicalName name, TimeSpan ttl, params CNameRecord[] records)
            : base(name, ttl, records.OfType<Record>().ToArray())
            => Type = RecordType.CName;
    }
}