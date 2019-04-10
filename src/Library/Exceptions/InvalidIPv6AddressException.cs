using System;
using System.Net;

namespace PowerDns.Client.Exceptions
{
    public class InvalidIPv6AddressException : Exception
    {
        public InvalidIPv6AddressException(IPAddress ipAddress)
            : base($"{ipAddress} is not a valid IPv6 address.")
        {}
    }
}
