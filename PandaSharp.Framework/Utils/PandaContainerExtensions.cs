using System;
using System.Collections.Generic;
using System.Linq;
using PandaSharp.Framework.IoC;
using PandaSharp.Framework.IoC.Contract;

namespace PandaSharp.Framework.Utils
{
    public static class PandaContainerExtensions
    {
        public static void RegisterPandaModules(
            this IPandaContainer container,
            IPandaContainerContext context,
            Action onAfterCoreModulesRegistered = null)
        {
            var coreModules = GetAllImplementationsOf<IPandaCoreModule>();
            foreach (var coreModule in coreModules)
            {
                coreModule.RegisterModule(container);
            }

            onAfterCoreModulesRegistered?.Invoke();

            var contextBasedModules = GetAllImplementationsOf<IPandaContextModule>();
            foreach (var contextBasedModule in contextBasedModules)
            {
                contextBasedModule.RegisterModule(container, context);
            }
        }

        public static IRequestProviderRegistration<T> RequestRegistrationFor<T>(this IPandaContainer container)
        {
            return new RequestProviderRegistration<T>(container);
        }

        private static IEnumerable<T> GetAllImplementationsOf<T>()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(i => i.GetTypes())
                .Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(T)))
                .Select(Activator.CreateInstance)
                .OfType<T>();
        }
    }
}