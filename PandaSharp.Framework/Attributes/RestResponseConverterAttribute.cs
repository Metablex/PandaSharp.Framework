using System;

namespace PandaSharp.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RestResponseConverterAttribute : Attribute
    {
        public Type RestResponseConverterType { get; }

        public RestResponseConverterAttribute(Type restResponseConverterType)
        {
            RestResponseConverterType = restResponseConverterType;
        }
    }
}