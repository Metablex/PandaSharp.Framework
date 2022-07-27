using NUnit.Framework;
using PandaSharp.Framework.IoC.Factory;
using Shouldly;

namespace PandaSharp.Framework.Test.IoC.Factory
{
    [TestFixture]
    public sealed class CreatedInstanceFactoryTest
    {
        [Test]
        public void CreateInstanceTest()
        {
            var instance = new object();
            var factory = new CreatedInstanceFactory(instance);
            
            factory.CreateInstance().ShouldBe(instance);
        }
    }
}