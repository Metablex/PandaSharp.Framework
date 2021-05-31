using System;

namespace PandaSharp.Framework.IoC.Contract
{
    public interface IPandaContainerContext
    {
        Version GetCurrentApiVersion(IPandaContainer container);
    }
}