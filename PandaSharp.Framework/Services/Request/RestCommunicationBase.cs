using System.Collections.Generic;
using System.Linq;
using PandaSharp.Framework.Rest.Contract;
using PandaSharp.Framework.Services.Aspect;
using RestSharp;

namespace PandaSharp.Framework.Services.Request
{
    public abstract class RestCommunicationBase
    {
        private readonly IRestFactory _restFactory;
        private readonly IList<IRequestParameterAspect> _parameterAspects;

        protected RestCommunicationBase(IRestFactory restClientFactory, IRequestParameterAspectFactory parameterAspectFactory)
        {
            _restFactory = restClientFactory;
            _parameterAspects = parameterAspectFactory.GetParameterAspects(GetType());
        }

        protected TAspect GetAspect<TAspect>()
        {
            return _parameterAspects.OfType<TAspect>().FirstOrDefault();
        }

        protected abstract string GetResourcePath();

        protected abstract Method GetRequestMethod();

        protected virtual void ApplyToRestRequest(RestRequest restRequest)
        {
        }

        protected IRestClient CreateRestClient()
        {
            return _restFactory.CreateClient();
        }

        protected RestRequest BuildRequest()
        {
            var restRequest = _restFactory.CreateRequest(GetResourcePath(), GetRequestMethod());

            ApplyToRestRequest(restRequest);
            foreach (var aspect in _parameterAspects)
            {
                aspect.ApplyToRestRequest(restRequest);
            }

            return restRequest;
        }
    }
}
