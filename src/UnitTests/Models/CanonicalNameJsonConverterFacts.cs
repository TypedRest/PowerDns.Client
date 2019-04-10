using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace PowerDns.Client.Models
{
    public class CanonicalNameJsonConverterFacts
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public CanonicalNameJsonConverterFacts()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                Converters = {new CanonicalNameJsonConverter()}
            };
        }

        [Fact]
        public void IsSerializable()
        {
            CanonicalName name = "axoom.com";

            string json = JsonConvert.SerializeObject(name, _jsonSerializerSettings);

            json.Should().Be("\"axoom.com.\"");
        }

        [Fact]
        public void IsDeserializable()
        {
            const string json = "\"axoom.com.\"";

            var name = JsonConvert.DeserializeObject<CanonicalName>(json, _jsonSerializerSettings);

            name.Should().Be(new CanonicalName("axoom.com"));
        }
    }
}
