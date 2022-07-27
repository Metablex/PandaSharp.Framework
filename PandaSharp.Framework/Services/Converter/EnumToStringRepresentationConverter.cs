using System;
using Newtonsoft.Json;
using PandaSharp.Framework.Utils;

namespace PandaSharp.Framework.Services.Converter
{
    public sealed class EnumToStringRepresentationConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var enumText = reader.Value?.ToString();

                if (enumText.IsNullOrEmpty())
                {
                    return null;
                }

                return enumText.GetEnumMember(objectType);
            }

            throw new InvalidOperationException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }
    }
}