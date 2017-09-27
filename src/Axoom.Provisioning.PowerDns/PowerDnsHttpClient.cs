using System;
using System.Net.Http.Formatting;
using Axoom.Provisioning.PowerDns.Endpoints;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypedRest;

namespace Axoom.Provisioning.PowerDns
{
    internal class PowerDnsHttpClient : EntryEndpoint
    {
        public PowerDnsHttpClient(Uri uri, string apiKey)
            : base(new Uri(uri, new Uri("api/v1", UriKind.Relative)), serializer: CreateJsonMediaTypeFormatter()) =>
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