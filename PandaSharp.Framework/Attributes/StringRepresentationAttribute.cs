using System;

namespace PandaSharp.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringRepresentationAttribute : Attribute
    {
        public string AsString { get; }

        public StringRepresentationAttribute(string stringRepresentation)
        {
            AsString = stringRepresentation;
        }
    }
}
