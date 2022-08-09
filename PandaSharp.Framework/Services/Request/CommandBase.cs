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
    public abstract class CommandBase : RestCommunicationBase, ICommandBase
    {
        protected CommandBase(IRestFactory restClientFactory, IRequestParameterAspectFactory parameterAspectFactory)
            : base(restClientFactory, parameterAspectFactory)
        {
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var client = CreateRestClient();
            var request = BuildRequest();
            var response = await client.ExecuteAsync(request, cancellationToken);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Unable to authenticate or insufficient rights. Please check your credentials.");
                }

                throw new InvalidOperationException($"Error retrieving response: {response.GetErrorResponseMessage()}");
            }
        }

        public Task ExecuteAsync()
        {
            return ExecuteAsync(CancellationToken.None);
        }

        public Uri GetCommandUri()
        {
            var client = CreateRestClient();
            var request = BuildRequest();

            return client.BuildUri(request);
        }
    }
}