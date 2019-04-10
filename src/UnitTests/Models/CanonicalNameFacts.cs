using FluentAssertions;
using Xunit;

namespace PowerDns.Client.Models
{
    public class CanonicalNameFacts
    {
        [Fact]
        public void AddsTrailingDotIfMissing()
        {
            new CanonicalName("test").ToString().Should().Be("test.");
        }

        [Fact]
        public void AddsNotTrailingDotIfAlreadyPresent()
        {
            new CanonicalName("test.").ToString().Should().Be("test.");
        }
    }
}
