using System;
using System.Collections.Generic;
using System.Linq;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Services.Request;

namespace PandaSharp.Framework.IoC
{
    internal sealed class RequestProviderRegistration<T> : IRequestProviderRegistration<T>
    {
        private readonly IPandaContainer _container;
        private readonly List<VersionSpecificRequestType> _versionSpecificRequestTypes;
        private Type _latestRequestType;

        public RequestProviderRegistration(IPandaContainer container)
        {
            _container = container;
            _versionSpecificRequestTypes = new List<VersionSpecificRequestType>();
        }

        public IRequestProviderRegistration<T> VersionSpecificRequest<TInstance>(Version version)
            where TInstance : T
        {
            _versionSpecificRequestTypes.Add(
                new VersionSpecificRequestType
                {
                    VersionUpTo = version,
                    RequestType = typeof(TInstance)
                });

            return this;
        }

        public IRequestProviderRegistration<T> LatestRequest<TInstance>()
            where TInstance : RestCommunicationBase, T
        {
            if (_latestRequestType != null)
            {
                throw new InvalidOperationException($"Type of latest request for {typeof(T).Name} was already specified");
            }

            _latestRequestType = typeof(TInstance);
            return this;
        }

        public void Register(IPandaContainerContext context)
        {
            var versionSpecificRequestType = GetVersionSpecificRequestType(context);

            var requestTypeToRegister = versionSpecificRequestType != null
                ? versionSpecificRequestType.RequestType
                : _latestRequestType;

            _container.RegisterType<T>(requestTypeToRegister);
        }

        private VersionSpecificRequestType GetVersionSpecificRequestType(IPandaContainerContext context)
        {
            var currentVersion = context.GetCurrentApiVersion(_container);
            if (currentVersion == null)
            {
                return null;
            }

            return _versionSpecificRequestTypes
                .Where(types => types.VersionUpTo >= currentVersion)
                .OrderBy(types => types.VersionUpTo)
                .FirstOrDefault();
        }

        private class VersionSpecificRequestType
        {
            public Version VersionUpTo { get; set; }

            public Type RequestType { get; set; }
        }
    }
}