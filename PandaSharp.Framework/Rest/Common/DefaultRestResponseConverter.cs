using PandaSharp.Framework.Rest.Contract;
using RestSharp;

namespace PandaSharp.Framework.Rest.Common
{
    internal sealed class DefaultRestResponseConverter : IRestResponseConverter
    {
        public T ConvertRestResponse<T>(RestResponse<T> response)
        {
            return response.Data;
        }
    }
}
