using System;
using System.Linq;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    public class AAAARecordSet : RecordSet
    {
        public AAAARecordSet(CanonicalName name, params AAAARecord[] records)
            : this(name, TimeSpan.FromHours(1), records)
        {
        }

        public AAAARecordSet(CanonicalName name, TimeSpan ttl, params AAAARecord[] records)
            : base(name, ttl, records.OfType<Record>().ToArray())
            => Type = RecordType.AAAA;
    }
}