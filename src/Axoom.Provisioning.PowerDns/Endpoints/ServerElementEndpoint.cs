using System;
using Newtonsoft.Json.Linq;
using TypedRest;

namespace Axoom.Provisioning.PowerDns.Endpoints
{
    internal class ServerElementEndpoint : ElementEndpoint<JToken>
    {
        public ServerElementEndpoint(IEndpoint referrer, Uri relativeUri)
            : base(referrer, relativeUri)
        {
        }

        public ZoneCollectionEndpoint Zones => new ZoneCollectionEndpoint(this, "./zones");
    }
}