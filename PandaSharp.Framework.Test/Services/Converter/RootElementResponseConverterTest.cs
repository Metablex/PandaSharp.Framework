using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.Services.Converter;
using PandaSharp.Framework.Services.Response;
using Shouldly;

namespace PandaSharp.Framework.Test.Services.Converter
{
    [TestFixture]
    public sealed class RootElementResponseConverterTest
    {
        [Test]
        public void ConverterWriteTest()
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            var writer = new JsonTextWriter(streamWriter);

            var converter = new RootElementResponseConverter<TestListResponse, TestResponse>();
            converter.CanWrite.ShouldBeFalse();
            converter.WriteJson(writer, new TestListResponse(), new JsonSerializer());

            memoryStream.Length.ShouldBe(0);
        }

        [Test]
        public void ConverterReadTest()
        {
            var reader = CreateTestReader();

            var converter = new RootElementResponseConverter<TestListResponse, TestResponse>();
            converter.CanRead.ShouldBeTrue();
            var result = converter.ReadJson(reader, typeof(TestResponse), new TestListResponse(), false, new JsonSerializer());

            result.ShouldNotBeNull();
            result.Number.ShouldBe(42);
            result.Select(r => r.Name).ShouldBe(new[] { "READ", "WRITE" });
        }

        [Test]
        public void ConverterReadNoRootElementTest()
        {
            var reader = CreateTestReader();

            var converter = new RootElementResponseConverter<TestListNoRootResponse, TestResponse>();
            converter.CanRead.ShouldBeTrue();
            var result = converter.ReadJson(reader, typeof(TestResponse), new TestListNoRootResponse(), false, new JsonSerializer());

            result.ShouldNotBeNull();
            result.Select(r => r.Name).ShouldBe(new[] { "READ", "WRITE" });
        }

        private static JsonTextReader CreateTestReader()
        {
            const string Json = @"
            {
                ""plans"": {
                    ""number"":""42"",
                    ""plan"": [
                        {
						    ""name"": ""READ""
                        },
                        {
						    ""name"": ""WRITE""
                        },
                    ]
                }
            }";

            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(Json));
            var streamReader = new StreamReader(memoryStream);
            return new JsonTextReader(streamReader);
        }

        private sealed class TestResponse
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        [JsonRootElementPath("plans")]
        [JsonListContentPath("plans.plan.[*]")]
        private sealed class TestListResponse : ListResponseBase<TestResponse>
        {
            [JsonProperty("number")]
            public int Number { get; set; }
        }

        [JsonListContentPath("plans.plan.[*]")]
        private sealed class TestListNoRootResponse : ListResponseBase<TestResponse>
        {
        }
    }
}