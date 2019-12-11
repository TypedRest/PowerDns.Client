using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using PowerDns.Client.Models;
using RichardSzalay.MockHttp;
using TypedRest.Endpoints.Generic;
using Xunit;

namespace PowerDns.Client
{
    public class ZoneCollectionFacts : EndpointFactsBase
    {
        private readonly ICollectionEndpoint<Zone> _zonesEndpoint;

        public ZoneCollectionFacts()
        {
            _zonesEndpoint = Client.Servers["localhost"].Zones;
        }

        [Fact]
        public async Task GetAll()
        {
            Mock.Expect(HttpMethod.Get, "http://localhost/api/v1/servers/localhost/zones")
                .Respond(_ => new HttpResponseMessage
                 {
                     Content = new StringContent("[{\"name\": \"example.org.\", \"nameservers\": [\"ns1.example.org\", \"ns2.example.org\"]}]", Encoding.UTF8, JsonMime)
                 });

            var zones = await _zonesEndpoint.ReadAllAsync();

            zones.Should().Equal(new Zone("example.org", /*nameservers:*/ "ns1.example.org", "ns2.example.org"));
        }

        [Fact]
        public async Task Get()
        {
            Mock.Expect(HttpMethod.Get, "http://localhost/api/v1/servers/localhost/zones/example.org.")
                .Respond(_ => new HttpResponseMessage
                 {
                     Content = new StringContent("{\"name\": \"example.org.\", \"nameservers\": [\"ns1.example.org\", \"ns2.example.org\"]}", Encoding.UTF8, JsonMime)
                 });

            var zone = await _zonesEndpoint["example.org"].ReadAsync();

            zone.Should().Be(new Zone("example.org", /*nameservers:*/ "ns1.example.org", "ns2.example.org"));
        }

        [Fact]
        public async Task Create()
        {
            Mock.Expect(HttpMethod.Post, "http://localhost/api/v1/servers/localhost/zones")
                .Respond(HttpStatusCode.Created);

            await _zonesEndpoint.CreateAsync(new Zone("example.org", /*nameservers:*/ "ns1.example.org", "ns2.example.org"));
        }

        [Fact]
        public async Task Delete()
        {
            Mock.Expect(HttpMethod.Delete, "http://localhost/api/v1/servers/localhost/zones/example.org.")
                .Respond(HttpStatusCode.NoContent);

            await _zonesEndpoint["example.org"].DeleteAsync();
        }
    }
}
