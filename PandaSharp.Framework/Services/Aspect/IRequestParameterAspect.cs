using RestSharp;

namespace PandaSharp.Framework.Services.Aspect
{
    public interface IRequestParameterAspect
    {
        void ApplyToRestRequest(IRestRequest restRequest);
    }
}