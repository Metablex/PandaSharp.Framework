using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace PandaSharp.Framework.Rest.Common
{
    internal sealed class RestRequestSerializer : IRestSerializer
    {
        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public string Serialize(Parameter parameter)
        {
            return Serialize(parameter.Value);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}