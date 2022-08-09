using PandaSharp.Framework.Rest.Contract;
using RestSharp.Authenticators;

namespace PandaSharp.Framework.Rest.Common
{
    internal sealed class RestOptions : IRestOptions
    {
        public string BaseUrl { get; }
        
        public IAuthenticator Authenticator { get; }

        public RestOptions(string baseUrl, IAuthenticator authenticator)
        {
            BaseUrl = baseUrl;
            Authenticator = authenticator;
        }
    }
}
