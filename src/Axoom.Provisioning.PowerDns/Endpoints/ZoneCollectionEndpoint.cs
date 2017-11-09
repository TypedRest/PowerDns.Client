using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using TypedRest;
using HttpClientExtensions = TypedRest.HttpClientExtensions;

namespace Axoom.Provisioning.PowerDns.Endpoints
{
    internal class ZoneCollectionEndpoint : ETagEndpointBase, ICollectionEndpoint<Zone, ZoneElementEndpoint>
    {
        /// <summary>
        /// Creates a new element collection endpoint.
        /// </summary>
        /// <param name="referrer">The endpoint used to navigate to this one.</param>
        /// <param name="relativeUri">The URI of this endpoint relative to the <paramref name="referrer"/>'s. Missing trailing slash will be appended automatically. Prefix <c>./</c> to append a trailing slash to the <paramref name="referrer"/> URI if missing.</param>
        public ZoneCollectionEndpoint(IEndpoint referrer, string relativeUri)
            : base(referrer, relativeUri)
        {
            // TODO: Remove after next TypedRest update
            SetDefaultLinkTemplate(rel: "child", href: "./zones/{id}");
        }

        /// <summary>
        /// Builds a <see cref="ZoneElementEndpoint"/> for a specific child element of this collection. Does not perform any network traffic yet.
        /// </summary>
        /// <param name="relativeUri">The URI of the child endpoint relative to the this endpoint.</param>
        protected virtual ZoneElementEndpoint BuildElementEndpoint(Uri relativeUri) => (ZoneElementEndpoint)Activator.CreateInstance(typeof(ZoneElementEndpoint), this, relativeUri);

        // TODO: Remove after next TypedRest update
        public virtual ZoneElementEndpoint this[string id]
        {
            get
            {
                if (id == null) throw new ArgumentNullException(nameof(id));

                return BuildElementEndpoint(LinkTemplate("child", new {id}));
            }
        }

        ZoneElementEndpoint ICollectionEndpoint<Zone, ZoneElementEndpoint>.this[Zone entity] => this[entity.Name];

        public bool? ReadAllAllowed => IsMethodAllowed(HttpMethod.Get);

        public virtual async Task<List<Zone>> ReadAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = await GetContentAsync(cancellationToken);
            return await content.ReadAsAsync<List<Zone>>(new[] {Serializer}, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// The value used for <see cref="Unit"/>.
        /// </summary>
        public string RangeUnit { get; set; } = "elements";

        protected override void HandleCapabilities(HttpResponseMessage response)
        {
            base.HandleCapabilities(response);
            ReadRangeAllowed = response.Headers.AcceptRanges.Contains(RangeUnit);
        }

        public bool? ReadRangeAllowed { get; private set; }

        public async Task<PartialResponse<Zone>> ReadRangeAsync(RangeItemHeaderValue range,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Uri)
            {
                Headers = {Range = new RangeHeaderValue {Ranges = {range}, Unit = RangeUnit}}
            };

            var response = await HandleResponseAsync(HttpClient.SendAsync(request, cancellationToken)).ConfigureAwait(false);
            return new PartialResponse<Zone>(
                elements: await response.Content.ReadAsAsync<List<Zone>>(new[] {Serializer}, cancellationToken).ConfigureAwait(false),
                range: response.Content.Headers.ContentRange);
        }

        public bool? CreateAllowed => IsMethodAllowed(HttpMethod.Post);

        public virtual async Task<ZoneElementEndpoint> CreateAsync(Zone entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var response = await HandleResponseAsync(HttpClient.PostAsync(Uri, entity, Serializer, cancellationToken)).ConfigureAwait(false);

            return (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.Accepted) && (response.Headers.Location != null)
                ? BuildElementEndpoint(response.Headers.Location)
                : null;
        }

        public bool? CreateAllAllowed => IsMethodAllowed(HttpClientExtensions.Patch);

        public virtual Task CreateAllAsync(IEnumerable<Zone> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return HandleResponseAsync(HttpClient.PatchAsync(Uri, entities, Serializer, cancellationToken));
        }

        public bool? SetAllAllowed => IsMethodAllowed(HttpMethod.Put);

        public async Task SetAllAsync(IEnumerable<Zone> entities, CancellationToken cancellationToken = new CancellationToken())
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var content = new ObjectContent<IEnumerable<Zone>>(entities, Serializer);
            await PutContentAsync(content, cancellationToken);
        }
    }
}