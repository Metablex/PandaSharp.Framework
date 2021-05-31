using NUnit.Framework;
using PandaSharp.Framework.Rest.Common;
using Shouldly;

namespace PandaSharp.Framework.Test.Rest.Common
{
    [TestFixture]
    public sealed class RestOptionsTest
    {
        private const string BambooTestAddress = "http://test.bamboo.com/rest/api/latest";

        [Test]
        public void BaseUrlAdjustementTest()
        {
            var options = new RestOptions();
            options.BaseUrl.ShouldBeNull();

            options.BaseUrl = BambooTestAddress;
            options.BaseUrl.ShouldBe(BambooTestAddress);

            options.BaseUrl = "http://test.bamboo.com";
            options.BaseUrl.ShouldBe(BambooTestAddress);
        }
    }
}