using RestSharp;

namespace PandaSharp.Framework.Services.Aspect
{
    public interface IRequestParameterAspect
    {
        void ApplyToRestRequest(RestRequest restRequest);
    }
}
