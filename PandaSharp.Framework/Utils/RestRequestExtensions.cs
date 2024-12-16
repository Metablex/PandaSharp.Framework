using System;
using System.Collections.Generic;
using RestSharp;

namespace PandaSharp.Framework.Utils
{
    public static class RestRequestExtensions
    {
        public static RestRequest AddParameterIfSet(this RestRequest restRequest, string parameter, string value)
        {
            if (!value.IsNullOrEmpty())
            {
                return restRequest.AddQueryParameter(parameter, value);
            }

            return restRequest;
        }

        public static RestRequest AddParameterIfSet(this RestRequest restRequest, string parameter, DateTime? value)
        {
            if (value != null)
            {
                return restRequest.AddQueryParameter(parameter, value.Value.ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            return restRequest;
        }

        public static RestRequest AddParameterIfSet(this RestRequest restRequest, string parameter, int? value)
        {
            if (value != null)
            {
                return restRequest.AddQueryParameter(parameter, value.Value.ToString());
            }

            return restRequest;
        }

        public static RestRequest AddParameterIfSet(this RestRequest restRequest, string parameter, bool? value)
        {
            if (value != null)
            {
                return restRequest.AddQueryParameter(parameter, value.Value.ToString());
            }

            return restRequest;
        }

        public static RestRequest AddParameterIfSet<T>(this RestRequest restRequest, string parameter, T? enumValue)
            where T : struct, Enum
        {
            if (enumValue != null)
            {
                return restRequest.AddQueryParameter(parameter, enumValue.Value.GetEnumStringRepresentation());
            }

            return restRequest;
        }

        public static RestRequest AddNotEncodedParameterIfSet(this RestRequest restRequest, string parameter, string value)
        {
            if (!value.IsNullOrEmpty())
            {
                return restRequest.AddQueryParameter(parameter, value, false);
            }

            return restRequest;
        }

        public static RestRequest AddParameterValues(this RestRequest restRequest, string parameter, ICollection<string> values)
        {
            if (values == null)
            {
                return restRequest;
            }

            if (values.Count > 0)
            {
                var parameterValues = string.Join(",", values);
                return restRequest.AddQueryParameter(parameter, parameterValues);
            }

            return restRequest;
        }

        public static RestRequest AddParameterValues(this RestRequest restRequest, string parameter, ICollection<int> values)
        {
            if (values == null)
            {
                return restRequest;
            }

            if (values.Count > 0)
            {
                var parameterValues = string.Join(",", values);
                return restRequest.AddQueryParameter(parameter, parameterValues);
            }

            return restRequest;
        }
    }
}
