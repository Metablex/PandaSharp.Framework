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

        T Resolve<T>(params InjectionBase[] injectedInformation);

        object Resolve(Type type, params InjectionBase[] injectedInformation);
    }
}