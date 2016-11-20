using HelloWorld.Buildstuff2016;
using NUnit.Framework;

namespace HelloWorld.UnitTests.Buildstuff2016
{
	[TestFixture]
	public class CardTest
	{
		[TestCase("cT", Suit.Club, 10)]
		[TestCase("h2", Suit.Heart, 2)]
		[TestCase("S7", Suit.Spade, 7)]
		public void Parse(string value, Suit expectedColor, int expectedValue)
		{
			var card = Card.Parse(value);
			Assert.AreEqual(expectedColor, card.Suit);
			Assert.AreEqual(expectedValue, card.Value);
		}

		[TestCase("cT", "♣10")]
		[TestCase("h2", "♠2")]
		[TestCase("S7", "♠7")]
		public void DebuggerDisplay(string value, string expected)
		{
			var card = Card.Parse(value);
			Assert.AreEqual(expected, card.DebuggerDisplay);
		}
	}
}
