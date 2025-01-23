using NUnit.Framework;
using PandaSharp.Framework.Rest.Common;
using RestSharp;
using Shouldly;

namespace PandaSharp.Framework.Test.Rest.Common
{
    [TestFixture]
    public sealed class JsonRestSerializerTest
    {
        [Test]
        public void DataTypeCorrectTest()
        {
            var serializer = new JsonRestSerializer(new[] { "application/json" });
            serializer.DataFormat.ShouldBe(DataFormat.Json);
            serializer.AcceptedContentTypes.ShouldBe(new[] { "application/json" });

            serializer.ContentType.Value.ShouldBe("application/json");
            serializer.ContentType = "application/xml";
            serializer.ContentType.Value.ShouldBe("application/xml");
        }

        [Test]
        public void SerializeTest()
        {
            var testObject = new SerializationTest { TestProperty = "Test" };

            var serializer = new JsonRestSerializer(new[] { "application/json" });
            var serializedA = serializer.Serialize(testObject);
            var serializedB = serializer.Serialize(new JsonParameter("X", testObject));

            serializedA.ShouldBe(serializedB);
            serializedA.ShouldBe("""{"TestProperty":"Test"}""");

            var response = new RestResponse { Content = serializedA };

            var deserialized = serializer.Deserialize<SerializationTest>(response);
            deserialized.ShouldNotBeNull();
            deserialized.TestProperty.ShouldBe("Test");
        }

        private class SerializationTest
        {
            public string TestProperty { get; set; }
        }
    }
}
