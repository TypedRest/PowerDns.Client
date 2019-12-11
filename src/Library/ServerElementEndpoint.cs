using System;
using PowerDns.Client.Models;
using TypedRest.Endpoints;
using TypedRest.Endpoints.Generic;

namespace PowerDns.Client
{
    /// <summary>
    /// Represents a specific server managed by an instance of the PowerDNS API.
    /// </summary>
    public class ServerElementEndpoint : EndpointBase, IServerElementEndpoint
    {
        public ServerElementEndpoint(IEndpoint referrer, Uri relativeUri)
            : base(referrer, relativeUri)
        {}

        public ICollectionEndpoint<Zone> Zones => new ZoneCollectionEndpoint(this);
    }
}
