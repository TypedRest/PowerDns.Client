using System;
using TypedRest;

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

        public IZoneCollectionEndpoint Zones => new ZoneCollectionEndpoint(this);
    }
}
