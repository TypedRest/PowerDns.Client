using System;
using Newtonsoft.Json.Linq;
using TypedRest;

namespace Axoom.Provisioning.PowerDns.Endpoints
{
    internal class ServerCollectionEndpoint : CollectionEndpoint<JToken, ServerElementEndpoint>
    {
        public ServerCollectionEndpoint(IEndpoint referrer, Uri relativeUri)
            : base(referrer, relativeUri)
        {
        }
    }
}