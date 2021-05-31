namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestOptions
    {
        string BaseUrl { get; set; }

        IRestAuthentication Authentication { get; }
    }
}