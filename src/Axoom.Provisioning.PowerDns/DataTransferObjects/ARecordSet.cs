using System;
using System.Linq;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    public class ARecordSet : RecordSet
    {
        public ARecordSet(CanonicalName name, params ARecord[] records)
            : this(name, TimeSpan.FromHours(1), records)
        {
        }
        
        public ARecordSet(CanonicalName name, TimeSpan ttl, params ARecord[] records)
            : base(name, ttl, records.OfType<Record>().ToArray())
            => Type = RecordType.A;
    }
}