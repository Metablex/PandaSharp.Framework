using System;
using PandaSharp.Framework.Services.Request;

namespace PandaSharp.Framework.IoC.Contract
{
    public interface IRequestProviderRegistration<in T>
    {
        IRequestProviderRegistration<T> VersionSpecificRequest<TInstance>(Version version)
            where TInstance : T;

        IRequestProviderRegistration<T> LatestRequest<TInstance>()
            where TInstance : RestCommunicationBase, T;

        void Register(IPandaContainerContext context);
    }
}