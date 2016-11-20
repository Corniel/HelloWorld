using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HelloWorld.Buildstuff2016
{
	/// <summary>Represents a playing card.</summary>
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Card : IEquatable<Card>, IComparable<Card>, IFormattable,  IXmlSerializable
	{
		private const string Suits = "SHCD";
		private const string Symbols = "♠♥♣♦";
		private const string Values = "  23456789TJQKA";

		/// <summary>A constant for every card might even be a good idea too.</summary>
		/// <remarks>
		/// Note, you cant not use the <code>const</code> keyword, as those are
		/// reserved for primative types.
		/// </remarks>
		public static readonly Card AceOfSpades = new Card(Suit.Spade, 14);

		/// <summary>Gets the underlying value.</summary>
		/// <remarks>
		/// Because of how XML Serialization works, this value can not be read-only.
		/// In practice it still is, besides the creation of cards when deserializing.
		/// </remarks>
		private /*read-only*/ int value;

		/// <summary>Initializes a new card.</summary>
		/// <remarks>
		/// The two sub parts of this Single Value Object are stored as one integer.
		/// The first two bit are stored for the suit, the rest for the value of the card.
		/// 
		/// The assignment can also be written as: value = suit + value * 4
		/// </remarks>
		public Card(Suit suit, int val): this((int)suit, val) { }

		/// <summary>Initializes a new card.</summary>
		/// <remarks>
		/// The two sub parts of this Single Value Object are stored as one integer.
		/// The first two bit are stored for the suit, the rest for the value of the card.
		/// 
		/// The assignment can also be written as: value = suit + value * 4
		/// </remarks>
		internal Card(int suit, int val)
		{
			value = suit | (val << 2);
		}

		/// <summary>Gets the suit of the card.</summary>
		/// <remarks>
		/// Same as return value % 4
		/// 
		/// Means that only the first two bit are taken into account.
		/// </remarks>
		public Suit Suit { get { return (Suit)(value & 3); } }

		/// <summary>Gets the value of the card.</summary>
		/// <remarks>
		/// Same as return value / 4
		/// 
		/// Means that the first two bits are ignored.
		/// </remarks>
		public int Value { get { return value >> 2; } }

		/// <summary>Gets a (unique) hash code for the card.</summary>
		public override int GetHashCode() { return value; }

		/// <summary>Returns true if the objects are equal, otherwise false.</summary>
		/// <remarks>
		/// By default the desired result is returned.
		/// 
		/// However, if speed is key, you might want to implement it differently
		/// (less generic).
		/// 
		/// <code>
		/// BCLDebug.Perf(false, "ValueType::Equals is not fast.  " + this.GetType().FullName + " should override Equals(Object)");
		/// if (null == obj)
		/// {
		/// 	return false;
		/// }
		/// RuntimeType thisType = (RuntimeType)this.GetType();
		/// RuntimeType thatType = (RuntimeType)obj.GetType();
		///
		/// if (thatType != thisType)
		/// {
		/// 	return false;
		/// }
		///
		/// Object thisObj = (Object)this;
		/// Object thisResult, thatResult;
		///
		/// // if there are no GC references in this object we can avoid reflection 
		/// // and do a fast memcmp
		/// if (CanCompareBits(this))
		/// 	return FastEqualsCheck(thisObj, obj);
		///
		/// FieldInfo[] thisFields = thisType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		///
		/// for (int i = 0; i < thisFields.Length; i++)
		/// {
		/// 	thisResult = ((RtFieldInfo)thisFields[i]).UnsafeGetValue(thisObj);
		/// 	thatResult = ((RtFieldInfo)thisFields[i]).UnsafeGetValue(obj);
		///
		/// 	if (thisResult == null)
		/// 	{
		/// 		if (thatResult != null)
		/// 			return false;
		/// 	}
		/// 	else
		/// 	if (!thisResult.Equals(thatResult))
		/// 	{
		/// 		return false;
		/// 	}
		/// }
		///
		/// return true;
		/// </code>
		/// </remarks>
		public override bool Equals(object obj)
		{
			return obj is Card && Equals((Card)obj);
		}

		/// <summary>Returns true if the cards are equal, otherwise false.</summary>
		public bool Equals(Card other)
		{
			return value.Equals(other.value);
		}

		public static bool operator ==(Card l, Card r) { return l.Equals(r); }
		public static bool operator !=(Card l, Card r) { return !(l == r); }


		#region IXmlSerializable

		/// <summary>Returns <see cref="null"/> as special schema's should not be required.</summary>
		XmlSchema IXmlSerializable.GetSchema() { return null; }

		/// <summary>Because XML serialization does not support returning a new instance,
		/// we have to modify the internal. This should be the only violation of its
		/// immutability.
		/// </summary>
		/// <param name="reader"></param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			var s = reader.ReadElementString();
			var val = Parse(s);
			value = val.value;
		}

		/// <summary>Just write the <see cref="string"/> representation.</summary>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteValue(ToString());
		}
		#endregion

		/// <summary>Formats the <see cref="Card"/> as a <see cref="string"/>.</summary>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			var formatted = "f" == format;
			var symbol = formatted ? Symbols[(int)Suit] : Suits[(int)Suit];
			var number = Values[Value].ToString();
			if (formatted) { number= number.Replace("T", "10"); }
			return string.Format("{0}{1}", symbol, number);
		}

		/// <summary>Returns the <see cref="Card"/> as a <see cref="string"/>.</summary>
		public override string ToString()
		{
			return ToString(null, null);
		}

		/// <summary>Represents a debug <see cref="string"/> representation of the <see cref="Card"/>.</summary>
		/// <remarks>
		/// Just to make your debug experience better.
		/// </remarks>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public string DebuggerDisplay { get { return ToString("f", CultureInfo.InvariantCulture); } }

		public static Card Parse(string value)
		{
			if (!string.IsNullOrEmpty(value) && value.Length == 2)
			{
				value = value.ToUpperInvariant();
				var color = Math.Max(Suits.IndexOf(value[0]), Symbols.IndexOf(value[0]));
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
