using PandaSharp.Framework.Rest.Contract;
using RestSharp;
using RestSharp.Serialization;

namespace PandaSharp.Framework.Rest.Common
{
    internal sealed class RestFactory : IRestFactory
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
            var client = new RestClient(_restOptions.BaseUrl)
            {
                Authenticator = _restOptions.Authentication.CreateAuthenticator()
            };

            client.UseSerializer(() => _serializer);
            return client;
        }

        public IRestRequest CreateRequest(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}
