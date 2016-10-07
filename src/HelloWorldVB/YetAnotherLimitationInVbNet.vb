﻿Public Class YetAnotherLimitationInVbNet

	Public Function Total(list As List(Of Integer)) As Integer
		' Return list.Count(Function(n) n > 17) Does not compile.
		' Return list.Where(Function(n) n > 17).Count() is performing smart not the best option.
		Return list.AsEnumerable().Count(Function(n) n > 17)
	End Function

	Public Function Total(list As IEnumerable(Of Integer)) As Integer
		Return list.Count(Function(n) n > 17)
	End Function

End Class
