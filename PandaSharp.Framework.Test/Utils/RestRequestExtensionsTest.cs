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
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterIfSet(TestParameter, (string)null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterIfSet(TestParameter, "Value");
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Value"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterIfSetDateTimeTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterIfSet(TestParameter, (DateTime?)null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterIfSet(TestParameter, new DateTime(2000, 1, 1, 12, 0, 0));
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "2000-01-01T12:00:00"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterIfSetIntegerTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterIfSet(TestParameter, (int?)null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterIfSet(TestParameter, 42);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "42"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterIfSetBooleanTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterIfSet(TestParameter, (bool?)null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterIfSet(TestParameter, true);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "True"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterIfSetEnumTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterIfSet(TestParameter, (TestEnum?)null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterIfSet<TestEnum>(TestParameter, TestEnum.Test);
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Hallo"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddNotEncodedParameterIfSetTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, string.Empty);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddNotEncodedParameterIfSet(TestParameter, "Value");
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Value", false), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterValuesStringTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterValues(TestParameter, (string[])null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, Array.Empty<string>());
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, new[] { "Test1", "Test2" });
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "Test1,Test2"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }

        [Test]
        public void AddParameterValuesIntegerTest()
        {
            var requestMock = new Mock<IRestRequest>();

            requestMock.Object.AddParameterValues(TestParameter, (int[])null);
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, Array.Empty<int>());
            requestMock.VerifyNoOtherCalls();

            requestMock.Object.AddParameterValues(TestParameter, new[] { 1, 2 });
            requestMock.Verify(r => r.AddQueryParameter(TestParameter, "1,2"), Times.Once);
            requestMock.VerifyNoOtherCalls();
        }
        
        private enum TestEnum
        {
            [StringRepresentation("Hallo")]
            Test
        }
    }
}