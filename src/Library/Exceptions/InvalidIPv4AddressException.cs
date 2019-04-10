using System;
using System.Net;

namespace PowerDns.Client.Exceptions
{
    public class InvalidIPv4AddressException : Exception
    {
        public InvalidIPv4AddressException(IPAddress ipAddress)
            : base($"{ipAddress} is not a valid IPv4 address.")
        {}
    }
}
