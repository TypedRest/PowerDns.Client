using System;
using FluentAssertions;
using Xunit;

namespace Axoom.Provisioning.PowerDns
{
    public class PowerDnsHttpClientFacts
    {
        private readonly PowerDnsHttpClient _client;

        public PowerDnsHttpClientFacts() 
            => _client = new PowerDnsHttpClient(new Uri("http://host"), apiKey: "123");

        [Fact]
        public void ServerCollectionEndpointHasCorrectUri()
        {
            _client.Servers.Uri.Should().Be("http://host/api/v1/servers/");
        }
        
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