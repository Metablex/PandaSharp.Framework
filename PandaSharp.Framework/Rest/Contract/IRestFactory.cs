using RestSharp;

namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestFactory
    {
        IRestClient CreateClient();

        RestRequest CreateRequest(string resource, Method method);
    }
}
