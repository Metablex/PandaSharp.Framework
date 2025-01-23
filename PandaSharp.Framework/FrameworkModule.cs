using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Rest.Common;
using PandaSharp.Framework.Rest.Contract;
using PandaSharp.Framework.Services.Aspect;

namespace PandaSharp.Framework
{
    internal sealed class FrameworkModule : IPandaContainerModule
    {
        public void RegisterModule(IPandaContainer container)
        {
            container.RegisterType<IRequestParameterAspectFactory, RequestParameterAspectFactory>();
            container.RegisterSingletonType<IRestResponseConverterFactory, RestResponseConverterFactory>();
        }
    }
}
