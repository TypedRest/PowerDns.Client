using JetBrains.Annotations;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    [PublicAPI]
    public enum ZoneKind
    {
        Native,
        Master,
        Slave
    }
}