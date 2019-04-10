using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PowerDns.Client.Exceptions;
using PowerDns.Client.Models;
using TypedRest;
using Xunit;

namespace PowerDns.Client
{
    public class ZoneEndpointExtensionsFacts
    {
        private readonly Mock<IElementEndpoint<Zone>> _endpointMock = new Mock<IElementEndpoint<Zone>>();
        private readonly IElementEndpoint<Zone> _endpoint;

        public ZoneEndpointExtensionsFacts()
        {
            _endpoint = _endpointMock.Object;
        }

        [Fact]
        public async Task GettingRecordSetReturnsCorrectRecordSet()
        {
            _endpointMock.Setup(x => x.ReadAsync(CancellationToken.None))
                         .ReturnsAsync(new Zone("example.org")
                          {
                              RecordSets =
                              {
                                  new RecordSet("www.example.org", TimeSpan.FromSeconds(1)),
                                  new RecordSet("mail.example.org", TimeSpan.FromSeconds(1))
                              }
                          });

            var recordSet = await _endpoint.GetRecordSetAsync("www.example.org");

            recordSet.Should().BeEquivalentTo(new RecordSet("www.example.org", TimeSpan.FromSeconds(1)));
        }

        [Fact]
        public void GettingRecordSetThrowsRecordSetNotFoundExceptionIfNotFound()
        {
            _endpointMock.Setup(x => x.ReadAsync(CancellationToken.None))
                         .ReturnsAsync(new Zone("example.org"));

            _endpoint.Awaiting(x => x.GetRecordSetAsync("www.example.org"))
                     .Should().Throw<RecordSetNotFoundException>();
        }

        [Fact]
        public async Task PatchingRecordSetIdentifiesCorrectZoneName()
        {
            const string zoneName = "example.com.";
            var recordSet = new RecordSet("www.example.org", TimeSpan.FromSeconds(1)) {ChangeType = ChangeType.Replace};
            var zone = new Zone(zoneName) {RecordSets = {recordSet}};

            _endpointMock.SetupGet(x => x.Uri).Returns(new Uri($"http://localhost/api/v1/servers/localhost/zones/{zoneName}"));
            _endpointMock.Setup(x => x.MergeAsync(zone, CancellationToken.None)).ReturnsAsync(zone).Verifiable();

            await _endpoint.PatchRecordSetAsync(recordSet);

            _endpointMock.Verify();
        }
    }
}
