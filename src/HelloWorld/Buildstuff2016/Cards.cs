using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HelloWorld.Buildstuff2016
{
	public class Cards: List<Card>
	{
		private static readonly Random rnd = new Random();
		public static readonly ReadOnlyCollection<Card> Deck = new ReadOnlyCollection<Card>(GetDeck());

		private static IList<Card> GetDeck()
		{
			var list = new List<Card>();
			var colors = new []{ CardColor.Club, CardColor.Heart, CardColor.Spade, CardColor.Diamond };

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
