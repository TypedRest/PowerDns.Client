using System;
using System.Net;

namespace Axoom.Provisioning.PowerDns.Exceptions
{
    public class InvalidIPv6AddressException : Exception
    {
        public InvalidIPv6AddressException(IPAddress ipAddress)
            : base($"{ipAddress} is not a valid IPv6 address.")
        {
        }
    }
}