using System;
using Moq;
using NUnit.Framework;
using PandaSharp.Framework.IoC;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Rest.Contract;
using PandaSharp.Framework.Services.Aspect;
using PandaSharp.Framework.Services.Request;
using RestSharp;
using Shouldly;

namespace PandaSharp.Framework.Test.IoC
{
    [TestFixture]
    public sealed class RequestProviderRegistrationTest
    {
        [Test]
        public void NoVersionSpecificRegistrationTest()
        {
            var containerMock = new Mock<IPandaContainer>();
            var contextMock = new Mock<IPandaContainerContext>();
            contextMock
                .Setup(i => i.GetCurrentApiVersion(It.IsAny<IPandaContainer>()))
                .Returns(() => new Version());

            new RequestProviderRegistration<ITestRequest>(containerMock.Object)
                .LatestRequest<TestRequestA>()
                .Register(contextMock.Object);

            containerMock.Verify(c => c.RegisterType<ITestRequest>(typeof(TestRequestA)), Times.Once);
        }

        [Test]
        public void DoubleLatestRequestRegistrationTest()
        {
            var containerMock = new Mock<IPandaContainer>();

            var registration = new RequestProviderRegistration<ITestRequest>(containerMock.Object)
                .LatestRequest<TestRequestA>();

            Should.Throw<InvalidOperationException>(() => registration.LatestRequest<TestRequestB>());
        }

        [Test]
        public void VersionSpecificRegistrationTest()
        {
            var containerMock = new Mock<IPandaContainer>();

            var currentVersion = new Version("1.0.0");
            var contextMock = new Mock<IPandaContainerContext>();
            contextMock
                .Setup(i => i.GetCurrentApiVersion(It.IsAny<IPandaContainer>()))
                .Returns(() => currentVersion);

            var registration = new RequestProviderRegistration<ITestRequest>(containerMock.Object)
                .VersionSpecificRequest<TestRequestA>(new Version("1.0.1"))
                .VersionSpecificRequest<TestRequestB>(new Version("1.1.1"))
                .LatestRequest<TestRequestC>();

            registration.Register(contextMock.Object);

            containerMock.Verify(c => c.RegisterType<ITestRequest>(typeof(TestRequestA)));
            containerMock.VerifyNoOtherCalls();

            currentVersion = new Version("1.0.6");
            registration.Register(contextMock.Object);

            containerMock.Verify(c => c.RegisterType<ITestRequest>(typeof(TestRequestB)));
            containerMock.VerifyNoOtherCalls();

            currentVersion = new Version("1.2.1");
            registration.Register(contextMock.Object);

            containerMock.Verify(c => c.RegisterType<ITestRequest>(typeof(TestRequestC)));
            containerMock.VerifyNoOtherCalls();

            currentVersion = new Version("1.1.1");
            registration.Register(contextMock.Object);

            containerMock.Verify(c => c.RegisterType<ITestRequest>(typeof(TestRequestB)));
            containerMock.VerifyNoOtherCalls();
        }

        private interface ITestRequest
        {
        }

        private class TestRequestBase : RestCommunicationBase, ITestRequest
        {
            protected TestRequestBase()
                : base(new Mock<IRestFactory>().Object, new Mock<IRequestParameterAspectFactory>().Object)
            {
            }

            protected override string GetResourcePath()
            {
                return "TestPath";
            }

            protected override Method GetRequestMethod()
            {
                return Method.GET;
            }
        }

        private class TestRequestA : TestRequestBase
        {
        }

        private class TestRequestB : TestRequestBase
        {
        }

        private class TestRequestC : TestRequestBase
        {
        }
    }
}