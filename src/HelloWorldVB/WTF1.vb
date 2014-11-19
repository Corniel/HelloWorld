Public Class WTF1

	Public Shared Function DecimalSeparator() As String
		DecimalSeparator = (1 / 2).ToString.Substring(1, 1)
	End Function

End Class

Public Class WTF1_

	Public Shared ReadOnly Property DecimalSeparator As String
		Get
			Return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
		End Get
	End Property

End Class
