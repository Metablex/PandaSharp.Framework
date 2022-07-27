using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.Services.Response;
using PandaSharp.Framework.Utils;

namespace PandaSharp.Framework.Services.Converter
{
    public sealed class RootElementResponseConverter<T, TItem> : JsonConverter<T>
        where T : ListResponseBase<TItem>, new()
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var result = new T();
            var json = JObject.Load(reader);

            var rootElementPath = GetRootElementPath();
            foreach (var property in typeof(T).GetProperties())
            {
                var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
                if (attribute != null)
                {
                    var propertyValue = json.SelectToken($"{rootElementPath}.{attribute.PropertyName}");
                    if (propertyValue != null)
                    {
                        property.SetValue(result, propertyValue.ToObject(property.PropertyType));
                    }
                }
            }

            var listContentPath = GetListContentPath();
            if (!listContentPath.IsNullOrEmpty())
            {
                var propertyValues = json
                    .SelectTokens(listContentPath)
                    .Select(i => i.ToObject<TItem>());

                result.AddResponses(propertyValues);
            }

            return result;
        }

        private static string GetRootElementPath()
        {
            var jsonRootElementAttribute = typeof(T).GetCustomAttribute<JsonRootElementPathAttribute>();

            return jsonRootElementAttribute?.RootElementPath;
        }

        private static string GetListContentPath()
        {
            var jsonItemsAttribute = typeof(T).GetCustomAttribute<JsonListContentPathAttribute>();

            return jsonItemsAttribute?.ListContentPath;
        }
    }
}