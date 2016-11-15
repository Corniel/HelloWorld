using System;
using System.Diagnostics;

namespace HelloWorld.Buildstuff2016
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Card
	{
		private const string Colors = "♠♥♣♦";
		private const string Values = "  23456789TJQKA";

		private readonly int value;
		internal Card(int color, int val)
		{
			value = color | (val << 2);
		}

		public CardColor Color { get { return (CardColor)(value & 3); } }
		public int Value { get { return value >> 2; } }


		public override int GetHashCode() { return value; }

		public override string ToString()
		{
			return string.Format("{0}{1}", Color.ToString()[0], Values[Value]);
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string DebuggerDisplay { get {
				var str = string.Format("{0}{1}", Colors[(int)Color], Values[Value]);
				return str.Replace("T", "10");
			}
		}

		public static Card Parse(string value)
		{
			if (!string.IsNullOrEmpty(value) && value.Length == 2)
			{
				value = value.ToUpperInvariant();
				var color = "SHCD".IndexOf(value[0]);
				var val = Values.IndexOf(value[1]);
				if (color != -1 && val > 1)
				{
					return new Card(color, val);
				}
			}
			throw new ArgumentException("Not a playing card.");
		}
	}
}
