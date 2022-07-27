using System;
using NUnit.Framework;
using PandaSharp.Framework.Attributes;
using PandaSharp.Framework.Utils;
using Shouldly;

namespace PandaSharp.Framework.Test.Utils
{
    [TestFixture]
    public sealed class EnumExtensionsTest
    {
        [Test]
        public void GetEnumStringRepresentationTest()
        {
            var memberStringA = TestEnum.TestMemberA.GetEnumStringRepresentation();
            memberStringA.ShouldBe("TestMeA");

            var memberStringB = TestEnum.TestMemberB.GetEnumStringRepresentation();
            memberStringB.ShouldBeNull();
        }

        [Test]
        public void GetEnumMemberTest()
        {
            var memberA = "TestMeA".GetEnumMember(typeof(TestEnum));
            memberA.ShouldBe(TestEnum.TestMemberA);
            
            var memberB = "TestMemberB".GetEnumMember(typeof(TestEnum));
            memberB.ShouldBeNull();

            var memberC = "Bla".GetEnumMember(typeof(string));
            memberC.ShouldBeNull();
        }

        private enum TestEnum
        {
            [StringRepresentation("TestMeA")]
            TestMemberA,

            TestMemberB
        }
    }
}