using Moq;
using NUnit.Framework;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Rest.Contract;
using PandaSharp.Framework.Utils;
using RestSharp.Authenticators;
using Shouldly;

namespace PandaSharp.Framework.Test.Utils
{
    [TestFixture]
    public sealed class PandaContainerExtensionsTest
    {
        [Test]
        public void RegisterWithBasicAuthenticationTest()
        {
            IRestOptions options = null; 
            var container = new Mock<IPandaContainer>();
            container
                .Setup(i => i.RegisterInstance(It.IsAny<IRestOptions>()))
                .Callback<IRestOptions>(i => options = i);
            
            container.Object.RegisterWithBasicAuthentication("someUrl", "user", "password");

            options.ShouldNotBeNull();
            options.BaseUrl.ShouldBe("someUrl");
            options.Authenticator.ShouldBeOfType<HttpBasicAuthenticator>();
        }

        [Test]
        public void RegisterWithOAuthAuthenticationTest()
        {
            IRestOptions options = null; 
            var container = new Mock<IPandaContainer>();
            container
                .Setup(i => i.RegisterInstance(It.IsAny<IRestOptions>()))
                .Callback<IRestOptions>(i => options = i);
            
            container.Object.RegisterWithOAuthAuthentication("someUrl", "consumerKey", "consumerSecret", "accessToken", "tokenSecret");
            
            options.ShouldNotBeNull();
            options.BaseUrl.ShouldBe("someUrl");
            options.Authenticator.ShouldBeOfType<OAuth1Authenticator>();
        }
    }
}