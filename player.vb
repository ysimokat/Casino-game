Public Class player
    Public name As String
    Public age As Integer
    Public wins As Integer
    Public losses As Integer
    Public skills As Integer

    Public Function winPercent() As Decimal
        Dim wp As Decimal

        For i = 0 To 50 - 1
            If wins + losses = 0 Then
                wp = 0
            Else
                wp = wins / (wins + losses)
            End If
        Next

        Return wp
    End Function

    Public Function record() As String
        'format the string as (wins-losses)
        Dim output As String

        output = "(" & wins & "-" & losses & ")"

        Return output
    End Function

    Public Function newName() As String
        Dim output As String

        output = name
        Return output
    End Function
End Class
