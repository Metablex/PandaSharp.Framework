using System;
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
            var response = new RestResponse
            {
                ErrorException = new InvalidOperationException("ExceptionError")
            };

            var errorMessage = response.GetErrorResponseMessage();
            errorMessage.ShouldContain("ExceptionError");

            response = new RestResponse
            {
                ErrorMessage = "ErrorMessage"
            };

            errorMessage = response.GetErrorResponseMessage();
            errorMessage.ShouldBe("ErrorMessage");

            response = new RestResponse
            {
                Content = """{"message":"JsonError"}"""
            };

            errorMessage = response.GetErrorResponseMessage();
            errorMessage.ShouldBe("JsonError");
        }
    }
}
