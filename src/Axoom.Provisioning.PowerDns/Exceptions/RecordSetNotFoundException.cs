using System;
using Axoom.Provisioning.PowerDns.DataTransferObjects;

namespace Axoom.Provisioning.PowerDns.Exceptions
{
    public class RecordSetNotFoundException : Exception
    {
        public RecordSetNotFoundException(CanonicalName zoneName, CanonicalName recordSetName)
            : base($"{nameof(RecordSet)} {recordSetName} not found in zone {zoneName}")
        {
        }
    }
}