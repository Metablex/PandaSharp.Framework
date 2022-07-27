using System;
using Moq;
using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.Utils;
using RestSharp;

namespace PandaSharp.Framework.Test.Utils
{
    [TestFixture]
    public sealed class RestRequestExtensionsTest
    {
        private const string TestParameter = "Test";

        [Test]
        public void AddParameterIfSetStringTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(requestMock.Object);

            requestMock.Object.AddParameterIfSet(TestParameter, (string)null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryString), Times.Never);

            requestMock.Object.AddParameterIfSet(TestParameter, "Value");
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Value"), Times.Once);
        }

        [Test]
        public void AddParameterIfSetDateTimeTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(requestMock.Object);

            requestMock.Object.AddParameterIfSet(TestParameter, (DateTime?)null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryString), Times.Never);

            var dateTime = new DateTime(2000, 1, 1, 12, 0, 0);
            requestMock.Object.AddParameterIfSet(TestParameter, dateTime);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "2000-01-01T12:00:00"), Times.Once);
        }

        [Test]
        public void AddParameterIfSetIntegerTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(requestMock.Object);

            requestMock.Object.AddParameterIfSet(TestParameter, (int?)null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryString), Times.Never);

            requestMock.Object.AddParameterIfSet(TestParameter, 42);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "42"), Times.Once);
        }

        [Test]
        public void AddParameterIfSetBooleanTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(requestMock.Object);

            requestMock.Object.AddParameterIfSet(TestParameter, (bool?)null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryString), Times.Never);

            requestMock.Object.AddParameterIfSet(TestParameter, true);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "True"), Times.Once);
        }

        [Test]
        public void AddParameterIfSetEnumTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(requestMock.Object);

            requestMock.Object.AddParameterIfSet(TestParameter, (TestEnum?)null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryString), Times.Never);

            requestMock.Object.AddParameterIfSet<TestEnum>(TestParameter, TestEnum.Test);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Hallo"), Times.Once);
        }

        [Test]
        public void AddNotEncodedParameterIfSetTest()
        {
            var requestMock = new Mock<IRestRequest>(MockBehavior.Strict);
            requestMock
                .Setup(i => i.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(requestMock.Object);

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, null);
            requestMock.Verify(r => r.AddParameter(TestParameter, null, ParameterType.QueryStringWithoutEncode), Times.Never);

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, "Value");
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Value", false), Times.Once);
        }

        [Test]
        public void AddParameterValuesStringTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterValues(TestParameter, (string[])null);
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, Array.Empty<string>());
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Setup(i => i.AddParameter(TestParameter, new[] { "Test1", "Test2" }));
            requestMock.Object.AddParameterValues(TestParameter, new[] { "Test1", "Test2" });
            requestMock.Verify(r => r.AddParameter(TestParameter, "Test1,Test2"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterValuesIntegerTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterValues(TestParameter, (int[])null);
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, Array.Empty<int>());
            requestMock.Verify(r => r.AddParameter(TestParameter, It.IsAny<object>()), Times.Never);
            requestMock.VerifyNoOtherCalls();

            requestMock.Setup(i => i.AddParameter(TestParameter, new[] { 1, 2 }));
            requestMock.Object.AddParameterValues(TestParameter, new[] { 1, 2 });
            requestMock.Verify(r => r.AddParameter(TestParameter, "1,2"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }
        
        private enum TestEnum
        {
            [StringRepresentation("Hallo")]
            Test
        }
    }
}