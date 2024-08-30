using System;
using Newtonsoft.Json;

namespace PostNLApi.Json
{
    /// <summary>
    /// JsonConverter that trims strings when serializing and deserializing
    /// </summary>
    public class PostNLStringTrimmer : JsonConverter<string>
    {
        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.Trim());
            }
        }

        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.TokenType == JsonToken.String
                ? ((string)reader.Value)?.Trim()
                : existingValue;
        }
    }
}