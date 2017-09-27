using System;
using Axoom.Provisioning.PowerDns.DataTransferObjects;
using Newtonsoft.Json;

namespace Axoom.Provisioning.PowerDns.JsonConverters
{
    public class CanonicalNameJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => writer.WriteValue(value.ToString());

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => new CanonicalName(reader.Value.ToString());

        public override bool CanConvert(Type objectType) => objectType == typeof(CanonicalName);
    }
}