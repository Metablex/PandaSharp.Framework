using PandaSharp.Framework.Rest.Contract;
using RestSharp;
using RestSharp.Serializers;

namespace PandaSharp.Framework.Rest.Common
{
    public sealed class RestFactory : IRestFactory
    {
        private readonly IRestOptions _restOptions;
        private readonly IRestSerializer _serializer;

        public RestFactory(IRestOptions restOptions, IRestSerializer serializer)
        {
            _restOptions = restOptions;
            _serializer = serializer;
        }

        public IRestClient CreateClient()
        {
            var options = new RestClientOptions(_restOptions.BaseUrl)
            {
                Authenticator = _restOptions.Authenticator
            };

            var client = new RestClient(
                options,
                configureSerialization: s => s.UseSerializer(() => _serializer));

            return client;
        }

        public RestRequest CreateRequest(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}
