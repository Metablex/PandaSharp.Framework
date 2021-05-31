using RestSharp;

namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestFactory
    {
        IRestClient CreateClient();

        IRestRequest CreateRequest(string resource, Method method);
    }
}
