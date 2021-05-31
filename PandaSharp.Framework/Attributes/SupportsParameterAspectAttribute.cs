using System;

namespace PandaSharp.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class SupportsParameterAspectAttribute : Attribute
    {
        public Type ParameterAspectType { get; }

        public SupportsParameterAspectAttribute(Type parameterAspectType)
        {
            ParameterAspectType = parameterAspectType;
        }
    }
}