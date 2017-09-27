using JetBrains.Annotations;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    [PublicAPI]
    public enum RecordType
    {
        // ReSharper disable InconsistentNaming
        A,
        AAAA,
        NS,
        CName,
        MX,
        SOA,
        PTR
        // ReSharper restore InconsistentNaming
    }
}