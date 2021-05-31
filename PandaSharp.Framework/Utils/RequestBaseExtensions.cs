using Newtonsoft.Json;
using RestSharp;

namespace PandaSharp.Framework.Utils
{
    public static class RequestBaseExtensions
    {
        public static string GetErrorResponseMessage(this IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                return response.ErrorException.ToString();
            }

            if (response.ErrorMessage != null)
            {
                return response.ErrorMessage;
            }

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            return errorResponse.Message;
        }

        private sealed class ErrorResponse
        {
            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}
