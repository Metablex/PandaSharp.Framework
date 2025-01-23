using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace PandaSharp.Framework.Rest.Common
{
    public sealed class JsonRestSerializer : IRestSerializer, ISerializer, IDeserializer
    {
        public ISerializer Serializer => this;

        public IDeserializer Deserializer => this;

        public string[] AcceptedContentTypes { get; }

        public DataFormat DataFormat => DataFormat.Json;

        public ContentType ContentType { get; set; } = ContentType.Json;

        public SupportsContentType SupportsContentType
        {
            get
            {

                return contentType => contentType.Value.EndsWith("json", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public static JsonRestSerializer Default { get; } =
            new JsonRestSerializer(new[]
            {
                "application/json",
                "text/json",
                "text/x-json",
                "text/javascript",
                "*+json"
            });

        public JsonRestSerializer(IEnumerable<string> acceptedContentTypes)
        {
            AcceptedContentTypes = acceptedContentTypes.ToArray();
        }

        public string Serialize(Parameter parameter)
        {
            if (parameter.Value == null)
            {
                return null;
            }

            return Serialize(parameter.Value);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(RestResponse response)
        {
            if (response.Content == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
