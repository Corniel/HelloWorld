using NUnit.Framework;
using System;

namespace HelloWorld.UnitTests
{
	[TestFixture]
	public class Iso8601WeekDateTest
	{
		[Test]
		public void Ctor_Y2009M12D18_Y2009W51D5()
		{
			var act = new Iso8601WeekDate(new DateTime(2009, 12, 18));

			Assert.AreEqual(2009, act.Year);
			Assert.AreEqual(51, act.Week);
			Assert.AreEqual(5, act.Day);
		}
	}
}
