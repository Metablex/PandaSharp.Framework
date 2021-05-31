using Moq;
using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.IoC.Contract;
using PandaSharp.Framework.Services.Aspect;
using Shouldly;

namespace PandaSharp.Framework.Test.Services.Aspect
{
    [TestFixture]
    public sealed class RequestParameterAspectFactoryTest
    {
        [Test]
        public void GetParameterAspectsTest()
        {
            var containerMock = new Mock<IPandaContainer>(MockBehavior.Strict);
            containerMock
                .Setup(c => c.Resolve(typeof(IAspectA)))
                .Returns(new Mock<IAspectA>().Object);

            containerMock
                .Setup(c => c.Resolve(typeof(IAspectB)))
                .Returns(new Mock<IAspectB>().Object);

            var factory = new RequestParameterAspectFactory(containerMock.Object);
            var aspects = factory.GetParameterAspects(typeof(TestAspectClass));

            aspects.Count.ShouldBe(2);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        internal interface IAspectA : IRequestParameterAspect
        {
        }

        // ReSharper disable once MemberCanBePrivate.Global
        internal interface IAspectB : IRequestParameterAspect
        {
        }

        [SupportsParameterAspect(typeof(IAspectA))]
        [SupportsParameterAspect(typeof(IAspectB))]
        private class TestAspectClass
        {
        }
    }
}