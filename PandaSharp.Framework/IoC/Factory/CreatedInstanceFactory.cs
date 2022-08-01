using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.IoC.Injections;

namespace PandaSharp.Framework.IoC.Factory
{
    internal sealed class CreatedInstanceFactory : IInstanceFactory
    {
        private readonly object _instance;

        public CreatedInstanceFactory(object instance)
        {
            _instance = instance;
        }

        public object CreateInstance(params InjectProperty[] injectedInformation)
        {
            return _instance;
        }
    }
}