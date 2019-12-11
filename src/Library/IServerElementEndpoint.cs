using PowerDns.Client.Models;
using TypedRest.Endpoints;
using TypedRest.Endpoints.Generic;

namespace PowerDns.Client
{
    /// <summary>
    /// Represents a specific server managed by an instance of the PowerDNS API.
    /// </summary>
    public interface IServerElementEndpoint : IEndpoint
    {
        /// <summary>
        /// Represents a list of DNS zones managed by a PowerDNS server.
        /// </summary>
        ICollectionEndpoint<Zone> Zones { get; }
    }
}
