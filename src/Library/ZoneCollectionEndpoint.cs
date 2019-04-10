using System.Threading;
using System.Threading.Tasks;
using PowerDns.Client.Models;
using TypedRest;

namespace PowerDns.Client
{
    /// <summary>
    /// Represents a collection of DNS zones on a PowerDNS server.
    /// </summary>
    public class ZoneCollectionEndpoint : CollectionEndpoint<Zone>, IZoneCollectionEndpoint
    {
        public ZoneCollectionEndpoint(IEndpoint referrer)
            : base(referrer, relativeUri: "./zones")
        {}

        public override IElementEndpoint<Zone> this[string id] => this[new CanonicalName(id)];

        public IElementEndpoint<Zone> this[CanonicalName canonicalName] => new ElementEndpoint<Zone>(this, $"./{canonicalName}");
    }
}
