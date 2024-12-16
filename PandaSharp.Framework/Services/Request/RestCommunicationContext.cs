using System.Collections.Generic;
using PandaSharp.Framework.Services.Contract;

namespace PandaSharp.Framework.Services.Request
{
    public sealed class RestCommunicationContext : IRestCommunicationContext
    {
        private readonly IDictionary<string, object> _contextParameters = new Dictionary<string, object>();

        public void AddContextParameter<T>(string parameter, T value)
        {
            _contextParameters[parameter] = value;
        }

        public T GetContextParameter<T>(string parameterName)
        {
            if (_contextParameters.TryGetValue(parameterName, out var value))
            {
                return (T)value;
            }

            throw new KeyNotFoundException($"Parameter '{parameterName}' was not found.");
        }
    }
}
