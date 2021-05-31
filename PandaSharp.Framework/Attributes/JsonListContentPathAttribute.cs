using System;

namespace PandaSharp.Framework.Attributes
{
    public sealed class JsonListContentPathAttribute : Attribute
    {
        public string ListContentPath { get; }

        public JsonListContentPathAttribute(string listContentPath)
        {
            ListContentPath = listContentPath;
        }
    }
}