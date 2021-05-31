using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.IoC.Factory;
using PandaSharp.Framework.IoC.Injections;
using Shouldly;

namespace PandaSharp.Framework.Test.IoC.Factory
{
    [TestFixture]
    public sealed class SingleInstanceFactoryTest
    {
        [Test]
        public void InstanceCreationTest()
        {
            var factory = new SingleInstanceFactory(() => new object());

            var instanceA = factory.CreateInstance();
            var instanceB = factory.CreateInstance();

            instanceA.ShouldBeSameAs(instanceB);
        }

        [Test]
        public void InstanceInjectionParameterTest()
        {
            var factory = new SingleInstanceFactory(() => new FactoryTest());

            var instance = (FactoryTest)factory.CreateInstance(
                new InjectProperty("TestProperty", "abc"),
                new InjectProperty("Test42", "xyz"),
                new InjectProperty("RandomProperty", 42));

            instance.TestProperty.ShouldBe("abc");
            instance.TestPropertyWithName.ShouldBe("xyz");
            instance.RandomProperty.ShouldNotBe(42);

            instance = (FactoryTest)factory.CreateInstance(
                new InjectProperty("TestPropertyWithName", "xyz"));

            instance.TestProperty.ShouldBe("abc");
            instance.TestPropertyWithName.ShouldBe("xyz");
            instance.RandomProperty.ShouldNotBe(42);
        }

        private class FactoryTest
        {
            [InjectedProperty]
            public string TestProperty { get; set; }

            [InjectedProperty("Test42")]
            public string TestPropertyWithName { get; set; }

            public int RandomProperty { get; set; }
        }
    }
}