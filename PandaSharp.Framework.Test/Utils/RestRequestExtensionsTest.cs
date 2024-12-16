using System;
using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.Utils;
using RestSharp;
using Shouldly;

namespace PandaSharp.Framework.Test.Utils
{
    [TestFixture]
    public sealed class RestRequestExtensionsTest
    {
        private const string TestParameter = "Test";

        [Test]
        public void AddParameterIfSetStringTest()
        {
            var request = new RestRequest();

            request.AddParameterIfSet(TestParameter, (string)null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterIfSet(TestParameter, "Value");

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("Value");
        }

        [Test]
        public void AddParameterIfSetDateTimeTest()
        {
            var request = new RestRequest();

            request.AddParameterIfSet(TestParameter, (DateTime?)null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterIfSet(TestParameter, new DateTime(2000, 1, 1, 12, 0, 0));

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("2000-01-01T12:00:00");
        }

        [Test]
        public void AddParameterIfSetIntegerTest()
        {
            var request = new RestRequest();

            request.AddParameterIfSet(TestParameter, (int?)null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterIfSet(TestParameter, 42);

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("42");
        }

        [Test]
        public void AddParameterIfSetBooleanTest()
        {
            var request = new RestRequest();

            request.AddParameterIfSet(TestParameter, (bool?)null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterIfSet(TestParameter, true);

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("True");
        }

        [Test]
        public void AddParameterIfSetEnumTest()
        {
            var request = new RestRequest();

            request.AddParameterIfSet(TestParameter, (TestEnum?)null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterIfSet<TestEnum>(TestParameter, TestEnum.Test);

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("Hallo");
        }

        [Test]
        public void AddNotEncodedParameterIfSetTest()
        {
            var request = new RestRequest();

            request.AddNotEncodedParameterIfSet(TestParameter, null);
            request.Parameters.ShouldBeEmpty();

            request.AddNotEncodedParameterIfSet(TestParameter, string.Empty);
            request.Parameters.ShouldBeEmpty();

            request.AddNotEncodedParameterIfSet(TestParameter, "Value");

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("Value");
        }

        [Test]
        public void AddParameterValuesStringTest()
        {
            var request = new RestRequest();

            request.AddParameterValues(TestParameter, (string[])null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterValues(TestParameter, Array.Empty<string>());
            request.Parameters.ShouldBeEmpty();

            request.AddParameterValues(TestParameter, new[] { "Test1", "Test2" });

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("Test1,Test2");
        }

        [Test]
        public void AddParameterValuesIntegerTest()
        {
            var request = new RestRequest();

            request.AddParameterValues(TestParameter, (int[])null);
            request.Parameters.ShouldBeEmpty();

            request.AddParameterValues(TestParameter, Array.Empty<int>());
            request.Parameters.ShouldBeEmpty();

            request.AddParameterValues(TestParameter, new[] { 1, 2 });

            var actualParameter = request.Parameters.TryFind(TestParameter);
            actualParameter.ShouldNotBeNull();
            actualParameter.Value.ShouldBe("1,2");
        }

        private enum TestEnum
        {
            [StringRepresentation("Hallo")]
            Test
        }
    }
}
