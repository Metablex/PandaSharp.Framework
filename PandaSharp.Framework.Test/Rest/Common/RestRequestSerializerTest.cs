using Moq;
using NUnit.Framework;
using PandaSharp.Framework.Rest.Common;
using RestSharp;
using Shouldly;

namespace PandaSharp.Framework.Test.Rest.Common
{
    [TestFixture]
    public sealed class RestRequestSerializerTest
    {
        [Test]
        public void DataTypeCorrectTest()
        {
            var serializer = new RestRequestSerializer();
            serializer.DataFormat.ShouldBe(DataFormat.Json);
            serializer.SupportedContentTypes.ShouldContain("application/json");

            serializer.ContentType.ShouldBe("application/json");
            serializer.ContentType = "application/xml";
            serializer.ContentType.ShouldBe("application/xml");
        }

        [Test]
        public void SerializeTest()
        {
            var testObject = new SerializationTest { TestProperty = "Test" };

            var serializer = new RestRequestSerializer();
            var serializedA = serializer.Serialize(testObject);
            var serializedB = serializer.Serialize(new Parameter("X", testObject, ParameterType.QueryString));

            serializedA.ShouldBe(serializedB);
            serializedA.ShouldBe(@"{""TestProperty"":""Test""}");

            var responseMock = new Mock<IRestResponse>();
            responseMock
                .SetupGet(i => i.Content)
                .Returns(serializedA);

            var deseriaized = serializer.Deserialize<SerializationTest>(responseMock.Object);
            deseriaized.ShouldNotBeNull();
            deseriaized.TestProperty.ShouldBe("Test");
        }

        private class SerializationTest
        {
            public string TestProperty { get; set; }
        }
    }
}