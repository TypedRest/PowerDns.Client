using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using Axoom.Provisioning.PowerDns.Endpoints;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace Axoom.Provisioning.PowerDns
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public sealed class PowerDns : IPowerDns
    {
        private readonly ServerElementEndpoint _serverEndpoint;

        public PowerDns(IOptions<PowerDnsOptions> options)
            => _serverEndpoint = new PowerDnsHttpClient(options.Value.GetEndpoint(), options.Value.ApiKey).Servers["localhost"];

        public Task<List<Zone>> GetZonesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => _serverEndpoint.Zones.ReadAllAsync(cancellationToken);

        public Task<Zone> GetZoneAsync(CanonicalName zoneName, CancellationToken cancellationToken = default(CancellationToken))
            => _serverEndpoint.Zones[zoneName].ReadAsync();

        public Task CreateZoneAsync(Zone zone, CancellationToken cancellationToken = default(CancellationToken))
            => _serverEndpoint.Zones.CreateAsync(zone, cancellationToken);

        public Task DeleteZoneAsync(CanonicalName zoneName, CancellationToken cancellationToken = default(CancellationToken))
            => _serverEndpoint.Zones[zoneName].DeleteAsync(cancellationToken);

        public Task PatchRecordSetAsync(CanonicalName zoneName,
                                        RecordSet recordSet,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!recordSet.ChangeType.HasValue)
                throw new ArgumentNullException(nameof(recordSet.ChangeType));

            return _serverEndpoint.Zones[zoneName].MergeAsync(new Zone(zoneName) {RecordSets = {recordSet}}, cancellationToken);
        }
    }
}