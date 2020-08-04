using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Entities;

namespace Common.JsonConverters
{
    public class MetadataConverter : JsonConverter<MetadataBase>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public MetadataConverter()
        {
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };
        }
        
        public override MetadataBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var type = doc.RootElement.GetProperty(@"eventType").GetString();
                switch (type)
                {
                    case "Factory":
                        return JsonSerializer.Deserialize<MetadataFactory>(doc.RootElement.GetRawText());
                    case "Machine":
                        return JsonSerializer.Deserialize<MetadataMachine>(doc.RootElement.GetRawText());
                    case "MaintenanceLookup":
                        return JsonSerializer.Deserialize<MetadataMaintenanceLookup>(doc.RootElement.GetRawText());
                    default:
                        return JsonSerializer.Deserialize<MetadataBase>(doc.RootElement.GetRawText());
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, MetadataBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}