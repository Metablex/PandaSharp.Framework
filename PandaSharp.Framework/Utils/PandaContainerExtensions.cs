using System;
using System.Collections.Generic;
using System.Linq;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Rest.Common;
using PandaSharp.Framework.Rest.Contract;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;

namespace PandaSharp.Framework.Utils
{
    public static class PandaContainerExtensions
    {
        public static void RegisterPandaModules(this IPandaContainer container)
        {
            var containerModules = GetAllImplementationsOf<IPandaContainerModule>();
            foreach (var containerModule in containerModules)
            {
                containerModule.RegisterModule(container);
            }
        }

        public static void RegisterWithBasicAuthentication(this IPandaContainer container, string baseUrl, string userName, string password)
        {
            var authentication = new HttpBasicAuthenticator(userName, password);
            var options = new RestOptions(baseUrl, authentication);
            
            container.RegisterInstance<IRestOptions>(options);
        }

        public static void RegisterWithOAuthAuthentication(this IPandaContainer container, string baseUrl, string consumerKey, string consumerSecret, string oAuthAccessToken, string oAuthTokenSecret)
        {
            var authentication = OAuth1Authenticator.ForProtectedResource(
                consumerKey,
                consumerSecret,
                oAuthAccessToken,
                oAuthTokenSecret,
                OAuthSignatureMethod.RsaSha1);
            
            var options = new RestOptions(baseUrl, authentication);
            
            container.RegisterInstance<IRestOptions>(options);
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