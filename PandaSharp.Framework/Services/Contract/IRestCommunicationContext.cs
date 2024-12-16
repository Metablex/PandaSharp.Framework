namespace PandaSharp.Framework.Services.Contract
{
    public interface IRestCommunicationContext
    {
        T GetContextParameter<T>(string parameterName);
    }
}
