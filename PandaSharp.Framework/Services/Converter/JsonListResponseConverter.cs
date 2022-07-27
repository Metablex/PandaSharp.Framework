using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PandaSharp.Framework.Services.Converter
{
    public sealed class JsonListResponseConverter<T> : JsonConverter<List<T>>
    {
        private readonly string _itemsPath;

        public override bool CanWrite => false;

        public JsonListResponseConverter(string itemsPath)
        {
            _itemsPath = itemsPath;
        }

        public override void WriteJson(JsonWriter writer, List<T> value, JsonSerializer serializer)
        {
        }

        public override List<T> ReadJson(JsonReader reader, Type objectType, List<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var json = JObject.Load(reader);
            var items = json.SelectToken(_itemsPath);

            if (items != null && items.HasValues)
            {
                return items.ToObject<List<T>>();
            }

            return new List<T>();
        }
    }
}