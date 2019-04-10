using System;
using PowerDns.Client.Models;

namespace PowerDns.Client.Exceptions
{
    public class RecordSetNotFoundException : Exception
    {
        public RecordSetNotFoundException(CanonicalName recordSetName)
            : base($"{nameof(RecordSet)} {recordSetName} not found")
        {}
    }
}
