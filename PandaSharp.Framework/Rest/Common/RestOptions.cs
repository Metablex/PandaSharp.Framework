using PandaSharp.Framework.Rest.Contract;

namespace PandaSharp.Framework.Rest.Common
{
    internal sealed class RestOptions : IRestOptions
    {
        private const string RestApiResource = "/rest/api/latest";

        private string _baseUrl;

        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = value;
                if (!_baseUrl.EndsWith(RestApiResource))
                {
                    _baseUrl += RestApiResource;
                }
            }
        }

        public IRestAuthentication Authentication { get; } = new RestAuthentication();
    }
}
