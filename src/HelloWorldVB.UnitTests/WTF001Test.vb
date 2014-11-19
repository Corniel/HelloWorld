Imports System.Globalization
Imports System.Threading
Imports NUnit.Framework

<TestFixture>
Public Class WTF001Test

	<SetUp>
	Public Sub SetUp()
		Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
	End Sub

	<Test>
	Public Sub WTFDecimalSeparator_None_IsDot()

		Dim act As String = HelloWorldVB.WTF1.DecimalSeparator()
		Dim exp As String = "."

	End Sub

	<Test>
	Public Sub DecimalSeparator_None_IsDot()

		Dim act As String = HelloWorldVB.WTF1_.DecimalSeparator
		Dim exp As String = "."

	End Sub

End Class
