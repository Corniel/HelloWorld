Imports System.Reflection
Imports HelloWorld
Imports NUnit.Framework

<TestFixture>
Public Class DebuggerDisplayClassTest

	<Test>
	Public Sub DebuggerDisplay_StringMethod_String17()

		Dim cls As DebuggerDisplayClassStringMethod = New DebuggerDisplayClassStringMethod()
		cls.Number = 17
		Dim dd As MethodInfo = cls.GetType().GetMethod("DebuggerDisplay", BindingFlags.Instance Or BindingFlags.NonPublic)

		Dim act As Object = dd.Invoke(cls, Nothing)
		Dim exp As Object = "17"

		Assert.AreEqual(exp, act)
	End Sub

	<Test>
	Public Sub DebuggerDisplay_ObjectMethod_Int17()

		Dim cls As DebuggerDisplayClassObjectMethod = New DebuggerDisplayClassObjectMethod()
		cls.Number = 17

		Dim dd As MethodInfo = cls.GetType().GetMethod("DebuggerDisplay", BindingFlags.Instance Or BindingFlags.NonPublic)

		Dim act As Object = dd.Invoke(cls, Nothing)
		Dim exp = 17

		Assert.AreEqual(exp, act)
	End Sub

	<Test>
	Public Sub DebuggerDisplay_StringProperty_String17()

		Dim cls As DebuggerDisplayClassStringProperty = New DebuggerDisplayClassStringProperty()
		cls.Number = 17
		Dim dd As PropertyInfo = cls.GetType().GetProperty("DebuggerDisplay", BindingFlags.Instance Or BindingFlags.NonPublic)

		Dim act As Object = dd.GetValue(cls)
		Dim exp As Object = "17"

		Assert.AreEqual(exp, act)
	End Sub

	<Test>
	Public Sub DebuggerDisplay_ObjectProperty_Int17()

		Dim cls As DebuggerDisplayClassObjectProperty = New DebuggerDisplayClassObjectProperty()
		cls.Number = 17

		Dim dd As PropertyInfo = cls.GetType().GetProperty("DebuggerDisplay", BindingFlags.Instance Or BindingFlags.NonPublic)

		Dim act As Object = dd.GetValue(cls)
		Dim exp As Object = 17

		Assert.AreEqual(exp, act)
	End Sub

End Class
