using System;

namespace PandaSharp.Framework.Attributes
{
    public sealed class JsonRootElementPathAttribute : Attribute
    {
        public string RootElementPath { get; }

        public JsonRootElementPathAttribute(string rootElementPath)
        {
            RootElementPath = rootElementPath;
        }
    }
}