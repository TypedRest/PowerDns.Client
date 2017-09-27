using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using JetBrains.Annotations;

namespace Axoom.Provisioning.PowerDns
{
    [PublicAPI]
    public interface IPowerDns
    {
        /// <summary>
        /// Get a list of <see cref="Zone"/>s.
        /// </summary>
        /// <exception cref="T:TypedRest.AuthenticationException"><see cref="F:System.Net.HttpStatusCode.Unauthorized" /></exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><see cref="F:System.Net.HttpStatusCode.Forbidden" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException"><see cref="F:System.Net.HttpStatusCode.NotFound" /> or <see cref="F:System.Net.HttpStatusCode.Gone" /></exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">Other non-success status code.</exception>
        Task<List<Zone>> GetZonesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a specific zone identified by the <paramref name="zoneName"/>.
        /// </summary>
        /// <exception cref="T:TypedRest.AuthenticationException"><see cref="F:System.Net.HttpStatusCode.Unauthorized" /></exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><see cref="F:System.Net.HttpStatusCode.Forbidden" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException"><see cref="F:System.Net.HttpStatusCode.NotFound" /> or <see cref="F:System.Net.HttpStatusCode.Gone" /></exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">Other non-success status code.</exception>
        Task<Zone> GetZoneAsync(CanonicalName zoneName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds the given <paramref name="zone"/> to the DNS.
        /// </summary>
        /// <exception cref="T:System.IO.InvalidDataException"><see cref="F:System.Net.HttpStatusCode.BadRequest" /></exception>
        /// <exception cref="T:TypedRest.AuthenticationException"><see cref="F:System.Net.HttpStatusCode.Unauthorized" /></exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><see cref="F:System.Net.HttpStatusCode.Forbidden" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException"><see cref="F:System.Net.HttpStatusCode.NotFound" /> or <see cref="F:System.Net.HttpStatusCode.Gone" /></exception>
        /// <exception cref="T:System.InvalidOperationException"><see cref="F:System.Net.HttpStatusCode.Conflict" /></exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">Other non-success status code.</exception>
        Task CreateZoneAsync(Zone zone, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the given <paramref name="zoneName"/> from DNS.
        /// </summary>
        /// <exception cref="T:System.IO.InvalidDataException"><see cref="F:System.Net.HttpStatusCode.BadRequest" /></exception>
        /// <exception cref="T:TypedRest.AuthenticationException"><see cref="F:System.Net.HttpStatusCode.Unauthorized" /></exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><see cref="F:System.Net.HttpStatusCode.Forbidden" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException"><see cref="F:System.Net.HttpStatusCode.NotFound" /> or <see cref="F:System.Net.HttpStatusCode.Gone" /></exception>
        /// <exception cref="T:System.InvalidOperationException">The entity has changed since it was last retrieved with <see cref="M:TypedRest.IElementEndpoint`1.ReadAsync(System.Threading.CancellationToken)" />. Your delete call was rejected to prevent a lost update.</exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">Other non-success status code.</exception>
        Task DeleteZoneAsync(CanonicalName zoneName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Modifies present <see cref="RecordSet"/>s.
        /// </summary>
        /// <remarks><see cref="RecordSet.ChangeType"/> must be set accordingly.</remarks>
        /// <exception cref="ArgumentNullException">If <see cref="RecordSet.ChangeType"/> is not set.</exception>
        /// <exception cref="T:System.IO.InvalidDataException"><see cref="F:System.Net.HttpStatusCode.BadRequest" /></exception>
        /// <exception cref="T:TypedRest.AuthenticationException"><see cref="F:System.Net.HttpStatusCode.Unauthorized" /></exception>
        /// <exception cref="T:System.UnauthorizedAccessException"><see cref="F:System.Net.HttpStatusCode.Forbidden" /></exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException"><see cref="F:System.Net.HttpStatusCode.NotFound" /> or <see cref="F:System.Net.HttpStatusCode.Gone" /></exception>
        /// <exception cref="T:System.InvalidOperationException">The entity has changed since it was last retrieved with <see cref="M:TypedRest.IElementEndpoint`1.ReadAsync(System.Threading.CancellationToken)" />. Your changes were rejected to prevent a lost update.</exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">Other non-success status code.</exception>
        Task PatchRecordSetAsync(CanonicalName zoneName,
                                 RecordSet recordSet,
                                 CancellationToken cancellationToken = default(CancellationToken));
    }
}