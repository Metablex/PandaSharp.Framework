using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Rest.Common;
using PandaSharp.Framework.Rest.Contract;
using RestSharp.Serialization;

namespace PandaSharp.Framework.Rest
{
    internal sealed class RestModule : IPandaCoreModule
    {
        public void RegisterModule(IPandaContainer container)
        {
            container.RegisterSingletonType<IRestOptions, RestOptions>();
            container.RegisterSingletonType<IRestSerializer, RestRequestSerializer>();
            container.RegisterSingletonType<IRestFactory, RestFactory>();
        }
    }
}
