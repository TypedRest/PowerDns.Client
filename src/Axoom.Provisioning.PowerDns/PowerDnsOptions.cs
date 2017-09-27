using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Axoom.Provisioning.PowerDns
{
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public class PowerDnsOptions
    {
        public string Hostname { get; set; } = "localhost";
        public int Port { get; set; } = 80;
        public bool UseTls { get; set; }
        public string ApiKey { get; set; }

        public Uri GetEndpoint() => new Uri($"http{(UseTls ? "s": string.Empty)}://{Hostname}:{Port}");
    }
}