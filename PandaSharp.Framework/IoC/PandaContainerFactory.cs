using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Rest.Contract;
using PandaSharp.Framework.Utils;

namespace PandaSharp.Framework.IoC
{
    public static class PandaContainerFactory
    {
        public static IPandaContainer CreateAndRegisterContainerWithOAuth(
            IPandaContainerContext context,
            string baseUrl,
            string consumerKey,
            string consumerSecret,
            string oAuthAccessToken,
            string oAuthTokenSecret)
        {
            var container = new PandaContainer();

            container.RegisterPandaModules(
                context,
                () => AuthenticateWithOAuth(container, baseUrl, consumerKey, consumerSecret, oAuthAccessToken, oAuthTokenSecret));

            return container;
        }

        public static IPandaContainer CreateAndRegisterContainerWithCredentials(
            IPandaContainerContext context,
            string baseUrl,
            string userName,
            string password)
        {
            var container = new PandaContainer();

            container.RegisterPandaModules(
                context,
                () => AuthenticateWithCredentials(container, baseUrl, userName, password));

            return container;
        }

        private static void AuthenticateWithCredentials(IPandaContainer container, string baseUrl, string userName, string password)
        {
            var options = container.Resolve<IRestOptions>();
            options.BaseUrl = baseUrl;
            options.Authentication.UseBasic(userName, password);
        }

        private static void AuthenticateWithOAuth(
            IPandaContainer container,
            string baseUrl,
            string consumerKey,
            string consumerSecret,
            string oAuthAccessToken,
            string oAuthTokenSecret)
        {
            var options = container.Resolve<IRestOptions>();
            options.BaseUrl = baseUrl;
            options.Authentication.UseOAuth(consumerKey, consumerSecret, oAuthAccessToken, oAuthTokenSecret);
        }
    }
}