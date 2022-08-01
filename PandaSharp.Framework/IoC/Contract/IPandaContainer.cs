using System;
using PandaSharp.Framework.IoC.Injections;

namespace PandaSharp.Framework.IoC.Contract
{
    public interface IPandaContainer
    {
        void RegisterType<T, TInstance>() where TInstance : T;

        void RegisterType<T>(Type type);

        void RegisterType<T>(Func<T> customFactoryMethod);

        void RegisterSingletonType<T, TInstance>() where TInstance : T;

        void RegisterSingletonType<T>(Func<T> customFactoryMethod);

        void RegisterInstance<T>(T instance);

        T Resolve<T>(params InjectProperty[] injectedInformation);

        object Resolve(Type type, params InjectProperty[] injectedInformation);
    }
}