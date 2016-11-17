using System;
using System.Diagnostics;
using System.Globalization;

namespace HelloWorld.Buildstuff2016
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Card : IFormattable, IComparable<Card>
	{
		private const string Colors = "SHCD";
		private const string Symbols = "♠♥♣♦";
		private const string Values = "  23456789TJQKA";

		private readonly int value;
		internal Card(int color, int val)
		{
			value = color | (val << 2);
		}

		public CardColor Color { get { return (CardColor)(value & 3); } }
		public int Value { get { return value >> 2; } }


		public override int GetHashCode() { return value; }

		public string ToString(string format, IFormatProvider formatProvider)
		{
			var formatted = "f" == format;
			var symbol = formatted ? Symbols[(int)Color] : Colors[(int)Color];
			var number = Values[Value].ToString();
			if (formatted) { number= number.Replace("T", "10"); }
			return string.Format("{0}{1}", symbol, number);
		}

		public override string ToString()
		{
			return ToString(null, null);
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string DebuggerDisplay { get { return ToString("f", CultureInfo.InvariantCulture); } }

		public static Card Parse(string value)
		{
			if (!string.IsNullOrEmpty(value) && value.Length == 2)
			{
				value = value.ToUpperInvariant();
				var color = Math.Max(Colors.IndexOf(value[0]), Symbols.IndexOf(value[0]));
				var val = Values.IndexOf(value[1]);
				if (color != -1 && val > 1)
				{
					return new Card(color, val);
				}
			}
			throw new ArgumentException("Not a playing card.");
		}

		public int CompareTo(Card other) { return value.CompareTo(other.value); }
	}
}
