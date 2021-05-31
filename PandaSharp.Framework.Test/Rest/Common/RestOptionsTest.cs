using NUnit.Framework;
using PandaSharp.Framework.Rest.Common;
using Shouldly;

namespace PandaSharp.Framework.Test.Rest.Common
{
    [TestFixture]
    public sealed class RestOptionsTest
    {
        private const string TestAddress = "http://test.company.com/rest/api/latest";

        [Test]
        public void BaseUrlAdjustementTest()
        {
            var options = new RestOptions();
            options.BaseUrl.ShouldBeNull();

            options.BaseUrl = TestAddress;
            options.BaseUrl.ShouldBe(TestAddress);

            options.BaseUrl = "http://test.company.com";
            options.BaseUrl.ShouldBe(TestAddress);
        }
    }
}