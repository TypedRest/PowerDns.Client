using FluentAssertions;
using Xunit;

namespace Axoom.Provisioning.PowerDns
{
    public class StringExtensionsFacts
    {
        [Fact]
        public void AddsTrailingDotIfMissing()
        {
            const string expected = "test.";

            string actual = "test".EnsureTrailingDot();

            actual.Should().Be(expected);
        }
        
        [Fact]
        public void AddsNotTrailingDotIfAlreadyPresent()
        {
            const string expected = "test.";

            string actual = "test.".EnsureTrailingDot();

            actual.Should().Be(expected);
        }
    }
}