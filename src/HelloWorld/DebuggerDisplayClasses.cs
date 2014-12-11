using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HelloWorld
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class DebuggerDisplayClassStringProperty
	{
		public int Number { get; set; }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay { get { return this.Number.ToString(CultureInfo.InvariantCulture); } }
	}

	[DebuggerDisplay("{DebuggerDisplay()}")]
	public class DebuggerDisplayClassStringMethod
	{
		public int Number { get; set; }
		private string DebuggerDisplay() { return this.Number.ToString(CultureInfo.InvariantCulture); }
	}

	[DebuggerDisplay("{DebuggerDisplay}")]
	public class DebuggerDisplayClassObjectProperty
	{
		public int Number { get; set; }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int DebuggerDisplay { get { return this.Number; } }
	}

	[DebuggerDisplay("{DebuggerDisplay()}")]
	public class DebuggerDisplayClassObjectMethod
	{
		public int Number { get; set; }

		[ExcludeFromCodeCoverage]
		private int DebuggerDisplay() { return this.Number; }
	}
}
