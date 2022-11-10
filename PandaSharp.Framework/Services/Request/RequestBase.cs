using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PandaSharp.Framework.Rest.Contract;
using PandaSharp.Framework.Services.Aspect;
using PandaSharp.Framework.Services.Contract;
using PandaSharp.Framework.Utils;

namespace PandaSharp.Framework.Services.Request
{
    public abstract class RequestBase<T> : RestCommunicationBase, IRequestBase<T>
        where T : class, new()
    {
        private readonly IRestResponseConverter _responseConverter;

        protected RequestBase(IRestFactory restClientFactory, IRequestParameterAspectFactory parameterAspectFactory, IRestResponseConverterFactory responseConverterFactory)
            : base(restClientFactory, parameterAspectFactory)
        {
            _responseConverter = responseConverterFactory.CreateResponseConverter(GetType());
        }

        public async Task<T> ExecuteAsync(CancellationToken cancellationToken)
        {
            var client = CreateRestClient();
            var request = BuildRequest();
            var response = await client.ExecuteAsync<T>(request, cancellationToken);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Unable to authenticate. Please check your credentials.");
                }

                throw new InvalidOperationException($"Error retrieving response: {response.GetErrorResponseMessage()}");
            }

            return _responseConverter.ConvertRestResponse(response);
        }

        public Task<T> ExecuteAsync()
        {
            return ExecuteAsync(CancellationToken.None);
        }

        public Uri GetRequestUri()
        {
            var client = CreateRestClient();
            var request = BuildRequest();

            return client.BuildUri(request);
        }
    }
}