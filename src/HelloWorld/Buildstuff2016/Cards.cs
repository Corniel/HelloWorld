using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HelloWorld.Buildstuff2016
{
	/// <summary>An implementation of a set of cards.</summary>
	/// <remarks>
	/// It really depends on the kind of things you want to do if creating a
	/// <see cref="List{Card}"/> is smart thing to do. So, this one is just for inspiration.
	/// </remarks>
	public class Cards: List<Card>
	{
		private static readonly Random rnd = new Random();
		public static readonly ReadOnlyCollection<Card> Deck = new ReadOnlyCollection<Card>(GetDeck());

		private static IList<Card> GetDeck()
		{
			var list = new List<Card>();
			var colors = new []{ Suit.Club, Suit.Heart, Suit.Spade, Suit.Diamond };

			foreach(var color in colors)
			{
				for (var val = 2; val <= 14; val++)
				{
					list.Add(new Card((int)color, val));
				}
			}

			return list;
		}

		public IEnumerable<Card> Shuffle(int take)
		{
			return this.OrderBy(c => rnd.Next()).Take(take);
		}
	}
}
