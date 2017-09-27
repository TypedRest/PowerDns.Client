using System;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using TypedRest;

namespace Axoom.Provisioning.PowerDns.Endpoints
{
    public class ZoneElementEndpoint : ElementEndpoint<Zone>
    {
        public ZoneElementEndpoint(IEndpoint referrer, Uri relativeUri)
            : base(referrer, relativeUri)
        {
        }
    }
}