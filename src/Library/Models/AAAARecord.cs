using System.Net;
using System.Net.Sockets;
using PowerDns.Client.Exceptions;

namespace PowerDns.Client.Models
{
    /// <summary>
    /// Represents an AAAA record which maps a domain name to a IPv6 address.
    /// </summary>
    public class AAAARecord : Record
    {
        /// <summary>
        /// Constructs a new AAAA record.
        /// </summary>
        /// <exception cref="InvalidIPv4AddressException"></exception>
        public AAAARecord(IPAddress ipAddress)
        {
            VerifyIPv6Address(ipAddress);
            Content = ipAddress.ToString();
        }

        private static void VerifyIPv6Address(IPAddress ipAddress)
        {
            if (ipAddress.AddressFamily != AddressFamily.InterNetworkV6)
                throw new InvalidIPv6AddressException(ipAddress);
        }
    }
}
