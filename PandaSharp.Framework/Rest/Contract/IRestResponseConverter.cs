using RestSharp;

namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestResponseConverter
    {
        T ConvertRestResponse<T>(IRestResponse<T> response);
    }
}