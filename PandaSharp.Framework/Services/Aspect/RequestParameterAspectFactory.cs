using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.IoC.Contract;

namespace PandaSharp.Framework.Services.Aspect
{
    internal sealed class RequestParameterAspectFactory : IRequestParameterAspectFactory
    {
        private readonly IPandaContainer _container;

        public RequestParameterAspectFactory(IPandaContainer container)
        {
            _container = container;
        }

        public IList<IRequestParameterAspect> GetParameterAspects(Type type)
        {
            return type
                .GetCustomAttributes(typeof(SupportsParameterAspectAttribute))
                .Cast<SupportsParameterAspectAttribute>()
                .Select(attribute => _container.Resolve(attribute.ParameterAspectType))
                .OfType<IRequestParameterAspect>()
                .ToList();
        }
    }
}