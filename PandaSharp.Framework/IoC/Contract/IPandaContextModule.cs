namespace PandaSharp.Framework.IoC.Contract
{
    public interface IPandaContextModule
    {
        void RegisterModule(IPandaContainer container, IPandaContainerContext context);
    }
}