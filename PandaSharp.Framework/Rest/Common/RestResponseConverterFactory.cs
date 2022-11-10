using System;
using System.Reflection;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.Rest.Contract;

namespace PandaSharp.Framework.Rest.Common
{
    internal sealed class RestResponseConverterFactory : IRestResponseConverterFactory
    {
        public IRestResponseConverter CreateResponseConverter(Type type)
        {
            var attribute = type.GetCustomAttribute<RestResponseConverterAttribute>();
            if (attribute == null)
            {
                return new DefaultRestResponseConverter();
            }

            return (IRestResponseConverter)Activator.CreateInstance(attribute.RestResponseConverterType);
        }
    }
}