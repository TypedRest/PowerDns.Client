using PowerDns.Client.Models;
using TypedRest.Endpoints;
using TypedRest.Endpoints.Generic;

namespace PowerDns.Client
{
    /// <summary>
    /// Represents a collection of DNS zones on a PowerDNS server.
    /// </summary>
    public class ZoneCollectionEndpoint : CollectionEndpoint<Zone>
    {
        public ZoneCollectionEndpoint(IEndpoint referrer)
            : base(referrer, relativeUri: "./zones")
        {}

        public override ElementEndpoint<Zone> this[string id] => new ElementEndpoint<Zone>(this, $"./{new CanonicalName(id)}");
    }
}
