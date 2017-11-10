using Newtonsoft.Json.Serialization;

namespace Axoom.Provisioning.PowerDns.JsonConverters
{
    public class LowerCasePropertyNamesContractResolver : DefaultContractResolver
    {
        public LowerCasePropertyNamesContractResolver()
        {
            NamingStrategy = new LowerCaseNamingStrategy();
        }

        private class LowerCaseNamingStrategy : NamingStrategy
        {
            protected override string ResolvePropertyName(string name) => name.ToLowerInvariant();
        }
    }
}