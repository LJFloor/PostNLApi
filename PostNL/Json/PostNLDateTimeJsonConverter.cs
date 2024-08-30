using System;
using Newtonsoft.Json;

namespace PostNLApi.Json
{
    public class PostNLDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.Value == null
                ? existingValue
                : DateTime.Parse(reader.Value.ToString());
        }
    }
}