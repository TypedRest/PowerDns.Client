using System;
using FluentAssertions;
using Xunit;

namespace Axoom.Provisioning.PowerDns.Endpoints
{
    public class PowerDnsEntryEndpointFacts
    {
        private readonly PowerDnsEntryEndpoint _client;

        public PowerDnsEntryEndpointFacts() 
            => _client = new PowerDnsEntryEndpoint(new Uri("http://host/api/v1/"), apiKey: "123");
        
        [Fact]
        public void ServerElementEndpointHasCorrectUri()
        {
            _client.Servers["localhost"].Uri.Should().Be("http://host/api/v1/servers/localhost");
        }
        
        [Fact]
        public void ZoneCollectionEndpointHasCorrectUri()
        {
            _client.Servers["localhost"].Zones.Uri.Should().Be("http://host/api/v1/servers/localhost/zones");
        }
           
        [Fact]
        public void ZoneElementEndpointHasCorrectUri()
        {
            _client.Servers["localhost"].Zones["example.com."].Uri.Should().Be("http://host/api/v1/servers/localhost/zones/example.com.");
        }
    }
}