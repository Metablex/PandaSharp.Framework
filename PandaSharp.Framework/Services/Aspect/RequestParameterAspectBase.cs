using RestSharp;

namespace PandaSharp.Framework.Services.Aspect
{
    public abstract class RequestParameterAspectBase : IRequestParameterAspect
    {
        public abstract void ApplyToRestRequest(RestRequest restRequest);
    }
}
