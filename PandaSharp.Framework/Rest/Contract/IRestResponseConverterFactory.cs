using System;

namespace PandaSharp.Framework.Rest.Contract
{
    public interface IRestResponseConverterFactory
    {
        IRestResponseConverter CreateResponseConverter(Type type);
    }
}