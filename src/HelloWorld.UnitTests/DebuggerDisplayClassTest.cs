using NUnit.Framework;
using System.Reflection;

namespace HelloWorld.UnitTests
{
	[TestFixture]
	public class DebuggerDisplayClassTest
	{
		[Test]
		public void DebuggerDisplay_StringMethod_String17()
		{
			var cls = new DebuggerDisplayClassStringMethod() { Number = 17 };
			var dd = cls.GetType().GetMethod("DebuggerDisplay", BindingFlags.Instance | BindingFlags.NonPublic);

			var act = dd.Invoke(cls, new object[0]);
			var exp = "17";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void DebuggerDisplay_ObjectMethod_Int17()
		{
			var cls = new DebuggerDisplayClassObjectMethod() { Number = 17 };
			var dd = cls.GetType().GetMethod("DebuggerDisplay", BindingFlags.Instance | BindingFlags.NonPublic);

			var act = dd.Invoke(cls, new object[0]);
			var exp = 17;

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void DebuggerDisplay_StringProperty_String17()
		{
			var cls = new DebuggerDisplayClassStringProperty() { Number = 17 };
			var dd = cls.GetType().GetProperty("DebuggerDisplay", BindingFlags.Instance | BindingFlags.NonPublic);

			var act = dd.GetValue(cls);
			var exp = "17";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void DebuggerDisplay_ObjectProperty_Int17()
		{
			var cls = new DebuggerDisplayClassObjectProperty() { Number = 17 };
			var dd = cls.GetType().GetProperty("DebuggerDisplay", BindingFlags.Instance | BindingFlags.NonPublic);

			var act = dd.GetValue(cls);
			var exp = 17;

			Assert.AreEqual(exp, act);
		}
	}
}
