using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using Axoom.Provisioning.PowerDns.Exceptions;
using JetBrains.Annotations;

namespace Axoom.Provisioning.PowerDns
{
    [PublicAPI]
    public static class PowerDnsExtensions
    {
        /// <summary>
        /// Gets a specific <see cref="RecordSet"/> identified by <paramref name="zoneName"/> and <paramref name="recordSetName"/>.
        /// </summary>
        /// <exception cref="RecordSetNotFoundException"></exception>
        public static async Task<RecordSet> GetRecordSetAsync(this IPowerDns powerDns,
                                                              CanonicalName zoneName,
                                                              CanonicalName recordSetName,
                                                              CancellationToken cancellationToken = default(CancellationToken))
        {
            Zone zone = await powerDns.GetZoneAsync(zoneName, cancellationToken);
            RecordSet recordSet = zone.RecordSets.FirstOrDefault(rrs => rrs.Name.Equals(recordSetName));

            if (recordSet == null)
                throw new RecordSetNotFoundException(zoneName, recordSetName);

            return recordSet;
        }

        /// <summary>
        /// Upserts the given <paramref name="recordSet"/> for the specified <paramref name="zoneName"/>.
        /// </summary>
        public static Task UpsertZoneRecordSetAsync(this IPowerDns powerDns,
                                                          CanonicalName zoneName,
                                                          RecordSet recordSet,
                                                          CancellationToken cancellationToken = default(CancellationToken))
        {
            recordSet.ChangeType = ChangeType.Replace;
            return powerDns.PatchRecordSetAsync(zoneName, recordSet, cancellationToken);
        }
    }
}