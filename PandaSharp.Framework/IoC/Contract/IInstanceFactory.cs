using PandaSharp.Framework.IoC.Injections;

namespace PandaSharp.Framework.IoC.Contract
{
    internal interface IInstanceFactory
    {
        object CreateInstance(params InjectProperty[] injectedInformation);
    }
}