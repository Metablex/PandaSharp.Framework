using System;
using Moq;
using NUnit.Framework;
using PandaSharp.Framework.Utils;
using RestSharp;
using Shouldly;

namespace PandaSharp.Framework.Test.Utils
{
    [TestFixture]
    public sealed class RequestBaseExtensionsTest
    {
        [Test]
        public void GetErrorResponseMessageTest()
        {
            var requestMock = new Mock<IRestResponse>();
            requestMock
                .SetupGet(i => i.ErrorException)
                .Returns(new InvalidOperationException("ExpectionError"));

            var errorMessage = requestMock.Object.GetErrorResponseMessage();
            errorMessage.ShouldContain("ExpectionError");

            requestMock = new Mock<IRestResponse>();
            requestMock
                .SetupGet(i => i.ErrorMessage)
                .Returns("ErrorMessage");

            errorMessage = requestMock.Object.GetErrorResponseMessage();
            errorMessage.ShouldBe("ErrorMessage");

            requestMock = new Mock<IRestResponse>();
            requestMock
                .SetupGet(i => i.Content)
                .Returns(@"{""message"":""JsonError""}");

            errorMessage = requestMock.Object.GetErrorResponseMessage();
            errorMessage.ShouldBe("JsonError");
        }
    }
}