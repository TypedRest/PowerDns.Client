using System;
using System.Threading;
using System.Threading.Tasks;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using Axoom.Provisioning.PowerDns.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Axoom.Provisioning.PowerDns
{
    public class PowerDnsExtensionsFacts
    {
        private readonly Mock<IPowerDns> _powerDnsMock;
        private readonly IPowerDns _powerDns;

        public PowerDnsExtensionsFacts()
        {
            _powerDnsMock = new Mock<IPowerDns>();
            _powerDns = _powerDnsMock.Object;
        }

        [Fact]
        public void GettingRecordSetThrowsRecordSetNotFoundExceptionIfNotFound()
        {
            _powerDnsMock.Setup(mock => mock.GetZoneAsync("example.org", It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new Zone("example.org"));
            
            _powerDns.Awaiting(dns => dns.GetRecordSetAsync("example.org", "www.example.org"))
                     .ShouldThrow<RecordSetNotFoundException>();
        }

        [Fact]
        public async Task GettingRecordSetReturnsCorrectRecordSet()
        {
            var zone = new Zone("example.org")
            {
                RecordSets =
                {
                    new RecordSet("www.example.org", TimeSpan.FromSeconds(1)),
                    new RecordSet("mail.example.org", TimeSpan.FromSeconds(1))
                }
            };
            _powerDnsMock.Setup(mock => mock.GetZoneAsync("example.org", It.IsAny<CancellationToken>())).ReturnsAsync(zone);

            RecordSet recordSet = await _powerDns.GetRecordSetAsync("example.org", "www.example.org");
            
            recordSet.ShouldBeEquivalentTo(new RecordSet("www.example.org", TimeSpan.FromSeconds(1)));
        }
    }
}