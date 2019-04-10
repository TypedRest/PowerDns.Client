using System.Threading;
using System.Threading.Tasks;
using PowerDns.Client.Models;
using TypedRest;

namespace PowerDns.Client
{
    /// <summary>
    /// Represents a collection of DNS zones on a PowerDNS server.
    /// </summary>
    public interface IZoneCollectionEndpoint : ICollectionEndpoint<Zone>
    {
        /// <summary>
        /// Returns a endpoint for a specific <see cref="Zone"/> in this collection identified by a <see cref="CanonicalName"/>.
        /// </summary>
        IElementEndpoint<Zone> this[CanonicalName canonicalName] { get; }
    }
}
