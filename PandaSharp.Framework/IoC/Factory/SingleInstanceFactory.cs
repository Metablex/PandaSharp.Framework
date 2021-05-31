using System;
using PandaSharp.Framework.IoC.Injections;

namespace PandaSharp.Framework.IoC.Factory
{
    internal class SingleInstanceFactory : InstanceFactoryBase
    {
        private object _instance;

        public SingleInstanceFactory(Func<object> factoryMethod)
            : base(factoryMethod)
        {
        }

        public override object CreateInstance(params InjectionBase[] injectedInformation)
        {
            return _instance ??= ConstructObject(injectedInformation);
        }
    }
}