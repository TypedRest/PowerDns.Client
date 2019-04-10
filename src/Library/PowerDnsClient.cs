using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypedRest;

namespace PowerDns.Client
{
    /// <summary>
    /// A type-safe client for the PowerDNS API.
    /// </summary>
    public class PowerDnsClient : EntryEndpoint, IPowerDnsClient
    {
        /// <summary>
        /// Creates a new PowerDNS API Client.
        /// </summary>
        /// <param name="uri">The URI of the PowerDNS instance (without /api/v1).</param>
        /// <param name="apiKey">The API key used for authentication.</param>
        public PowerDnsClient(Uri uri, string apiKey)
            : base(uri, serializer: BuildSerializer())
        {
            HttpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
        }

        /// <summary>
        /// Creates a new PowerDNS API Client using a custom <see cref="HttpClient"/>. This is usually used for custom authentication schemes, e.g. client certificates.
        /// </summary>
        /// <param name="uri">The URI of the PowerDNS instance (without /api/v1).</param>
        /// <param name="httpClient">The <see cref="HttpClient"/> to use for communication with My Service.</param>
        public PowerDnsClient(Uri uri, HttpClient httpClient)
            : base(uri, httpClient, serializer: BuildSerializer())
        {}

        public IIndexerEndpoint<IServerElementEndpoint> Servers => new IndexerEndpoint<ServerElementEndpoint>(this, relativeUri: "./api/v1/servers");

        private static JsonMediaTypeFormatter BuildSerializer()
            => new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new DefaultContractResolver {NamingStrategy = new LowerCaseNamingStrategy()}
                }
            };

        private class LowerCaseNamingStrategy : NamingStrategy
        {
            protected override string ResolvePropertyName(string name) => name.ToLowerInvariant();
        }
    }
}
