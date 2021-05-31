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

            var memberStringB = TestFlagsEnum.TestMemberA.GetEnumStringRepresentation();
            memberStringB.ShouldBeNull();
        }

        [Test]
        public void AddEnumMemberTest()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                var result = (TestEnum?)TestEnum.TestMemberA;
                result.AddEnumMember(TestEnum.TestMemberB);
            });

            var flagsResult = (TestFlagsEnum?)TestFlagsEnum.TestMemberA;

            flagsResult.AddEnumMember(TestFlagsEnum.TestMemberB);
            flagsResult.ShouldBe(TestFlagsEnum.TestMemberA | TestFlagsEnum.TestMemberB);

            flagsResult.AddEnumMember(TestFlagsEnum.TestMemberC);
            flagsResult.ShouldBe(TestFlagsEnum.TestMemberA | TestFlagsEnum.TestMemberB | TestFlagsEnum.TestMemberC);

            flagsResult.AddEnumMember(TestFlagsEnum.TestMemberA);
            flagsResult.ShouldBe(TestFlagsEnum.TestMemberA | TestFlagsEnum.TestMemberB | TestFlagsEnum.TestMemberC);

            var nullFlagsResult = (TestFlagsEnum?)null;
            nullFlagsResult.AddEnumMember(TestFlagsEnum.TestMemberB);
            nullFlagsResult.ShouldBe(TestFlagsEnum.TestMemberB);
        }

        private enum TestEnum
        {
            [StringRepresentation("TestMeA")]
            TestMemberA,

            [StringRepresentation("TestMeB")]
            TestMemberB
        }

        [Flags]
        private enum TestFlagsEnum
        {
            TestMemberA = 1 << 0,

            TestMemberB = 1 << 1,

            TestMemberC = 1 << 2
        }
    }
}