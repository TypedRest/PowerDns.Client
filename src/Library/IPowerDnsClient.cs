using TypedRest.Endpoints;
using TypedRest.Endpoints.Generic;

namespace PowerDns.Client
{
    /// <summary>
    /// A type-safe client for the PowerDNS API.
    /// </summary>
    public interface IPowerDnsClient : IEndpoint
    {
        /// <summary>
        /// Represents a list of servers managed by this instance of the PowerDNS API.
        /// </summary>
        IIndexerEndpoint<IServerElementEndpoint> Servers { get; }
    }
}
