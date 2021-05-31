using System;
using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.IoC;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.IoC.Injections;
using Shouldly;

namespace PandaSharp.Framework.Test.IoC
{
    [TestFixture]
    public sealed class PandaContainerTest
    {
        [Test]
        public void RegistrationErrorHandlingTest()
        {
            var container = new PandaContainer();
            Should.Throw<InvalidOperationException>(() => container.Resolve<IRegistrationTest>());

            container.RegisterType<NoPublicConstructorTest, NoPublicConstructorTest>();

            Should.Throw<InvalidOperationException>(() => container.Resolve<NoPublicConstructorTest>());
            Should.Throw<InvalidOperationException>(() => container.RegisterType<NoPublicConstructorTest, NoPublicConstructorTest>());
            Should.Throw<InvalidOperationException>(() => container.RegisterSingletonType<NoPublicConstructorTest, NoPublicConstructorTest>());

            container.RegisterType<ParameterizedConstructorTest, ParameterizedConstructorTest>();

            Should.Throw<InvalidOperationException>(() => container.Resolve<ParameterizedConstructorTest>());
        }

        [Test]
        public void MultipleInstanceRegistrationTest()
        {
            var container = new PandaContainer();
            container.RegisterType<IRegistrationTest, RegistrationTest>();

            var instance = container.Resolve<IRegistrationTest>();
            instance.Name.ShouldBeNull();
            instance.Value.ShouldBeNull();

            var anotherInstance = container.Resolve<IRegistrationTest>(
                new InjectProperty("Test", "Name"),
                new InjectProperty("Value", "Value"));

            anotherInstance.ShouldNotBeSameAs(instance);
            anotherInstance.Name.ShouldBe("Name");
            anotherInstance.Value.ShouldBe("Value");
        }

        [Test]
        public void MultipleInstanceCustomFactoryRegistrationTest()
        {
            var container = new PandaContainer();
            container.RegisterType<IRegistrationTest>(() => new RegistrationTest { Name = "CustomName", Value = "CustomValue" });

            var instance = container.Resolve<IRegistrationTest>();
            instance.Name.ShouldBe("CustomName");
            instance.Value.ShouldBe("CustomValue");
        }

        [Test]
        public void MultipleInstanceTypeOfRegistrationTest()
        {
            var container = new PandaContainer();
            container.RegisterType<IRegistrationTest>(typeof(RegistrationTest));

            var instance = container
                .Resolve(typeof(IRegistrationTest))
                .ShouldBeOfType<RegistrationTest>();

            instance.Name.ShouldBeNull();
            instance.Value.ShouldBeNull();
        }

        [Test]
        public void SingleInstanceRegistrationTest()
        {
            var container = new PandaContainer();
            container.RegisterSingletonType<IRegistrationTest, RegistrationTest>();

            var instance = container.Resolve<IRegistrationTest>();
            instance.Name.ShouldBeNull();
            instance.Value.ShouldBeNull();

            var anotherInstance = container.Resolve<IRegistrationTest>(
                new InjectProperty("Test", "Name"),
                new InjectProperty("Value", "Value"));

            anotherInstance.ShouldBeSameAs(instance);
        }

        [Test]
        public void SingleInstanceCustomFactoryRegistrationTest()
        {
            var container = new PandaContainer();
            container.RegisterSingletonType<IRegistrationTest>(() => new RegistrationTest { Name = "CustomName", Value = "CustomValue" });

            var instance = container.Resolve<IRegistrationTest>();
            instance.Name.ShouldBe("CustomName");
            instance.Value.ShouldBe("CustomValue");

            var anotherInstance = container.Resolve<IRegistrationTest>();
            anotherInstance.ShouldBeSameAs(instance);
        }

        [Test]
        public void ParameterizedConstructorRegistrationTest()
        {
            var container = new PandaContainer();
            container.RegisterType<IRegistrationTest>(() => new RegistrationTest { Name = "42" });
            container.RegisterType<ParameterizedConstructorTest, ParameterizedConstructorTest>();

            var instance = container.Resolve<ParameterizedConstructorTest>();
            instance.Container.ShouldBeSameAs(container);
            instance.TestProperty.ShouldNotBeNull();
            instance.TestProperty.Name.ShouldBe("42");
        }

        private interface IRegistrationTest
        {
            string Name { get; }

            string Value { get; }
        }

        private sealed class RegistrationTest : IRegistrationTest
        {
            [InjectedProperty("Test")]
            public string Name { get; set; }

            [InjectedProperty]
            public string Value { get; set; }
        }

        private sealed class NoPublicConstructorTest
        {
            private NoPublicConstructorTest()
            {
            }
        }

        private sealed class ParameterizedConstructorTest
        {
            public IRegistrationTest TestProperty { get; }

            public IPandaContainer Container { get; }

            public ParameterizedConstructorTest(IPandaContainer container, IRegistrationTest registrationTest)
            {
                Container = container;
                TestProperty = registrationTest;
            }
        }
    }
}