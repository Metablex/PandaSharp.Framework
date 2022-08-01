using System;
using PandaSharp.Framework.IoC.Injections;

namespace PandaSharp.Framework.IoC.Factory
{
    internal class MultipleInstanceFactory : InstanceFactoryBase
    {
        public MultipleInstanceFactory(Func<object> factoryMethod)
            : base(factoryMethod)
        {
        }

        public override object CreateInstance(params InjectProperty[] injectedInformation)
        {
            return ConstructObject(injectedInformation);
        }
    }
}