using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace PandaSharp.Framework.Utils
{
    public static class RestRequestExtensions
    {
        public static IRestRequest AddParameterIfSet(this IRestRequest restRequest, string parameter, object value)
        {
            return restRequest.AddParameterIfSet(parameter, value, ParameterType.QueryString);
        }

        public static IRestRequest AddNotEncodedParameterIfSet(this IRestRequest restRequest, string parameter, object value)
        {
            return restRequest.AddParameterIfSet(parameter, value, ParameterType.QueryStringWithoutEncode);
        }

        public static IRestRequest AddParameterValues(this IRestRequest restRequest, string parameter, IEnumerable<string> values)
        {
            if (values == null)
            {
                return restRequest;
            }

            var validValues = values
                .Where(value => !value.IsNullOrEmpty())
                .ToArray();

            if (validValues.Length > 0)
            {
                var parameterValues = string.Join(",", validValues);
                return restRequest.AddParameter(parameter, parameterValues);
            }

            return restRequest;
        }

        private static IRestRequest AddParameterIfSet(this IRestRequest restRequest, string parameter, object value, ParameterType parameterType)
        {
            if (value != null)
            {
                return restRequest.AddParameter(parameter, value, parameterType);
            }

            return restRequest;
        }
    }
}
