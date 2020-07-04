using NUnit.Framework;
using System;

namespace HelloWorld.UnitTests
{
    public class IdGeneratorTest
    {
        [Test]
        public void ByDate_2020Y01M26DWithSomeGuid_TheFirst4ByteShouldBeDifferent()
        {
            var id = IdGenerator.ByDate(new DateTime(2020,01,26), Guid.Parse("be54bc45-f368-42ba-9c4c-b79c362382c8"));
            var expected = Guid.Parse("33f83f0b-f368-42ba-9c4c-b79c362382c8");
            Assert.AreEqual(expected, id);
        }

        [Test]
        public void ByDate_2456Y09M05D_MaxDate()
        {
            var date = new DateTime(2456, 09, 05, 23, 40, 07);
            
            var id = IdGenerator.ByDate(date, Guid.Parse("be54bc45-f368-42ba-9c4c-b79c362382c8"));
            var expected = Guid.Parse("ffffffff-f368-42ba-9c4c-b79c362382c8");

            Assert.AreEqual(expected, id);
        }

    }
}
