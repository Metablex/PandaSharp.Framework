using System.Linq;
using Moq;
using NUnit.Framework;
using PandaSharp.Framework.Utils;
using RestSharp;

namespace PandaSharp.Framework.Test.Utils
{
    [TestFixture]
    public sealed class RestRequestExtensionsTest
    {
        private const string TestParameter = "Test";

        [Test]
        public void AddParameterIfSetTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddParameter(TestParameter, It.IsAny<object>(), ParameterType.QueryString))
                .Returns(requestMock.Object);

            requestMock.Object.AddParameterIfSet(TestParameter, null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryString), Times.Never);

            requestMock.Object.AddParameterIfSet(TestParameter, "Value");
            requestMock.Verify(r => r.AddParameter(TestParameter, "Value", ParameterType.QueryString), Times.Once);
        }

        [Test]
        public void AddNotEncodedParameterIfSetTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddParameter(TestParameter, It.IsAny<object>(), ParameterType.QueryStringWithoutEncode))
                .Returns(requestMock.Object);

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryStringWithoutEncode), Times.Never);

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, "Value");
            requestMock.Verify(r => r.AddParameter(TestParameter, "Value", ParameterType.QueryStringWithoutEncode), Times.Once);
        }

        [Test]
        public void AddParameterValuesTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterValues(TestParameter, null);
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, Enumerable.Empty<string>());
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, new[] { null, "" });
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Setup(i => i.AddParameter(TestParameter, new[] { "Test1", "Test2" }));
            requestMock.Object.AddParameterValues(TestParameter, new[] { "Test1", "Test2" });
            requestMock.Verify(r => r.AddParameter(TestParameter, "Test1,Test2"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }
    }
}