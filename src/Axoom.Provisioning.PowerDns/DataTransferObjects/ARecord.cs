using System.Net;
using System.Net.Sockets;
using Axoom.Provisioning.PowerDns.Exceptions;

namespace Axoom.Provisioning.PowerDns.DataTransferObjects
{
    /// <summary>
    /// Represents an A record which maps a domain name to a IPv4 address. 
    /// </summary>
    public class ARecord : Record
    {
        /// <summary>
        /// Constructs a new A record.
        /// </summary>
        /// <exception cref="InvalidIPv4AddressException"></exception>
        public ARecord(IPAddress ipAddress)
        {
            VerifyIPv4Address(ipAddress);
            Content = ipAddress.ToString();
        }

        private static void VerifyIPv4Address(IPAddress ipAddress)
        {
            if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
                throw new InvalidIPv4AddressException(ipAddress);
        }
    }
}