using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.IoC.Factory;
using PandaSharp.Framework.IoC.Injections;

namespace PandaSharp.Framework.IoC
{
    public sealed class PandaContainer : IPandaContainer
    {
        private readonly Dictionary<Type, IInstanceFactory> _registeredFactories;

        public PandaContainer()
        {
            _registeredFactories = new Dictionary<Type, IInstanceFactory>();
        }

        public void RegisterType<T, TInstance>()
            where TInstance : T
        {
            RegisterMultipleInstance<T>(ResolveIntern<TInstance>);
        }

        public void RegisterType<T>(Type type)
        {
            RegisterMultipleInstance<T>(() => ResolveIntern(type));
        }

        public void RegisterType<T>(Func<T> customFactoryMethod)
        {
            RegisterMultipleInstance<T>(() => customFactoryMethod());
        }

        public void RegisterSingletonType<T, TInstance>()
            where TInstance : T
        {
            RegisterSingleInstance<T>(ResolveIntern<TInstance>);
        }

        public void RegisterSingletonType<T>(Func<T> customFactoryMethod)
        {
            RegisterSingleInstance<T>(() => customFactoryMethod());
        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterCreatedInstance(instance);
        }

        public T Resolve<T>(params InjectionBase[] injectedInformation)
        {
            return (T)ResolveInstance(typeof(T), injectedInformation);
        }

        public object Resolve(Type type, params InjectionBase[] injectedInformation)
        {
            return ResolveInstance(type, injectedInformation);
        }

        private void RegisterSingleInstance<T>(Func<object> factoryMethod)
        {
            if (_registeredFactories.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Registration for {typeof(T)} already found!");
            }

            _registeredFactories.Add(typeof(T), new SingleInstanceFactory(factoryMethod));
        }

        private void RegisterCreatedInstance<T>(T instance)
        {
            if (_registeredFactories.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Registration for {typeof(T)} already found!");
            }

            _registeredFactories.Add(typeof(T), new CreatedInstanceFactory(instance));
        }

        private void RegisterMultipleInstance<T>(Func<object> factoryMethod)
        {
            if (_registeredFactories.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Registration for {typeof(T)} already found!");
            }

            _registeredFactories.Add(typeof(T), new MultipleInstanceFactory(factoryMethod));
        }

        private object ResolveInstance(Type type, params InjectionBase[] injectedInformation)
        {
            if (_registeredFactories.TryGetValue(type, out var factory))
            {
                return factory.CreateInstance(injectedInformation);
            }

            throw new InvalidOperationException($"No registration for {type} found!");
        }

        private object ResolveIntern<T>()
        {
            return ResolveIntern(typeof(T));
        }

        private object ResolveIntern(Type type)
        {
            var publicConstructors = type
                .GetConstructors()
                .Where(c => c.IsPublic)
                .ToArray();

            if (publicConstructors.Length != 1)
            {
                throw new InvalidOperationException($"There can be only one public constructor for {type}");
            }

            var resolvedParameters = publicConstructors[0]
                .GetParameters()
                .Select(ResolveParameterInstance)
                .ToArray();

            return Activator.CreateInstance(type, resolvedParameters);
        }

        private object ResolveParameterInstance(ParameterInfo parameterInfo)
        {
            if (parameterInfo.ParameterType == typeof(IPandaContainer))
            {
                return this;
            }

            if (_registeredFactories.TryGetValue(parameterInfo.ParameterType, out var factory))
            {
                return factory.CreateInstance();
            }

            throw new InvalidOperationException($"There is no registration for {parameterInfo.ParameterType}");
        }
    }
}