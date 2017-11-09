using System;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypedRest;

namespace Axoom.Provisioning.PowerDns.Endpoints
{
    internal class PowerDnsEntryEndpoint : EntryEndpoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">The URI of the PowerDNS API including the API version.</param>
        /// <param name="apiKey">The API key used for authentication.</param>
        public PowerDnsEntryEndpoint(Uri uri, string apiKey)
            : base(uri, serializer: CreateJsonMediaTypeFormatter()) =>
            HttpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

        public ServerCollectionEndpoint Servers => new ServerCollectionEndpoint(this, new Uri("servers", UriKind.Relative));

        private static JsonMediaTypeFormatter CreateJsonMediaTypeFormatter()
            => new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };
    }
}