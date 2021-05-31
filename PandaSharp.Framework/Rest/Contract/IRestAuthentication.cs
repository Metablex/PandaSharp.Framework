using RestSharp.Authenticators;

namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestAuthentication
    {
        IAuthenticator CreateAuthenticator();

        void UseBasic(string userName, string userPassword);

        void UseOAuth(
            string consumerKey,
            string consumerSecret,
            string oAuthAccessToken,
            string oAuthTokenSecret);
    }
}