using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using PowerDns.Client.Exceptions;
using PowerDns.Client.Models;
using TypedRest.Endpoints.Generic;

namespace PowerDns.Client
{
    public static class ZoneEndpointExtensions
    {
        /// <summary>
        /// Gets a specific <see cref="RecordSet"/> identified by a <paramref name="recordSetName"/>.
        /// </summary>
        /// <exception cref="RecordSetNotFoundException"></exception>
        public static async Task<RecordSet> GetRecordSetAsync(this IElementEndpoint<Zone> endpoint,
                                                              CanonicalName recordSetName,
                                                              CancellationToken cancellationToken = default)
        {
            var zone = await endpoint.ReadAsync(cancellationToken);
            var recordSet = zone.RecordSets.FirstOrDefault(rrs => rrs.Name.Equals(recordSetName));

            if (recordSet == null)
                throw new RecordSetNotFoundException(recordSetName);

            return recordSet;
        }

        /// <summary>
        /// Modifies existing <see cref="RecordSet"/>s.
        /// </summary>
        /// <remarks><see cref="RecordSet.ChangeType"/> must be set accordingly.</remarks>
        /// <exception cref="ArgumentNullException">If <see cref="RecordSet.ChangeType"/> is not set.</exception>
        /// <exception cref="InvalidDataException"><see cref="HttpStatusCode.BadRequest" /></exception>
        /// <exception cref="AuthenticationException"><see cref="HttpStatusCode.Unauthorized" /></exception>
        /// <exception cref="UnauthorizedAccessException"><see cref="HttpStatusCode.Forbidden" /></exception>
        /// <exception cref="KeyNotFoundException"><see cref="HttpStatusCode.NotFound" /> or <see cref="HttpStatusCode.Gone" /></exception>
        /// <exception cref="HttpRequestException">Other non-success status code.</exception>
        public static Task PatchRecordSetAsync(this IElementEndpoint<Zone> endpoint,
                                               RecordSet recordSet,
                                               CancellationToken cancellationToken = default)
        {
            if (!recordSet.ChangeType.HasValue)
                throw new ArgumentException($"{nameof(ChangeType)} must be set.", nameof(recordSet));

            string zoneName = endpoint.Uri.AbsolutePath.Split('/').Last();
            return endpoint.MergeAsync(new Zone(zoneName) {RecordSets = {recordSet}}, cancellationToken);
        }

        /// <summary>
        /// Upserts the given <paramref name="recordSet"/>.
        /// </summary>
        public static Task UpsertZoneRecordSetAsync(this IElementEndpoint<Zone> endpoint,
                                                    RecordSet recordSet,
                                                    CancellationToken cancellationToken = default)
        {
            recordSet.ChangeType = ChangeType.Replace;
            return endpoint.PatchRecordSetAsync(recordSet, cancellationToken);
        }
    }
}
