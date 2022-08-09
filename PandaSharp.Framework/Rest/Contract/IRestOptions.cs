using RestSharp.Authenticators;

namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestOptions
    {
        string BaseUrl { get; }

        IAuthenticator Authenticator { get; }
    }
}