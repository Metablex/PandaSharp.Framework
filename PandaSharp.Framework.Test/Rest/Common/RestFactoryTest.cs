using System;
using Moq;
using NUnit.Framework;
using PandaSharp.Framework.Rest.Common;
using PandaSharp.Framework.Rest.Contract;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;
using Shouldly;

namespace PandaSharp.Framework.Test.Rest.Common
{
    [TestFixture]
    public sealed class RestFactoryTest
    {
        [Test]
        public void CreateRequestTest()
        {
            var factory = new RestFactory(
                new Mock<IRestOptions>().Object,
                new Mock<IRestSerializer>().Object);

            var request = factory.CreateRequest("TestResource", Method.PUT);

            request.ShouldNotBeNull();
            request.Resource.ShouldBe("TestResource");
            request.Method.ShouldBe(Method.PUT);
        }

        [Test]
        public void CreateClientTest()
        {
            var restOptions = new Mock<IRestOptions>();
            restOptions
                .SetupGet(i => i.BaseUrl)
                .Returns("http://test.company.com");

            var authenticator = new Mock<IAuthenticator>();

            restOptions
                .Setup(i => i.Authenticator)
                .Returns(authenticator.Object);

            var serializer = new Mock<IRestSerializer>();
            serializer
                .SetupGet(i => i.DataFormat)
                .Returns(DataFormat.Json)
                .Verifiable();

            serializer
                .SetupGet(i => i.SupportedContentTypes)
                .Returns(new[] { "application/json" })
                .Verifiable();

            var factory = new RestFactory(restOptions.Object, serializer.Object);

            var client = factory.CreateClient();

            client.ShouldNotBeNull();
            client.Authenticator.ShouldBe(authenticator.Object);
            client.BaseUrl.ShouldBe(new Uri("http://test.company.com"));

            serializer.Verify();
        }
    }
}